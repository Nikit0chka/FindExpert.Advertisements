using API.Extensions;
using Application.CQRS.Advertisements.GetAdvertisement;
using FastEndpoints;
using MediatR;

namespace API.Endpoints.Advertisements.GetAdvertisement;

public class GetAdvertisementEndpoint(IMediator mediator):Endpoint<GetAdvertisementRequest, GetAdvertisementResponse>
{
    public override void Configure()
    {
        Get("/api/advertisements/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetAdvertisementRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAdvertisementQuery(request.Id), cancellationToken);

        await this.SendResponse(result, static _ => new {}, cancellationToken);
    }
}