namespace Domain.AggregateModels.AdvertisementAggregate.Specifications;

public sealed class AdvertisementsWithCategoryAndResponsesSpecification:BaseAdvertisementSpecification
{
    public AdvertisementsWithCategoryAndResponsesSpecification()
    {
        IncludeCategory();
        IncludeResponses();
    }
}