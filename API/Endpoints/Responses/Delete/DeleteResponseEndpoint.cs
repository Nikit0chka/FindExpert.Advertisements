using API.Endpoints.Base;
using API.Helpers;
using Application.CQRS.Responses.Delete;
using MediatR;

namespace API.Endpoints.Responses.Delete;

public class DeleteResponseEndpoint(IMediator mediator, DeleteResponseErrorMapper errorMapper):BaseEndpoint<DeleteResponseRequest>
{
    public override void Configure()
    {
        Delete(BaseEndpointsRoute.ResponseRoute + "/{Id}");
    }

    public override async Task HandleAsync(DeleteResponseRequest request, CancellationToken cancellationToken)
    {
        var userId = HeaderHelper.GetUserIdFromHeader(HttpContext);

        var result = await mediator.Send(new DeleteResponseCommand(request.Id, userId!.Value), cancellationToken);

        await SendResponseByResult(result, errorMapper, cancellationToken);
    }
}