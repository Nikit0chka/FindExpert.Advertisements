using Ardalis.Result;
using Ardalis.SharedKernel;
using Domain.AggregateModels.AdvertisementAggregate;

namespace Application.CQRS.Advertisements.GetAdvertisement;

public readonly record struct GetAdvertisementQuery(int Id) : IQuery<Result<Advertisement>>;