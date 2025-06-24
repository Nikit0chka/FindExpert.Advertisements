using API.Endpoints.Advertisements.Dto;

namespace API.Endpoints.Advertisements.Search;

public readonly record struct SearchAdvertisementResponse(IReadOnlyCollection<AdvertisementInfoWithResponseCount> Advertisements);