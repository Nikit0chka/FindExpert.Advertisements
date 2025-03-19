using Ardalis.GuardClauses;
using Ardalis.Result;
using Ardalis.SharedKernel;
using Domain.AggregateModels.ServiceOfferRequestCategoryAggregate;
using Domain.Extensions;

namespace Domain.AggregateModels.AdvertisementAggregate;

public class Advertisement:EntityBase, IAggregateRoot
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public int UserId { get; private set; }
    public DateTime DateCreated { get; init; } = DateTime.UtcNow;
    public int AdvertisementCategoryId { get; private set; }
    public virtual AdvertisementCategory AdvertisementCategory { get; private set; }

    protected Advertisement() { }

    public Advertisement(string name, string description, int userId, int advertisementCategoryId)
    {
        Validate(name, description, userId, advertisementCategoryId);

        Name = name;
        Description = description;
        UserId = userId;
        AdvertisementCategoryId = advertisementCategoryId;
    }

    public Result Update(string name, string description, int userId, int serviceOfferRequestCategoryId)
    {
        if (userId != UserId)
            return Result.Forbidden("Has no access to this advertisement");

        Validate(name, description, userId, serviceOfferRequestCategoryId);

        Name = name;
        Description = description;
        AdvertisementCategoryId = serviceOfferRequestCategoryId;
        return Result.Success();
    }


    private static void Validate(string name, string description, int userId, int serviceOfferRequestCategoryId)
    {
        Guard.Against.NullOrEmpty(name, nameof(name), AdvertisementValidationMessages.NameIsRequired);

        GuardAgainstExtensions.StringLengthOutOfRange(name,
                                                      AdvertisementConstants.MinNameLength,
                                                      AdvertisementConstants.MaxNameLength,
                                                      nameof(name),
                                                      AdvertisementValidationMessages.NameLengthIsOutOfRange);

        Guard.Against.NullOrEmpty(name, nameof(description), AdvertisementValidationMessages.DescriptionIsRequired);

        GuardAgainstExtensions.StringLengthOutOfRange(description,
                                                      AdvertisementConstants.MinDescriptionLength,
                                                      AdvertisementConstants.MaxDescriptionLength,
                                                      nameof(description),
                                                      AdvertisementValidationMessages.DescriptionLengthIsOutOfRange);

        Guard.Against.NegativeOrZero(userId, nameof(userId), AdvertisementValidationMessages.UserIdMustBeGreaterThanZero);
        Guard.Against.NegativeOrZero(serviceOfferRequestCategoryId, nameof(serviceOfferRequestCategoryId), AdvertisementValidationMessages.AdvertisementCategoryIdMustBeGreaterThanZero);

    }
}