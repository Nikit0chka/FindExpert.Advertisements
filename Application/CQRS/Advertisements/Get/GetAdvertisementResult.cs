using Domain.AggregateModels.AdvertisementAggregate;

namespace Application.CQRS.Advertisements.Get;

public readonly record struct GetAdvertisementResult(Advertisement Advertisement, bool CurrentUserHasResponded);