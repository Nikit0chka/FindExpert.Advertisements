using Ardalis.GuardClauses;
using Ardalis.SharedKernel;
using Domain.AggregateModels.CategoryAggregate;
using Domain.AggregateModels.ResponseAggregate;
using Domain.Extensions;
using Domain.Utils;
using JetBrains.Annotations;

namespace Domain.AggregateModels.AdvertisementAggregate;

public class Advertisement:EntityBase, IAggregateRoot
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public int UserId { get; private set; }
    public DateTime DateCreated { get; init; } = DateTime.UtcNow;
    public int CategoryId { get; private set; }
    public Category Category { get; private set; }
    public ICollection<Response> Responses { get; private set; } = new List<Response>();

    [UsedImplicitly]
    private Advertisement() { }

    public Advertisement(string name, string description, int userId, int categoryId)
    {
        Validate(name, description, userId, categoryId);

        Name = name;
        Description = description;
        UserId = userId;
        CategoryId = categoryId;
    }

    public OperationResult Update(string name, string description, int userId, int serviceOfferRequestCategoryId)
    {
        if (userId != UserId)
            return OperationResult.Error("Has no access to this advertisement");

        Validate(name, description, userId, serviceOfferRequestCategoryId);

        Name = name;
        Description = description;
        CategoryId = serviceOfferRequestCategoryId;
        return OperationResult.Success();
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