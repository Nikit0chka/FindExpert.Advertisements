using API.Endpoints.Advertisements.Dto;

namespace API.Endpoints.Advertisements.GetMy;

public readonly record struct GetMyAdvertisementResponse(IReadOnlyCollection<AdvertisementInfoWithResponseCount> Advertisements);