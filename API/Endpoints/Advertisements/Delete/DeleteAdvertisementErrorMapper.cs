using API.Contracts;
using Application.CQRS.Advertisements.Delete;

namespace API.Endpoints.Advertisements.Delete;

public sealed class DeleteAdvertisementErrorMapper:IErrorMapper
{
    public int GetStatusCode(string? errorCode) => errorCode switch
    {
        DeleteAdvertisementErrorCodes.AdvertisementNotFound => StatusCodes.Status404NotFound,
        DeleteAdvertisementErrorCodes.Forbidden => StatusCodes.Status403Forbidden,
        _ => StatusCodes.Status500InternalServerError
    };

    public string GetTitle(string? errorCode) => errorCode switch
    {
        DeleteAdvertisementErrorCodes.AdvertisementNotFound => "Advertisement Not Found",
        DeleteAdvertisementErrorCodes.Forbidden => "Forbidden",
        _ => "Internal Server Error"
    };

    public string GetDetail(string? errorCode) => errorCode switch
    {
        DeleteAdvertisementErrorCodes.AdvertisementNotFound => "Advertisement by provided id not found",
        DeleteAdvertisementErrorCodes.Forbidden => "No access to delete this advertisement",
        _ => "An error occurred"
    };
}