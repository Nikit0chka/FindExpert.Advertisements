using Ardalis.Result;
using Ardalis.SharedKernel;
using Domain.AggregateModels.AdvertisementAggregate;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Advertisements.UpdateAdvertisement;

public sealed class UpdateAdvertisementHandler(ILogger<UpdateAdvertisementHandler> logger, IRepository<Advertisement> advertisementRepository):ICommandHandler<UpdateAdvertisementCommand, Result>
{
    public async Task<Result> Handle(UpdateAdvertisementCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling {Command} for user with Id: {UserId}", nameof(UpdateAdvertisementCommand), request.UserId);

        try
        {
            var advertisement = await advertisementRepository.GetByIdAsync(request.Id, cancellationToken);

            if (advertisement is null)
            {
                logger.LogWarning("Advertisement with Id: {Id} not found", request.Id);
                return Result.NotFound($"Advertisement with Id: {request.Id} was not found.");
            }

            if (advertisement.UserId != request.UserId)
            {
                logger.LogWarning("An attempt to delete advertisement without access, UserId: {UserId}, AdvertisementId: {AdvertisementId}", request.UserId, advertisement.Id);
                return Result.Forbidden("Have no access to delete this advertisement.");
            }

            var updateResult = advertisement.Update(request.Name, request.Description, request.UserId, request.AdvertisementCategoryId);

            if (!updateResult.IsSuccess)
                return updateResult;

            await advertisementRepository.UpdateAsync(advertisement, cancellationToken);

            logger.LogInformation("Advertisement with Id: {AdvertisementId} updated successful", advertisement.Id);
            logger.LogInformation("{Command} handled successful UserId: {UserId}, AdvertisementId: {AdvertisementId}", nameof(UpdateAdvertisementCommand), request.UserId, advertisement.Id);

            return Result.NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while handling {Command}, for user with Id: {UserId}, AdvertisementId: {AdvertisementId}", nameof(UpdateAdvertisementCommand), request.UserId, request.Id);
            return Result.Error("An error occured while handling your request");
        }
    }
}