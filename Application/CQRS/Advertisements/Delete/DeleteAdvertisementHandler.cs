using Ardalis.SharedKernel;
using Domain.AggregateModels.AdvertisementAggregate;
using Domain.Utils;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Advertisements.Delete;

internal sealed class DeleteAdvertisementHandler(ILogger<DeleteAdvertisementHandler> logger, IRepository<Advertisement> advertisementRepository):ICommandHandler<DeleteAdvertisementCommand, OperationResult>
{
    public async Task<OperationResult> Handle(DeleteAdvertisementCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling {Command} for user with Id: {UserId}", nameof(DeleteAdvertisementCommand), request.UserId);

        try
        {
            var advertisement = await advertisementRepository.GetByIdAsync(request.Id, cancellationToken);

            if (advertisement is null)
            {
                logger.LogWarning("Advertisement with Id: {Id} not found", request.Id);
                return OperationResult.Error(DeleteAdvertisementErrorCodes.AdvertisementNotFound);
            }

            if (advertisement.UserId != request.UserId)
            {
                logger.LogWarning("An attempt to delete advertisement without access, UserId: {UserId}, AdvertisementId: {AdvertisementId}", request.UserId, advertisement.Id);
                return OperationResult.Error(DeleteAdvertisementErrorCodes.Forbidden);
            }

            await advertisementRepository.DeleteAsync(advertisement, cancellationToken);

            logger.LogInformation("{Command} handled successful. Advertisement deleted successful. UserId: {UserId}, AdvertisementId: {AdvertisementId}", nameof(DeleteAdvertisementCommand), request.UserId, advertisement.Id);

            return OperationResult.Success();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while handling {Command}, for user with Id: {UserId}", nameof(DeleteAdvertisementCommand), request.UserId);
            return OperationResult.Error();
        }
    }
}