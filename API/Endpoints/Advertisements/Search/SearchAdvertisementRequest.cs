using FastEndpoints;
using JetBrains.Annotations;

namespace API.Endpoints.Advertisements.Search;

[UsedImplicitly]
public sealed record SearchAdvertisementRequest([property: QueryParam] int CategoryId, [property: QueryParam] string SearchText);