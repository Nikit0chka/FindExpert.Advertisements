using FastEndpoints;
using FluentValidation;

namespace API.Endpoints.Advertisements.Get;

internal class GetAdvertisementValidator:Validator<GetAdvertisementRequest>
{
    public GetAdvertisementValidator() { RuleFor(static request => request.Id).GreaterThan(0).WithMessage("Id must be greater than 0"); }
}