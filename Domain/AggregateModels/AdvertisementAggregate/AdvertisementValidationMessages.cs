namespace Domain.AggregateModels.AdvertisementAggregate;

public class AdvertisementValidationMessages
{
    public const string NameIsRequired = "Advertisement name cannot be null or empty.";
    public const string DescriptionIsRequired = "Advertisement description cannot be null or empty.";
    public const string UserIdMustBeGreaterThanZero = "User id cannot be less or equal to zero.";
    public const string AdvertisementCategoryIdMustBeGreaterThanZero = "Advertisement category id cannot be less or equal to zero.";

    public readonly static string NameLengthIsOutOfRange = $"Advertisement name must be between {AdvertisementConstants.MinNameLength} and {AdvertisementConstants.MaxNameLength} characters.";
    public readonly static string DescriptionLengthIsOutOfRange = $"Advertisement description must be between {AdvertisementConstants.MinDescriptionLength} and {AdvertisementConstants.MaxDescriptionLength} characters.";
}