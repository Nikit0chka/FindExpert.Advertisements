using Ardalis.SharedKernel;
using Domain.Utils;

namespace Application.CQRS.Advertisements.Get;

public readonly record struct GetAdvertisementQuery(int Id, bool IncludeResponses, int UserId):IQuery<OperationResult<GetAdvertisementResult>>;