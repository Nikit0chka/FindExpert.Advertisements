using Ardalis.SharedKernel;
using Domain.AggregateModels.CategoryAggregate;
using Domain.Utils;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Categories.GetList;

internal class GetCategoriesHandler(IReadRepository<Category> categoryRepository, ILogger<GetCategoriesHandler> logger):IQueryHandler<GetCategoriesListQuery, OperationResult<IReadOnlyCollection<Category>>>
{
    public async Task<OperationResult<IReadOnlyCollection<Category>>> Handle(GetCategoriesListQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling {Query}", nameof(GetCategoriesListQuery));

        try
        {
            var categories = await categoryRepository.ListAsync(cancellationToken);

            logger.LogInformation("{Query} handled successful", nameof(GetCategoriesListQuery));

            return OperationResult<IReadOnlyCollection<Category>>.Success(categories);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while handling {Query}", nameof(GetCategoriesListQuery));
            return OperationResult<IReadOnlyCollection<Category>>.Error();
        }
    }
}