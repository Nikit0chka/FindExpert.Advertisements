using Ardalis.Result;
using FastEndpoints;
using FluentValidation.Results;
using IResult = Ardalis.Result.IResult;

namespace API.Extensions;

internal static class ResultsExtensions
{
    internal static Task SendResponse<TResult, TResponse>(
        this IEndpoint ep,
        TResult result,
        Func<TResult, TResponse> mapper,
        CancellationToken cancellationToken) where TResult : IResult
    {
        switch (result.Status)
        {
            case ResultStatus.Ok:
                return ep.HttpContext.Response.SendAsync(mapper(result), cancellation: cancellationToken);

            case ResultStatus.Created:
                return ep.HttpContext.Response.SendCreatedAsync(mapper(result), cancellationToken);

            case ResultStatus.NoContent:
                return ep.HttpContext.Response.SendNoContentAsync(cancellationToken);

            case ResultStatus.NotFound:
                return ep.HttpContext.Response.SendNotFoundAsync(result.Errors.FirstOrDefault() ?? "Resource not found.", cancellationToken);

            case ResultStatus.Invalid:
                foreach (var error in result.ValidationErrors)
                {
                    ep.ValidationFailures.Add(new(error.Identifier, error.ErrorMessage));
                }

                return ep.HttpContext.Response.SendErrorsAsync(ep.ValidationFailures, cancellationToken);

            case ResultStatus.Forbidden:
                return ep.HttpContext.Response.SendForbiddenAsync(cancellationToken);

            case ResultStatus.Unauthorized:
                return ep.HttpContext.Response.SendUnauthorizedAsync(cancellationToken);

            case ResultStatus.Conflict:
                return ep.HttpContext.Response.SendConflictAsync(cancellationToken);

            case ResultStatus.CriticalError:
                return ep.HttpContext.Response.SendCriticalErrorAsync(cancellationToken);

            case ResultStatus.Unavailable:
                return ep.HttpContext.Response.SendUnavailableAsync(cancellationToken);

            case ResultStatus.Error:
                return ep.HttpContext.Response.SendErrorAsync(result.Errors.FirstOrDefault() ?? "An error occurred while processing the request.", cancellationToken);

            default:
                return ep.HttpContext.Response.SendErrorAsync("An unexpected result status was encountered.", cancellationToken);
        }
    }

    internal static Task SendResponse<TResult>(
        this IEndpoint ep,
        TResult result,
        CancellationToken cancellationToken) where TResult : IResult
    {
        switch (result.Status)
        {
            case ResultStatus.Ok:
            case ResultStatus.Created:
                return ep.HttpContext.Response.SendAsync("", cancellation: cancellationToken);

            case ResultStatus.NoContent:
                return ep.HttpContext.Response.SendNoContentAsync(cancellationToken);

            case ResultStatus.NotFound:
                return ep.HttpContext.Response.SendNotFoundAsync(result.Errors.FirstOrDefault() ?? "Resource not found.", cancellationToken);

            case ResultStatus.Invalid:
                foreach (var error in result.ValidationErrors)
                {
                    ep.ValidationFailures.Add(new(error.Identifier, error.ErrorMessage));
                }

                return ep.HttpContext.Response.SendErrorsAsync(ep.ValidationFailures, cancellationToken);

            case ResultStatus.Forbidden:
                return ep.HttpContext.Response.SendForbiddenAsync(cancellationToken);

            case ResultStatus.Unauthorized:
                return ep.HttpContext.Response.SendUnauthorizedAsync(cancellationToken);

            case ResultStatus.Conflict:
                return ep.HttpContext.Response.SendConflictAsync(cancellationToken);

            case ResultStatus.CriticalError:
                return ep.HttpContext.Response.SendCriticalErrorAsync(cancellationToken);

            case ResultStatus.Unavailable:
                return ep.HttpContext.Response.SendUnavailableAsync(cancellationToken);

            case ResultStatus.Error:
                return ep.HttpContext.Response.SendErrorAsync(result.Errors.FirstOrDefault() ?? "An error occurred while processing the request.", cancellationToken);

            default:
                return ep.HttpContext.Response.SendErrorAsync("An unexpected result status was encountered.", cancellationToken);
        }
    }

    private static Task SendCreatedAsync<TResponse>(this HttpResponse response, TResponse data, CancellationToken cancellationToken)
    {
        response.StatusCode = StatusCodes.Status201Created;
        return response.WriteAsJsonAsync(data, cancellationToken);
    }

    private static Task SendNoContentAsync(this HttpResponse response, CancellationToken cancellationToken)
    {
        response.StatusCode = StatusCodes.Status204NoContent;
        return response.WriteAsync(string.Empty, cancellationToken);
    }

    private static Task SendNotFoundAsync(this HttpResponse response, string error, CancellationToken cancellationToken)
    {
        response.StatusCode = StatusCodes.Status404NotFound;
        return response.WriteAsync(error, cancellationToken);
    }

    private static Task SendForbiddenAsync(this HttpResponse response, CancellationToken cancellationToken)
    {
        response.StatusCode = StatusCodes.Status403Forbidden;
        return response.WriteAsync("Forbidden.", cancellationToken);
    }

    private static Task SendUnauthorizedAsync(this HttpResponse response, CancellationToken cancellationToken)
    {
        response.StatusCode = StatusCodes.Status401Unauthorized;
        return response.WriteAsync("Unauthorized.", cancellationToken);
    }

    private static Task SendConflictAsync(this HttpResponse response, CancellationToken cancellationToken)
    {
        response.StatusCode = StatusCodes.Status409Conflict;
        return response.WriteAsync("Conflict occurred.", cancellationToken);
    }

    private static Task SendCriticalErrorAsync(this HttpResponse response, CancellationToken cancellationToken)
    {
        response.StatusCode = StatusCodes.Status500InternalServerError;
        return response.WriteAsync("A critical error occurred.", cancellationToken);
    }

    private static Task SendUnavailableAsync(this HttpResponse response, CancellationToken cancellationToken)
    {
        response.StatusCode = StatusCodes.Status503ServiceUnavailable;
        return response.WriteAsync("Service is unavailable.", cancellationToken);
    }

    private static Task SendErrorAsync(this HttpResponse response, string errorMessage, CancellationToken cancellationToken)
    {
        response.StatusCode = StatusCodes.Status500InternalServerError;
        return response.WriteAsync(errorMessage, cancellationToken);
    }

    private static Task SendErrorsAsync(this HttpResponse response, IEnumerable<ValidationFailure> validationFailures, CancellationToken cancellationToken)
    {
        response.StatusCode = StatusCodes.Status400BadRequest;

        var errors = validationFailures
            .Select(static validationFailure => new { Property = validationFailure.PropertyName, Message = validationFailure.ErrorMessage })
            .ToList();

        return response.WriteAsJsonAsync(errors, cancellationToken);
    }
}