using Application.CQRS.Advertisements.Get;
using Ardalis.SharedKernel;
using Domain.AggregateModels.AdvertisementAggregate;
using Domain.AggregateModels.AdvertisementAggregate.Specifications;
using Domain.Utils;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Advertisements.GetMy;

internal sealed class GetMyAdvertisementsHandler(IReadRepository<Advertisement> advertisementRepository, ILogger<GetAdvertisementHandler> logger):IQueryHandler<GetMyAdvertisementsQuery, OperationResult<IReadOnlyCollection<Advertisement>>>
{
    public async Task<OperationResult<IReadOnlyCollection<Advertisement>>> Handle(GetMyAdvertisementsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling {Query} for UserId: {UserId}", nameof(GetMyAdvertisementsQuery), request.UserId);

        try
        {
            var advertisements = await advertisementRepository.ListAsync(new AdvertisementByUserIdWithResponsesAndCategoriesSpecification(request.UserId), cancellationToken);

            logger.LogInformation("{Query} handled successful", nameof(GetMyAdvertisementsQuery));

            return OperationResult<IReadOnlyCollection<Advertisement>>.Success(advertisements);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while handling {Query}, for UserId: {UserId}", nameof(GetMyAdvertisementsQuery), request.UserId);
            return OperationResult<IReadOnlyCollection<Advertisement>>.Error();
        }
    }
}