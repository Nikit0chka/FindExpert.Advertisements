using FastEndpoints;
using FluentValidation;

namespace API.Endpoints.Advertisements.Delete;

internal class DeleteAdvertisementValidator:Validator<DeleteAdvertisementRequest>
{
    public DeleteAdvertisementValidator()
    {
        RuleFor(static request => request.Id).GreaterThan(0).WithMessage("Id must be greater than 0");
    }
}