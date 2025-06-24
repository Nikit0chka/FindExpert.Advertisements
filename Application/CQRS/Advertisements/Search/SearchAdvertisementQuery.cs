using Ardalis.SharedKernel;
using Domain.AggregateModels.AdvertisementAggregate;
using Domain.Utils;

namespace Application.CQRS.Advertisements.Search;

public readonly record struct SearchAdvertisementQuery(int CategoryId, string SearchText):IQuery<OperationResult<List<Advertisement>>>;