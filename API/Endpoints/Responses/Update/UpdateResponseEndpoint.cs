using API.Endpoints.Advertisements.Update;
using API.Endpoints.Base;
using API.Helpers;
using Application.CQRS.Responses.Update;
using MediatR;

namespace API.Endpoints.Responses.Update;

public class UpdateResponseEndpoint(IMediator mediator, UpdateResponseErrorMapper errorMapper):BaseEndpoint<UpdateResponseRequest>
{
    public override void Configure() { Put(BaseEndpointsRoute.ResponseRoute + "/{Id}"); }

    public override async Task HandleAsync(UpdateResponseRequest request, CancellationToken cancellationToken)
    {
        var userId = HeaderHelper.GetUserIdFromHeader(HttpContext);

        var result = await mediator.Send(new UpdateResponseCommand(request.Id, request.Comment, userId!.Value), cancellationToken);

        await SendResponseByResult(result, errorMapper, cancellationToken);
    }
}