using API.Endpoints.Advertisements.Delete;
using FastEndpoints;
using FluentValidation;

namespace API.Endpoints.Responses.Delete;

internal class DeleteResponseValidator:Validator<DeleteResponseRequest>
{
    public DeleteResponseValidator() { RuleFor(static request => request.Id).GreaterThan(0).WithMessage("Id must be greater than 0"); }
}