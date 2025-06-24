namespace Domain.AggregateModels.AdvertisementAggregate.Specifications;

public sealed class AdvertisementBySearchTextSpecificationWithResponses:BaseAdvertisementSpecification
{
    public AdvertisementBySearchTextSpecificationWithResponses(string searchText)
    {
        ApplySearchTextCriteria(searchText);
        IncludeResponses();
        IncludeCategory();
    }
}