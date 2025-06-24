namespace Domain.AggregateModels.AdvertisementAggregate.Specifications;

public sealed class AdvertisementByUserIdSpecification:BaseAdvertisementSpecification
{
    public AdvertisementByUserIdSpecification(int userId) { ApplyUserIdCriteria(userId); }
}