using API.Endpoints.Responses.Dto;
using JetBrains.Annotations;

namespace API.Endpoints.Advertisements.Dto;

[UsedImplicitly]
public readonly record struct AdvertisementInfo(int Id, string Name, int CategoryId, string Description, int UserId, bool CurrentUserHasResponded, IReadOnlyCollection<ResponseInfo> Responses);