using API.Endpoints.Base;
using API.Helpers;
using Application.CQRS.Advertisements.Delete;
using MediatR;

namespace API.Endpoints.Advertisements.Delete;

public class DeleteAdvertisementEndpoint(IMediator mediator, DeleteAdvertisementErrorMapper errorMapper):BaseEndpoint<DeleteAdvertisementRequest>
{
    public override void Configure()
    {
        Delete(BaseEndpointsRoute.AdvertisementRoute + "/{Id}");
    }

    public override async Task HandleAsync(DeleteAdvertisementRequest request, CancellationToken cancellationToken)
    {
        var userId = HeaderHelper.GetUserIdFromHeader(HttpContext);

        var result = await mediator.Send(new DeleteAdvertisementCommand(request.Id, userId!.Value), cancellationToken);

        await SendResponseByResult(result, errorMapper, cancellationToken);
    }
}