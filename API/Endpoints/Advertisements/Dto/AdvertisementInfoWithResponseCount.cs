using JetBrains.Annotations;

namespace API.Endpoints.Advertisements.Dto;

[UsedImplicitly]
public readonly record struct AdvertisementInfoWithResponseCount(int Id, string Name, string CategoryName, string Description, int ResponseCount);
