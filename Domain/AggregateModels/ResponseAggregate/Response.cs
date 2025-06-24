using Ardalis.GuardClauses;
using Ardalis.SharedKernel;
using Domain.AggregateModels.AdvertisementAggregate;
using Domain.Extensions;
using JetBrains.Annotations;

namespace Domain.AggregateModels.ResponseAggregate;

public class Response:EntityBase, IAggregateRoot
{
    //TODO: убрать public set
    public string Comment { get; set; }
    public int UserId { get; private set; }
    public int AdvertisementId { get; private set; }
    public Advertisement Advertisement { get; private set; }
    public DateTime DateCreated { get; init; } = DateTime.UtcNow;

    [UsedImplicitly]
    private Response() { }

    public Response(string comment, int userId, int advertisementId, int advertisementOwnerId)
    {
        Validate(comment, userId, advertisementId, advertisementOwnerId);

        Comment = comment;
        UserId = userId;
        AdvertisementId = advertisementId;
    }

    private static void Validate(string comment, int userId, int advertisementId, int advertisementOwnerId)
    {
        Guard.Against.NullOrEmpty(comment, nameof(comment), ResponseValidationMessages.CommentIsRequired);

        GuardAgainstExtensions.StringLengthOutOfRange(comment,
                                                      ResponseConstants.MinCommentLength,
                                                      ResponseConstants.MaxCommentLength,
                                                      nameof(comment),
                                                      ResponseValidationMessages.CommentLengthIsOutOfRange);


        Guard.Against.NegativeOrZero(userId, nameof(userId), ResponseValidationMessages.UserIdMustBeGreaterThanZero);
        Guard.Against.NegativeOrZero(advertisementId, nameof(advertisementId), ResponseValidationMessages.AdvertisementIdMustBeGreaterThanZero);
        Guard.Against.NegativeOrZero(advertisementOwnerId, nameof(advertisementOwnerId), ResponseValidationMessages.AdvertisementOwnerIdMustBeGreaterThanZero);

        if (advertisementOwnerId == userId)
            throw new InvalidOperationException("AdvertisementOwnerId cannot be equal to UserId.");
    }
}