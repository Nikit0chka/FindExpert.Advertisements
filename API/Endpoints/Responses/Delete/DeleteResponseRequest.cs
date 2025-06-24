using FastEndpoints;
using JetBrains.Annotations;

namespace API.Endpoints.Responses.Delete;

[UsedImplicitly]
public sealed record DeleteResponseRequest([property: RouteParam] int Id);