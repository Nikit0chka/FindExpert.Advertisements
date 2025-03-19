using API.Extensions;
using API.Helpers;
using Application.CQRS.Advertisements.UpdateAdvertisement;
using FastEndpoints;
using MediatR;

namespace API.Endpoints.Advertisements.UpdateAdvertisement;

public class UpdateAdvertisementEndpoint(IMediator mediator):Endpoint<UpdateAdvertisementRequest>
{
    public override void Configure()
    {
        Put("/api/advertisements/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateAdvertisementRequest request, CancellationToken cancellationToken)
    {
        var userIdResult = HeaderHelper.GetUserIdFromHeader(HttpContext);

        if (!userIdResult.IsSuccess)
        {
            await this.SendResponse(userIdResult, cancellationToken);
            return;
        }

        var result = await mediator.Send(new UpdateAdvertisementCommand(request.Id, request.Name, request.Description, request.AdvertisementCategoryId, userIdResult.Value), cancellationToken);

        await this.SendResponse(result, cancellationToken);
    }
}