using Ardalis.SharedKernel;
using Domain.AggregateModels.AdvertisementAggregate;
using Domain.AggregateModels.AdvertisementAggregate.Specifications;
using Domain.Utils;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Advertisements.Get;

internal sealed class GetAdvertisementHandler(IReadRepository<Advertisement> advertisementRepository, ILogger<GetAdvertisementHandler> logger):IQueryHandler<GetAdvertisementQuery, OperationResult<GetAdvertisementResult>>
{
    public async Task<OperationResult<GetAdvertisementResult>> Handle(GetAdvertisementQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling {Query} for advertisement with Id: {AdvertisementId}", nameof(GetAdvertisementQuery), request.Id);

        try
        {
            Advertisement? advertisement;

            if (request.IncludeResponses)
                advertisement = await advertisementRepository.SingleOrDefaultAsync(new AdvertisementWithCategoryAndResponsesSpecification(request.Id), cancellationToken);
            else
                advertisement = await advertisementRepository.SingleOrDefaultAsync(new AdvertisementWithCategorySpecification(request.Id), cancellationToken);


            if (advertisement is null)
            {
                logger.LogWarning("Advertisement with Id {Id} not found", request.Id);
                return OperationResult<GetAdvertisementResult>.Error(GetAdvertisementErrorCodes.AdvertisementNotFound);
            }

            logger.LogInformation("{Query} handled successful", nameof(GetAdvertisementQuery));

            return OperationResult<GetAdvertisementResult>.Success(new(advertisement, advertisement.Responses.Any(response => response.UserId == request.UserId)));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while handling {Query}, for advertisement with Id: {AdvertisementId}", nameof(GetAdvertisementQuery), request.Id);
            return OperationResult<GetAdvertisementResult>.Error();
        }
    }
}