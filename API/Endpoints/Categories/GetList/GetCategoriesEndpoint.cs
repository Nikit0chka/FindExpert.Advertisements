using API.Endpoints.Advertisements.Get;
using API.Endpoints.Base;
using API.Endpoints.Categories.Dto;
using Application.CQRS.Categories.GetList;
using MediatR;

namespace API.Endpoints.Categories.GetList;

public class GetCategoriesEndpoint(IMediator mediator, GetAdvertisementErrorMapper errorMapper):BaseEndpointWithoutRequest<GetCategoriesResponse>
{
    public override void Configure() { Get(BaseEndpointsRoute.CategoriesRoute); }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetCategoriesListQuery(), cancellationToken);

        await SendResponseByResult(result,
                                   static categoryList => new(categoryList.Select(static category => new CategoryInfo(category.Id, category.Name)).ToList()),
                                   errorMapper,
                                   cancellationToken);
    }
}