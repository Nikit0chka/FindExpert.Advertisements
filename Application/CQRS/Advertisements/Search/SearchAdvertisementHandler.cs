using Ardalis.SharedKernel;
using Domain.AggregateModels.AdvertisementAggregate;
using Domain.AggregateModels.AdvertisementAggregate.Specifications;
using Domain.Utils;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Advertisements.Search;

public class SearchAdvertisementHandler(ILogger<SearchAdvertisementHandler> logger, IReadRepository<Advertisement> advertisementRepository):IQueryHandler<SearchAdvertisementQuery, OperationResult<List<Advertisement>>>
{
    public async Task<OperationResult<List<Advertisement>>> Handle(SearchAdvertisementQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Starting handling {Query}", nameof(SearchAdvertisementQuery));

        try
        {
            List<Advertisement> advertisements;

            if (request.CategoryId != 0 && !string.IsNullOrEmpty(request.SearchText))
                advertisements = await advertisementRepository.ListAsync(new AdvertisementByCategoryIdAndSearchTextSpecificationWithResponses(request.CategoryId, request.SearchText), cancellationToken);
            else if (request.CategoryId != 0 && string.IsNullOrEmpty(request.SearchText))
                advertisements = await advertisementRepository.ListAsync(new AdvertisementByCategoryIdSpecificationWithResponses(request.CategoryId), cancellationToken);
            else if (request.CategoryId == 0 && !string.IsNullOrEmpty(request.SearchText))
                advertisements = await advertisementRepository.ListAsync(new AdvertisementBySearchTextSpecificationWithResponses(request.SearchText), cancellationToken);
            else
                advertisements = await advertisementRepository.ListAsync(new AdvertisementsWithCategoryAndResponsesSpecification(), cancellationToken);

            return OperationResult<List<Advertisement>>.Success(advertisements);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while handling {Query}", nameof(SearchAdvertisementQuery));
            return OperationResult<List<Advertisement>>.Error();
        }
    }
}