using FastEndpoints;
using JetBrains.Annotations;

namespace API.Endpoints.Advertisements.Get;

[UsedImplicitly]
public sealed record GetAdvertisementRequest([property: RouteParam] int Id, [property: QueryParam] bool IncludeResponses);