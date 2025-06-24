using Ardalis.Specification;

namespace Domain.AggregateModels.AdvertisementAggregate.Specifications;

public sealed class AdvertisementWithCategoryAndResponsesSpecification:SingleResultSpecification<Advertisement>
{
    public AdvertisementWithCategoryAndResponsesSpecification(int id)
    {
        Query.Where(advertisement => advertisement.Id == id)
            .Include(static advertisement => advertisement.Category).Include(static advertisement => advertisement.Responses).Include(advertisement => advertisement.Responses);
    }
}