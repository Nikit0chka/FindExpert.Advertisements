using API.Contracts;
using Application.CQRS.Responses.Delete;

namespace API.Endpoints.Responses.Delete;

public sealed class DeleteResponseErrorMapper:IErrorMapper
{
    public int GetStatusCode(string? errorCode) => errorCode switch
    {
        DeleteResponseErrorCodes.ResponseNotFound => StatusCodes.Status404NotFound,
        DeleteResponseErrorCodes.Forbidden => StatusCodes.Status403Forbidden,
        _ => StatusCodes.Status500InternalServerError
    };

    public string GetTitle(string? errorCode) => errorCode switch
    {
        DeleteResponseErrorCodes.ResponseNotFound => "Response Not Found",
        DeleteResponseErrorCodes.Forbidden => "Forbidden",
        _ => "Internal Server Error"
    };

    public string GetDetail(string? errorCode) => errorCode switch
    {
        DeleteResponseErrorCodes.ResponseNotFound => "Response by provided id not found",
        DeleteResponseErrorCodes.Forbidden => "No access to delete this response",
        _ => "An error occurred"
    };
}