using API.Endpoints.Responses.Dto;

namespace API.Endpoints.Responses.GetMy;

public readonly record struct GetMyResponseResponse(IReadOnlyCollection<ResponseInfo> Responses);