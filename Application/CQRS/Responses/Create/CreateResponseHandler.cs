using Ardalis.SharedKernel;
using Domain.AggregateModels.AdvertisementAggregate;
using Domain.AggregateModels.ResponseAggregate;
using Domain.Utils;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Responses.Create;

internal sealed class CreateResponseHandler(ILogger<CreateResponseHandler> logger, IReadRepository<Advertisement> advertisementRepository, IRepository<Response> responseRepository):ICommandHandler<CreateResponseCommand, OperationResult>
{
    public async Task<OperationResult> Handle(CreateResponseCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling {Command} for UserId: {UserId}, AdvertisementId: {AdvertisementId}", nameof(CreateResponseCommand), request.UserId, request.AdvertisementId);

        try
        {
            //TODO: Запретить добавлять отзыв дважды
            var advertisement = await advertisementRepository.GetByIdAsync(request.AdvertisementId, cancellationToken);

            if (advertisement == null)
            {
                throw new InvalidOperationException("AdvertisementId cannot be equal to AdvertisementId.");
                //TODO: переделать под ошибку
            }

            var response = new Response(request.Comment, request.UserId, request.AdvertisementId, advertisement.UserId);

            await responseRepository.AddAsync(response, cancellationToken);

            logger.LogInformation("{Command} handled successful. Response added", nameof(CreateResponseCommand));

            return OperationResult.Success();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while handling {Command}, UserId: {UserId}, AdvertisementId: {AdvertisementId}", nameof(CreateResponseCommand), request.UserId, request.AdvertisementId);
            return OperationResult.Error();
        }
    }
}