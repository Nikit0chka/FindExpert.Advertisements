namespace Domain.AggregateModels.ServiceOfferRequestCategoryAggregate;

public static class AdvertisementCategoryValidationMessages
{
    public const string NameIsRequired = "Advertisement category name cannot be null or empty.";
    public const string ParentCategoryIdMustBeGreaterThanZero = "Advertisement category parent id must be greater than 0)";

    public readonly static string NameLengthOutOfRange = $"Advertisement category name must be between {AdvertisementCategoryConstants.MinNameLength} and {AdvertisementCategoryConstants.MaxNameLength} characters long.";
}