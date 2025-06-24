using Domain.AggregateModels.ResponseAggregate;
using FastEndpoints;
using FluentValidation;

namespace API.Endpoints.Responses.Create;

internal class CreateResponseValidator:Validator<CreateResponseRequest>
{
    public CreateResponseValidator()
    {
        RuleFor(static request => request.Comment).NotEmpty().WithMessage(ResponseValidationMessages.CommentIsRequired)
            .MinimumLength(ResponseConstants.MinCommentLength).WithMessage(ResponseValidationMessages.CommentLengthIsOutOfRange)
            .MaximumLength(ResponseConstants.MaxCommentLength).WithMessage(ResponseValidationMessages.CommentLengthIsOutOfRange);

        RuleFor(static request => request.AdvertisementId)
            .GreaterThan(0).WithMessage(ResponseValidationMessages.AdvertisementIdMustBeGreaterThanZero);
    }
}