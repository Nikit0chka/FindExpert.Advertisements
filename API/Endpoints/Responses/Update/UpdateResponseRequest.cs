using FastEndpoints;

namespace API.Endpoints.Responses.Update;

public sealed record UpdateResponseRequest([property: RouteParam] int Id, string Comment);