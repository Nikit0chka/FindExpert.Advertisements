using API.Extensions;
using API.Helpers;
using Application.CQRS.Advertisements.CreateAdvertisement;
using FastEndpoints;
using MediatR;

namespace API.Endpoints.Advertisements.CreateAdvertisement;

public class CreateAdvertisementEndpoint(IMediator mediator):Endpoint<CreateAdvertisementRequest>
{
    public override void Configure()
    {
        Post("/api/advertisement");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateAdvertisementRequest request, CancellationToken cancellationToken)
    {
        var userIdResult = HeaderHelper.GetUserIdFromHeader(HttpContext);

        if (!userIdResult.IsSuccess)
        {
            await this.SendResponse(userIdResult, cancellationToken);
            return;
        }

        var result = await mediator.Send(new CreateAdvertisementCommand(request.Name, request.Description, request.AdvertisementCategoryId, userIdResult.Value), cancellationToken);

        await this.SendResponse(result, cancellationToken);
    }
}