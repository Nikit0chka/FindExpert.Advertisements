using FastEndpoints;

namespace API.Endpoints.Advertisements.UpdateAdvertisement;

public sealed record UpdateAdvertisementRequest([property: RouteParam] int Id, string Name, string Description, int AdvertisementCategoryId);