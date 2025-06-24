using Ardalis.Specification;

namespace Domain.AggregateModels.AdvertisementAggregate.Specifications;

public abstract class BaseAdvertisementSpecification:Specification<Advertisement>
{
    protected void ApplyUserIdCriteria(int userId) { Query.Where(advertisement => advertisement.UserId == userId); }

    protected void ApplySearchTextCriteria(string searchText) { Query.Where(advertisement => advertisement.Description.Contains(searchText) || advertisement.Name.Contains(searchText)); }

    protected void ApplyCategoryIdCriteria(int categoryId) { Query.Where(advertisement => advertisement.CategoryId == categoryId); }

    protected void IncludeCategory() { Query.Include(static advertisement => advertisement.Category); }

    protected void IncludeResponses() { Query.Include(static advertisement => advertisement.Responses); }
}