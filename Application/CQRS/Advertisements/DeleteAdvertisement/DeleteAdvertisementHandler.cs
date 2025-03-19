using Ardalis.Result;
using Ardalis.SharedKernel;
using Domain.AggregateModels.AdvertisementAggregate;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Advertisements.DeleteAdvertisement;

internal sealed class DeleteAdvertisementHandler(ILogger<DeleteAdvertisementHandler> logger, IRepository<Advertisement> advertisementRepository):ICommandHandler<DeleteAdvertisementCommand, Result>
{
    public async Task<Result> Handle(DeleteAdvertisementCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling {Command} for user with Id: {UserId}", nameof(DeleteAdvertisementCommand), request.UserId);

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

            await advertisementRepository.DeleteAsync(advertisement, cancellationToken);

            logger.LogInformation("Advertisement with Id: {AdvertisementId} deleted successful", advertisement.Id);
            logger.LogInformation("{Command} handled successful UserId: {UserId}, AdvertisementId: {AdvertisementId}", nameof(DeleteAdvertisementCommand), request.UserId, advertisement.Id);

            return Result.NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while handling {Command}, for user with Id: {UserId}", nameof(DeleteAdvertisementCommand), request.UserId);
            return Result.Error("An error occured while handling your request");
        }
    }
}