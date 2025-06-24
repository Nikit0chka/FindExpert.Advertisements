using API.Endpoints.Advertisements.Dto;
using API.Endpoints.Base;
using API.Helpers;
using Application.CQRS.Advertisements.GetMy;
using MediatR;

namespace API.Endpoints.Advertisements.GetMy;

public class GetMyAdvertisementEndpoint(IMediator mediator, GetMyAdvertisementErrorMapper errorMapper):BaseEndpointWithoutRequest<GetMyAdvertisementResponse>
{
    public override void Configure() { Get(BaseEndpointsRoute.AdvertisementRoute + "/my"); }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var userId = HeaderHelper.GetUserIdFromHeader(HttpContext);

        var result = await mediator.Send(new GetMyAdvertisementsQuery(userId!.Value), cancellationToken);

        await SendResponseByResult(result,
                                   static advertisementList => new(
                                                                   advertisementList.Select(static advertisement =>
                                                                                                new AdvertisementInfoWithResponseCount(advertisement.Id,
                                                                                                                                       advertisement.Name,
                                                                                                                                       advertisement.Category.Name,
                                                                                                                                       advertisement.Description,
                                                                                                                                       advertisement.Responses.Count)).ToList()),
                                   errorMapper,
                                   cancellationToken);
    }
}