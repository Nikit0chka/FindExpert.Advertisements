using API.Endpoints.Advertisements.GetMy;
using API.Endpoints.Base;
using API.Endpoints.Responses.Dto;
using API.Helpers;
using Application.CQRS.Responses.GetMy;
using MediatR;

namespace API.Endpoints.Responses.GetMy;

public class GetMyResponseEndpoint(IMediator mediator, GetMyAdvertisementErrorMapper errorMapper):BaseEndpointWithoutRequest<GetMyResponseResponse>
{
    public override void Configure() { Get(BaseEndpointsRoute.ResponseRoute + "/my"); }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var userId = HeaderHelper.GetUserIdFromHeader(HttpContext);

        var result = await mediator.Send(new GetMyResponseQuery(userId!.Value), cancellationToken);

        await SendResponseByResult(result,
                                   static responseList => new(
                                                              responseList.Select(static response =>
                                                                                      new ResponseInfo(response.Id,
                                                                                                       response.Comment,
                                                                                                       response.UserId,
                                                                                                       response.AdvertisementId,
                                                                                                       response.DateCreated)).ToList()),
                                   errorMapper,
                                   cancellationToken);
    }
}