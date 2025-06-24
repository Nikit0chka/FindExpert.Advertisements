namespace Domain.AggregateModels.ResponseAggregate;

public static class ResponseValidationMessages
{
    public const string CommentIsRequired = "Comment cannot be null or empty.";
    internal const string UserIdMustBeGreaterThanZero = "User id cannot be less or equal to zero.";
    public const string AdvertisementIdMustBeGreaterThanZero = "Advertisement id cannot be less or equal to zero.";
    internal const string AdvertisementOwnerIdMustBeGreaterThanZero = "Advertisement owner id cannot be less or equal to zero.";


    public readonly static string CommentLengthIsOutOfRange = $"Response comment must be between {ResponseConstants.MinCommentLength} and {ResponseConstants.MaxCommentLength} characters.";

}