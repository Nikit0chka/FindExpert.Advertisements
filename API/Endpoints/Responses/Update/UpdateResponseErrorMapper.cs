using API.Contracts;
using Application.CQRS.Advertisements.Update;
using Application.CQRS.Responses.Update;

namespace API.Endpoints.Responses.Update;

public sealed class UpdateResponseErrorMapper:IErrorMapper
{
    public int GetStatusCode(string? errorCode) => errorCode switch
    {
        UpdateResponseErrorCodes.ResponseNotFound => StatusCodes.Status404NotFound,
        UpdateResponseErrorCodes.Forbidden => StatusCodes.Status403Forbidden,
        _ => StatusCodes.Status500InternalServerError
    };

    public string GetTitle(string? errorCode) => errorCode switch
    {
        UpdateResponseErrorCodes.ResponseNotFound => "Response Not Found",
        UpdateResponseErrorCodes.Forbidden => "Forbidden",
        _ => "Internal Server Error"
    };

    public string GetDetail(string? errorCode) => errorCode switch
    {
        UpdateResponseErrorCodes.ResponseNotFound => "Response by provided id not found",
        UpdateResponseErrorCodes.Forbidden => "No access to this response",
        _ => "An error occurred"
    };
}