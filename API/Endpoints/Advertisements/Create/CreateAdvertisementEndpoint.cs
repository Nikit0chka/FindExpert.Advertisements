using API.Endpoints.Base;
using API.Helpers;
using Application.CQRS.Advertisements.Create;
using MediatR;

namespace API.Endpoints.Advertisements.Create;

public class CreateAdvertisementEndpoint(IMediator mediator, CreateAdvertisementErrorMapper errorMapper):BaseEndpoint<CreateAdvertisementRequest>
{
    public override void Configure()
    {
        Post(BaseEndpointsRoute.AdvertisementRoute);
        Description(static routeHandlerBuilder => routeHandlerBuilder.RequireAuthorization());
    }

    public override async Task HandleAsync(CreateAdvertisementRequest request, CancellationToken cancellationToken)
    {
        var userId = HeaderHelper.GetUserIdFromHeader(HttpContext);

        var result = await mediator.Send(new CreateAdvertisementCommand(request.Name, request.Description, request.AdvertisementCategoryId, userId!.Value), cancellationToken);

        await SendResponseByResult(result, errorMapper, cancellationToken);
    }
}