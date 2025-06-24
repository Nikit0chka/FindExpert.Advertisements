using Ardalis.SharedKernel;
using Domain.AggregateModels.ResponseAggregate;
using Domain.Utils;

namespace Application.CQRS.Responses.GetMy;

public readonly record struct GetMyResponseQuery(int UserId):IQuery<OperationResult<IReadOnlyCollection<Response>>>;