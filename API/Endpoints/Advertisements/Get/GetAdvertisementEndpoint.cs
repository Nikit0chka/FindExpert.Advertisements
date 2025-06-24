using API.Endpoints.Advertisements.Dto;
using API.Endpoints.Base;
using API.Endpoints.Responses.Dto;
using API.Helpers;
using Application.CQRS.Advertisements.Get;
using MediatR;

namespace API.Endpoints.Advertisements.Get;

public class GetAdvertisementEndpoint(IMediator mediator, GetAdvertisementErrorMapper errorMapper):BaseEndpoint<GetAdvertisementRequest, AdvertisementInfo>
{
    public override void Configure() { Get(BaseEndpointsRoute.AdvertisementRoute + "/{Id}"); }

    public override async Task HandleAsync(GetAdvertisementRequest request, CancellationToken cancellationToken)
    {
        var userId = HeaderHelper.GetUserIdFromHeader(HttpContext);

        var result = await mediator.Send(new GetAdvertisementQuery(request.Id, request.IncludeResponses, userId!.Value), cancellationToken);

        await SendResponseByResult(result,
                                   static getResult => new(getResult.Advertisement.Id,
                                                           getResult.Advertisement.Name,
                                                           getResult.Advertisement.CategoryId,
                                                           getResult.Advertisement.Description,
                                                           getResult.Advertisement.UserId,
                                                           getResult.CurrentUserHasResponded,
                                                           getResult.Advertisement.Responses.Select(static response => new ResponseInfo(response.Id, response.Comment, response.UserId, response.AdvertisementId, response.DateCreated)).ToList()),
                                   errorMapper,
                                   cancellationToken);
    }
}