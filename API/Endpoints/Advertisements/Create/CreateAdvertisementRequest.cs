namespace API.Endpoints.Advertisements.Create;

public readonly record struct CreateAdvertisementRequest(string Name, string Description, int AdvertisementCategoryId);