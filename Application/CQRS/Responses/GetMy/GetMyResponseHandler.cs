using Application.CQRS.Advertisements.Get;
using Ardalis.SharedKernel;
using Domain.AggregateModels.ResponseAggregate;
using Domain.AggregateModels.ResponseAggregate.Specification;
using Domain.Utils;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Responses.GetMy;

internal sealed class GetMyResponseHandler(IReadRepository<Response> responseRepository, ILogger<GetAdvertisementHandler> logger):IQueryHandler<GetMyResponseQuery, OperationResult<IReadOnlyCollection<Response>>>
{
    public async Task<OperationResult<IReadOnlyCollection<Response>>> Handle(GetMyResponseQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling {Query} for UserId: {UserId}", nameof(GetMyResponseQuery), request.UserId);

        try
        {
            var responses = await responseRepository.ListAsync(new ResponsesByUserIdSpecification(request.UserId), cancellationToken);

            logger.LogInformation("{Query} handled successful", nameof(GetMyResponseQuery));

            return OperationResult<IReadOnlyCollection<Response>>.Success(responses);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while handling {Query}, for UserId: {UserId}", nameof(GetMyResponseQuery), request.UserId);
            return OperationResult<IReadOnlyCollection<Response>>.Error();
        }
    }
}