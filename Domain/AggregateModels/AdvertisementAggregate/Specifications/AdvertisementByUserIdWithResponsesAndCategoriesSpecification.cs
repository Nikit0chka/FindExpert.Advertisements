namespace Domain.AggregateModels.AdvertisementAggregate.Specifications;

public sealed class AdvertisementByUserIdWithResponsesAndCategoriesSpecification:BaseAdvertisementSpecification
{
    public AdvertisementByUserIdWithResponsesAndCategoriesSpecification(int userId)
    {
        ApplyUserIdCriteria(userId);
        IncludeCategory();
        IncludeResponses();
    }
}