using Ardalis.SharedKernel;
using Domain.AggregateModels.AdvertisementAggregate;
using Domain.Utils;

namespace Application.CQRS.Advertisements.GetMy;

public readonly record struct GetMyAdvertisementsQuery(int UserId):IQuery<OperationResult<IReadOnlyCollection<Advertisement>>>;