using FastEndpoints;
using JetBrains.Annotations;

namespace API.Endpoints.Advertisements.Delete;

[UsedImplicitly]
public sealed record DeleteAdvertisementRequest([property: RouteParam] int Id);