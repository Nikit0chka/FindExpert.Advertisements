using API.Contracts;
using Domain.Utils;
using FastEndpoints;

namespace API.Endpoints.Base;

public abstract class BaseEndpoint<TRequest>:Endpoint<TRequest, ApiResponse>
    where TRequest : notnull
{
    private protected new Task SendOkAsync(CancellationToken cancellationToken) => base.SendOkAsync(ApiResponse.Success(), cancellationToken);

    private Task SendErrorAsync(
        string? errorCode,
        IErrorMapper errorMapper,
        CancellationToken cancellationToken)
    {
        var problemDetails = new BaseProblemDetails(errorCode, errorMapper, HttpContext);
        return SendAsync(ApiResponse.Fail(problemDetails), problemDetails.Status, cancellationToken);
    }

    private protected Task SendResponseByResult(OperationResult operationResult, IErrorMapper errorMapper, CancellationToken cancellationToken) =>
        operationResult.IsSuccess? SendOkAsync(ApiResponse.Success(), cancellationToken) : SendErrorAsync(operationResult.ErrorCode, errorMapper, cancellationToken);
}

public abstract class BaseEndpoint<TRequest, TResponse>:Endpoint<TRequest, ApiResponse<TResponse>>
    where TRequest : notnull
{
    private Task SendOkAsync(TResponse response, CancellationToken cancellationToken) => base.SendOkAsync(ApiResponse<TResponse>.Success(response), cancellationToken);

    private Task SendErrorAsync(
        string? errorCode,
        IErrorMapper errorMapper,
        CancellationToken cancellationToken)
    {
        var problemDetails = new BaseProblemDetails(errorCode, errorMapper, HttpContext);
        return SendAsync(ApiResponse<TResponse>.Fail(problemDetails), problemDetails.Status, cancellationToken);
    }

    private protected Task SendResponseByResult<TDomain>(
        OperationResult<TDomain> operationResult,
        Func<TDomain, TResponse> mapper,
        IErrorMapper errorMapper,
        CancellationToken cancellationToken) => operationResult.IsSuccess
        ? SendOkAsync(mapper(operationResult.Data!), cancellationToken)
        : SendErrorAsync(operationResult.ErrorCode, errorMapper, cancellationToken);
}

public abstract class BaseEndpointWithoutRequest<TResponse>:EndpointWithoutRequest<ApiResponse<TResponse>>
{
    private protected Task SendOkAsync(TResponse response, CancellationToken cancellationToken) => SendAsync(ApiResponse<TResponse>.Success(response), 200, cancellationToken);

    private protected Task SendErrorAsync(
        string? errorCode,
        IErrorMapper errorMapper,
        CancellationToken cancellationToken)
    {
        var problemDetails = new BaseProblemDetails(errorCode, errorMapper, HttpContext);
        return SendAsync(ApiResponse<TResponse>.Fail(problemDetails), problemDetails.Status, cancellationToken);
    }

    private protected Task SendResponseByResult<TDomain>(
        OperationResult<TDomain> operationResult,
        Func<TDomain, TResponse> mapper,
        IErrorMapper errorMapper,
        CancellationToken cancellationToken) => operationResult.IsSuccess
        ? SendOkAsync(mapper(operationResult.Data!), cancellationToken)
        : SendErrorAsync(operationResult.ErrorCode, errorMapper, cancellationToken);
}

public abstract class BaseEndpointWithoutRequest : EndpointWithoutRequest<ApiResponse>
{
    private protected Task SendOkAsync(CancellationToken cancellationToken) 
        => SendAsync(ApiResponse.Success(), 200, cancellationToken);

    private protected Task SendErrorAsync(
        string? errorCode,
        IErrorMapper errorMapper,
        CancellationToken cancellationToken)
    {
        var problemDetails = new BaseProblemDetails(errorCode, errorMapper, HttpContext);
        return SendAsync(ApiResponse.Fail(problemDetails), problemDetails.Status, cancellationToken);
    }

    private protected Task SendResponseByResult(
        OperationResult operationResult,
        IErrorMapper errorMapper,
        CancellationToken cancellationToken) 
        => operationResult.IsSuccess
            ? SendOkAsync(cancellationToken)
            : SendErrorAsync(operationResult.ErrorCode, errorMapper, cancellationToken);
}