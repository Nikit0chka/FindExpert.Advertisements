using Ardalis.SharedKernel;
using Domain.AggregateModels.CategoryAggregate;
using Domain.Utils;

namespace Application.CQRS.Categories.GetList;

public readonly record struct GetCategoriesListQuery:IQuery<OperationResult<IReadOnlyCollection<Category>>>;