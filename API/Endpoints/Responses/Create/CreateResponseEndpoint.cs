using API.Endpoints.Advertisements.Update;
using API.Endpoints.Base;
using API.Helpers;
using Application.CQRS.Responses.Create;
using MediatR;

namespace API.Endpoints.Responses.Create;

public class CreateResponseEndpoint(IMediator mediator, UpdateAdvertisementErrorMapper errorMapper):BaseEndpoint<CreateResponseRequest>
{
    public override void Configure()
    {
        Post(BaseEndpointsRoute.ResponseRoute);
    }

    public override async Task HandleAsync(CreateResponseRequest request, CancellationToken cancellationToken)
    {
        var userId = HeaderHelper.GetUserIdFromHeader(HttpContext);

        var result = await mediator.Send(new CreateResponseCommand(request.Comment, request.AdvertisementId, userId!.Value), cancellationToken);

        await SendResponseByResult(result, errorMapper, cancellationToken);
    }
}