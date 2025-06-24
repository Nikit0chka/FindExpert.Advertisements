using API.Endpoints.Base;
using API.Helpers;
using Application.CQRS.Advertisements.Update;
using MediatR;

namespace API.Endpoints.Advertisements.Update;

public class UpdateAdvertisementEndpoint(IMediator mediator, UpdateAdvertisementErrorMapper errorMapper):BaseEndpoint<UpdateAdvertisementRequest>
{
    public override void Configure()
    {
        Put(BaseEndpointsRoute.AdvertisementRoute + "/{Id}");
    }

    public override async Task HandleAsync(UpdateAdvertisementRequest request, CancellationToken cancellationToken)
    {
        var userId = HeaderHelper.GetUserIdFromHeader(HttpContext);

        var result = await mediator.Send(new UpdateAdvertisementCommand(request.Id, request.Name, request.Description, request.AdvertisementCategoryId, userId!.Value), cancellationToken);

        await SendResponseByResult(result, errorMapper, cancellationToken);
    }
}