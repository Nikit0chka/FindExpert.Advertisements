using Ardalis.Result;
using Ardalis.SharedKernel;
using Domain.AggregateModels.AdvertisementAggregate;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Advertisements.GetAdvertisement;

internal sealed class GetAdvertisementHandler(IReadRepository<Advertisement> advertisementRepository, ILogger<GetAdvertisementHandler> logger):IQueryHandler<GetAdvertisementQuery, Result<Advertisement>>
{
    public async Task<Result<Advertisement>> Handle(GetAdvertisementQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling {Query} for advertisement with Id: {AdvertisementId}", nameof(GetAdvertisementQuery), request.Id);

        try
        {
            var advertisement = await advertisementRepository.GetByIdAsync(request.Id, cancellationToken);

            if (advertisement is null)
            {
                logger.LogWarning("Advertisement with Id {Id} not found", request.Id);
                return Result.NotFound($"Advertisement with Id: {request.Id} does not exist");
            }

            logger.LogInformation("{Query} handled successful", nameof(GetAdvertisementQuery));

            return advertisement;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while handling {Query}, for advertisement with Id: {AdvertisementId}", nameof(GetAdvertisementQuery),  request.Id);
            return Result.Error("An error occured while handling your request");
        }
    }
}