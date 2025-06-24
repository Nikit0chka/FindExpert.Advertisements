namespace Domain.AggregateModels.AdvertisementAggregate.Specifications;

public sealed class AdvertisementByCategoryIdSpecificationWithResponses:BaseAdvertisementSpecification
{
    public AdvertisementByCategoryIdSpecificationWithResponses(int categoryId)
    {
        ApplyCategoryIdCriteria(categoryId);
        IncludeResponses();
        IncludeCategory();
    }
}