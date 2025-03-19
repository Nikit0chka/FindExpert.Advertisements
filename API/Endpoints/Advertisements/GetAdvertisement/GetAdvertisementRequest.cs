using FastEndpoints;

namespace API.Endpoints.Advertisements.GetAdvertisement;

public sealed record GetAdvertisementRequest([property: RouteParam] int Id);