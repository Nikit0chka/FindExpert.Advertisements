using Application.CQRS.Advertisements.Update;
using Ardalis.SharedKernel;
using Domain.AggregateModels.AdvertisementAggregate;
using Domain.AggregateModels.ResponseAggregate;
using Domain.Utils;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Responses.Update;

internal sealed class UpdateResponseHandler(ILogger<UpdateResponseHandler> logger, IRepository<Response> responseRepository):ICommandHandler<UpdateResponseCommand, OperationResult>
{
    public async Task<OperationResult> Handle(UpdateResponseCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling {Command} ResponseId: {ResponseId} UserId: {UserId}", nameof(UpdateResponseCommand), request.Id, request.UserId);

        try
        {
            var response = await responseRepository.GetByIdAsync(request.Id, cancellationToken);

            if (response is null)
            {
                logger.LogWarning("Response with Id: {Id} not found", request.Id);
                return OperationResult.Error(UpdateResponseErrorCodes.ResponseNotFound);
            }

            if (response.UserId != request.UserId)
            {
                logger.LogWarning("An attempt to delete advertisement without access, UserId: {UserId}, ResponseId: {ResponseId}", request.UserId, response.Id);
                return OperationResult.Error(UpdateResponseErrorCodes.Forbidden);
            }

            // var updateResult = response.Update(request.Name, request.Description, request.UserId, request.AdvertisementCategoryId);
            //
            // if (!updateResult.IsSuccess)
            //     return updateResult;
            //TODO: возможно действительно нужна доменная логика обновления

            response.Comment = request.Comment;

            await responseRepository.UpdateAsync(response, cancellationToken);

            logger.LogInformation("{Command} handled successful. Response updated. UserId: {UserId}, ResponseId: {ResponseId}", nameof(UpdateResponseCommand), request.UserId, response.Id);

            return OperationResult.Success();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while handling {Command}, for user with Id: {UserId}, AdvertisementId: {AdvertisementId}", nameof(UpdateResponseCommand), request.UserId, request.Id);
            return OperationResult.Error();
        }
    }
}