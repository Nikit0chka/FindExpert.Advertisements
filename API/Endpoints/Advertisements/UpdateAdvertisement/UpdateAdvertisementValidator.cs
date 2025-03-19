using Domain.AggregateModels.AdvertisementAggregate;
using FastEndpoints;
using FluentValidation;

namespace API.Endpoints.Advertisements.UpdateAdvertisement;

internal class UpdateAdvertisementValidator:Validator<UpdateAdvertisementRequest>
{
    public UpdateAdvertisementValidator()
    {
        RuleFor(static request => request.Name)
            .NotEmpty().WithMessage(AdvertisementValidationMessages.NameIsRequired)
            .MinimumLength(AdvertisementConstants.MinNameLength).WithMessage(AdvertisementValidationMessages.NameLengthIsOutOfRange)
            .MaximumLength(AdvertisementConstants.MaxNameLength).WithMessage(AdvertisementValidationMessages.NameLengthIsOutOfRange);

        RuleFor(static request => request.Description)
            .NotEmpty().WithMessage(AdvertisementValidationMessages.DescriptionIsRequired)
            .MinimumLength(AdvertisementConstants.MinDescriptionLength).WithMessage(AdvertisementValidationMessages.DescriptionLengthIsOutOfRange)
            .MaximumLength(AdvertisementConstants.MaxDescriptionLength).WithMessage(AdvertisementValidationMessages.DescriptionLengthIsOutOfRange);

        RuleFor(static request => request.AdvertisementCategoryId)
            .GreaterThan(0).WithMessage(AdvertisementValidationMessages.AdvertisementCategoryIdMustBeGreaterThanZero);

        RuleFor(static request => request.Id)
            .GreaterThan(0).WithMessage("Advertisement Id must be greater than 0");

    }
}