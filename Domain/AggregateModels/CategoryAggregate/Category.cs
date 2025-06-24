using Ardalis.GuardClauses;
using Ardalis.SharedKernel;
using Domain.AggregateModels.AdvertisementAggregate;
using Domain.Extensions;
using JetBrains.Annotations;

namespace Domain.AggregateModels.CategoryAggregate;

/// <inheritdoc cref="Ardalis.SharedKernel.IAggregateRoot" />
/// <summary>
/// Advertisement category entity
/// </summary>
public sealed class Category:EntityBase, IAggregateRoot
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
    public Category? ParentCategory { get; private set; }

    /// <summary>
    /// Advertisement category children categories
    /// </summary>
    public ICollection<Category> ChildCategories { get; private set; }

    /// <summary>
    /// Advertisement category advertisements
    /// </summary>
    public ICollection<Advertisement> Advertisements { get; private set; }

    /// <summary>
    /// Ef core constructor
    /// </summary>
    [UsedImplicitly]
    private Category() { }

    /// <summary>
    /// Constructor with validation
    /// </summary>
    /// <param name="name">Advertisement category name</param>
    /// <param name="parentCategoryId">Advertisement category parent category id</param>
    public Category(string name, int? parentCategoryId)
    {
        Guard.Against.NullOrEmpty(name, nameof(name), CategoryValidationMessages.NameIsRequired);
        GuardAgainstExtensions.StringLengthOutOfRange(name, CategoryConstants.MinNameLength, CategoryConstants.MaxNameLength, nameof(name), CategoryValidationMessages.NameLengthOutOfRange);

        if (parentCategoryId.HasValue)
            Guard.Against.NegativeOrZero(parentCategoryId.Value, nameof(parentCategoryId), CategoryValidationMessages.ParentCategoryIdMustBeGreaterThanZero);

        Name = name;
        ParentCategoryId = parentCategoryId;
    }
}