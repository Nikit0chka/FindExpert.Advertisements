using FastEndpoints;

namespace API.Endpoints.Advertisements.DeleteAdvertisement;

public sealed record DeleteAdvertisementRequest([property: RouteParam] int Id);