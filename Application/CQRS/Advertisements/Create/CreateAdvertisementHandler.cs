using Ardalis.SharedKernel;
using Domain.AggregateModels.AdvertisementAggregate;
using Domain.Utils;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Advertisements.Create;

internal sealed class CreateAdvertisementHandler(ILogger<CreateAdvertisementHandler> logger, IRepository<Advertisement> advertisementRepository):ICommandHandler<CreateAdvertisementCommand, OperationResult>
{
    public async Task<OperationResult> Handle(CreateAdvertisementCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling {Command} for user with Id: {UserId}", nameof(CreateAdvertisementCommand), request.UserId);

        try
        {
            var advertisement = new Advertisement(request.Name, request.Description, request.UserId, request.AdvertisementCategoryId);

            await advertisementRepository.AddAsync(advertisement, cancellationToken);

            logger.LogInformation("{Command} handled successful. Advertisement added. UserId: {UserId}", nameof(CreateAdvertisementCommand), request.UserId);

            return OperationResult.Success();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while handling {Command}, for user with Id: {UserId}", nameof(CreateAdvertisementCommand), request.UserId);
            return OperationResult.Error();
        }
    }
}