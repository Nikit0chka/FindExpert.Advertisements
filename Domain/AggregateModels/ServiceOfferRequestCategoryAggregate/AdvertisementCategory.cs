using Ardalis.GuardClauses;
using Ardalis.SharedKernel;
using Domain.AggregateModels.AdvertisementAggregate;
using Domain.Extensions;

namespace Domain.AggregateModels.ServiceOfferRequestCategoryAggregate;

/// <inheritdoc cref="Ardalis.SharedKernel.IAggregateRoot" />
/// <summary>
/// Advertisement category entity
/// </summary>
public class AdvertisementCategory:EntityBase, IAggregateRoot
{
    /// <summary>
    /// Advertisement category name
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Advertisement category parent category id
    /// </summary>
    public int? ParentCategoryId { get; private set; }

    /// <summary>
    /// Advertisement category parent category navigation property
    /// </summary>
    public virtual AdvertisementCategory? ParentCategory { get; private set; }

    /// <summary>
    /// Advertisement category childs categories
    /// </summary>
    public virtual ICollection<AdvertisementCategory> ChildCategories { get; private set; }

    /// <summary>
    /// Advertisement category advertisements
    /// </summary>
    public virtual ICollection<Advertisement> Advertisements { get; private set; }

    /// <summary>
    /// Ef core constructor
    /// </summary>
    protected AdvertisementCategory() { }

    /// <summary>
    /// Constructor with validation
    /// </summary>
    /// <param name="name">Advertisement category name</param>
    /// <param name="parentCategoryId">Advertisement category parent category id</param>
    public AdvertisementCategory(string name, int? parentCategoryId)
    {
        Guard.Against.NullOrEmpty(name, nameof(name), AdvertisementCategoryValidationMessages.NameIsRequired);
        GuardAgainstExtensions.StringLengthOutOfRange(name, AdvertisementCategoryConstants.MinNameLength, AdvertisementCategoryConstants.MaxNameLength, nameof(name), AdvertisementCategoryValidationMessages.NameLengthOutOfRange);

        if (parentCategoryId.HasValue)
            Guard.Against.NegativeOrZero(parentCategoryId.Value, nameof(parentCategoryId), AdvertisementCategoryValidationMessages.ParentCategoryIdMustBeGreaterThanZero);

        Name = name;
        ParentCategoryId = parentCategoryId;
    }
}