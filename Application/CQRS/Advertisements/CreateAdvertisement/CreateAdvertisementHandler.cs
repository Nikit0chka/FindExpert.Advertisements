using Ardalis.Result;
using Ardalis.SharedKernel;
using Domain.AggregateModels.AdvertisementAggregate;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Advertisements.CreateAdvertisement;

internal sealed class CreateAdvertisementHandler(ILogger<CreateAdvertisementHandler> logger, IRepository<Advertisement> advertisementRepository):ICommandHandler<CreateAdvertisementCommand, Result>
{
    public async Task<Result> Handle(CreateAdvertisementCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling {Command} for user with Id: {UserId}", nameof(CreateAdvertisementCommand), request.UserId);

        try
        {
            var advertisement = new Advertisement(request.Name, request.Description, request.UserId, request.AdvertisementCategoryId);

            await advertisementRepository.AddAsync(advertisement, cancellationToken);

            logger.LogInformation("Advertisement with Id: {AdvertisementId} added successful", advertisement.Id);
            logger.LogInformation("{Command} handled successful UserId: {UserId}", nameof(CreateAdvertisementCommand), request.UserId);

            return Result.NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while handling {Command}, for user with Id: {UserId}", nameof(CreateAdvertisementCommand), request.UserId);
            return Result.Error("An error occured while handling your request");
        }
    }
}