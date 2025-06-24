namespace Domain.AggregateModels.AdvertisementAggregate.Specifications;

public sealed class AdvertisementByCategoryIdAndSearchTextSpecificationWithResponses:BaseAdvertisementSpecification
{
    public AdvertisementByCategoryIdAndSearchTextSpecificationWithResponses(int categoryId, string searchText)
    {
        ApplyCategoryIdCriteria(categoryId);
        ApplySearchTextCriteria(searchText);
        IncludeResponses();
        IncludeCategory();
    }
}