using Ardalis.Specification;

namespace Domain.AggregateModels.AdvertisementAggregate.Specifications;

public sealed class AdvertisementWithCategorySpecification:SingleResultSpecification<Advertisement>
{
    public AdvertisementWithCategorySpecification(int id)
    {
        Query.Where(advertisement => advertisement.Id == id)
            .Include(static advertisement => advertisement.Category).Include(static advertisement => advertisement.Responses);
    }
}