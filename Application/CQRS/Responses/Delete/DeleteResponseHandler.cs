using Ardalis.SharedKernel;
using Domain.AggregateModels.ResponseAggregate;
using Domain.Utils;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Responses.Delete;

internal sealed class DeleteResponseHandler(ILogger<DeleteResponseHandler> logger, IRepository<Response> responseRepository):ICommandHandler<DeleteResponseCommand, OperationResult>
{
    public async Task<OperationResult> Handle(DeleteResponseCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling {Command} for user with Id: {UserId}", nameof(DeleteResponseCommand), request.UserId);

        try
        {
            var response = await responseRepository.GetByIdAsync(request.Id, cancellationToken);

            if (response is null)
            {
                logger.LogWarning("Response with Id: {Id} not found", request.Id);
                return OperationResult.Error(DeleteResponseErrorCodes.ResponseNotFound);
            }

            if (response.UserId != request.UserId)  
            {
                logger.LogWarning("An attempt to delete advertisement without access, UserId: {UserId}, ResponseId: {ResponseId}", request.UserId, response.Id);
                return OperationResult.Error(DeleteResponseErrorCodes.Forbidden);
            }

            await responseRepository.DeleteAsync(response, cancellationToken);

            logger.LogInformation("{Command} handled successful. Advertisement deleted successful. UserId: {UserId}, ResponseId: {ResponseId}", nameof(DeleteResponseCommand), request.UserId, response.Id);

            return OperationResult.Success();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while handling {Command}, for user with Id: {UserId}", nameof(DeleteResponseCommand), request.UserId);
            return OperationResult.Error();
        }
    }
}