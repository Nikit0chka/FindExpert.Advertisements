namespace API.Endpoints.Advertisements.CreateAdvertisement;

public readonly record struct CreateAdvertisementRequest(string Name, string Description, int AdvertisementCategoryId);