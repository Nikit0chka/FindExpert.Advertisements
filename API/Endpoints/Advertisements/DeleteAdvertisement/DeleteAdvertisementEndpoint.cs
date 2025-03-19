using API.Extensions;
using API.Helpers;
using Application.CQRS.Advertisements.DeleteAdvertisement;
using FastEndpoints;
using MediatR;

namespace API.Endpoints.Advertisements.DeleteAdvertisement;

public class DeleteAdvertisementEndpoint(IMediator mediator):Endpoint<DeleteAdvertisementRequest>
{
    public override void Configure() { Delete("/api/advertisement/{Id}"); }

    public override async Task HandleAsync(DeleteAdvertisementRequest request, CancellationToken cancellationToken)
    {
        var userIdResult = HeaderHelper.GetUserIdFromHeader(HttpContext);

        if (!userIdResult.IsSuccess)
        {
            await this.SendResponse(userIdResult, cancellationToken);
            return;
        }

        var result = await mediator.Send(new DeleteAdvertisementCommand(request.Id, userIdResult.Value), cancellationToken);

        await this.SendResponse(result, cancellationToken);
    }
}