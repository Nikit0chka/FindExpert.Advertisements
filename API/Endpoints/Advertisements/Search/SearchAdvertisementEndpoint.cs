using API.Endpoints.Advertisements.Dto;
using API.Endpoints.Base;
using Application.CQRS.Advertisements.Search;
using MediatR;

namespace API.Endpoints.Advertisements.Search;

public class SearchAdvertisementEndpoint(IMediator mediator, SearchAdvertisementErrorMapper errorMapper):BaseEndpoint<SearchAdvertisementRequest, SearchAdvertisementResponse>
{
    public override void Configure() { Get(BaseEndpointsRoute.AdvertisementRoute + "/search"); }

    public override async Task HandleAsync(SearchAdvertisementRequest req, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new SearchAdvertisementQuery(req.CategoryId, req.SearchText), cancellationToken);

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