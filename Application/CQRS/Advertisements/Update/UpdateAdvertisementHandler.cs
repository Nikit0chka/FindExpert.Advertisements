using Ardalis.SharedKernel;
using Domain.AggregateModels.AdvertisementAggregate;
using Domain.Utils;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Advertisements.Update;

internal sealed class UpdateAdvertisementHandler(ILogger<UpdateAdvertisementHandler> logger, IRepository<Advertisement> advertisementRepository):ICommandHandler<UpdateAdvertisementCommand, OperationResult>
{
    public async Task<OperationResult> Handle(UpdateAdvertisementCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling {Command} for AdvertisementID: {AdvertisementID} UserId: {UserId}", nameof(UpdateAdvertisementCommand), request.Id, request.UserId);

        try
        {
            var advertisement = await advertisementRepository.GetByIdAsync(request.Id, cancellationToken);

            if (advertisement is null)
            {
                logger.LogWarning("Advertisement with Id: {Id} not found", request.Id);
                return OperationResult.Error(UpdateAdvertisementErrorCodes.AdvertisementNotFound);
            }

            if (advertisement.UserId != request.UserId)
            {
                logger.LogWarning("An attempt to delete advertisement without access, UserId: {UserId}, AdvertisementId: {AdvertisementId}", request.UserId, advertisement.Id);
                return OperationResult.Error(UpdateAdvertisementErrorCodes.Forbidden);
            }

            var updateResult = advertisement.Update(request.Name, request.Description, request.UserId, request.AdvertisementCategoryId);

            if (!updateResult.IsSuccess)
                return updateResult;

            await advertisementRepository.UpdateAsync(advertisement, cancellationToken);

            logger.LogInformation("{Command} handled successful. Advertisement updated. UserId: {UserId}, AdvertisementId: {AdvertisementId}", nameof(UpdateAdvertisementCommand), request.UserId, advertisement.Id);

            return OperationResult.Success();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while handling {Command}, for user with Id: {UserId}, AdvertisementId: {AdvertisementId}", nameof(UpdateAdvertisementCommand), request.UserId, request.Id);
            return OperationResult.Error();
        }
    }
}