using API.Contracts;
using Application.CQRS.Advertisements.Update;

namespace API.Endpoints.Advertisements.Update;

public sealed class UpdateAdvertisementErrorMapper:IErrorMapper
{
    public int GetStatusCode(string? errorCode) => errorCode switch
    {
        UpdateAdvertisementErrorCodes.AdvertisementNotFound => StatusCodes.Status404NotFound,
        UpdateAdvertisementErrorCodes.Forbidden => StatusCodes.Status403Forbidden,
        _ => StatusCodes.Status500InternalServerError
    };

    public string GetTitle(string? errorCode) => errorCode switch
    {
        UpdateAdvertisementErrorCodes.AdvertisementNotFound => "Advertisement Not Found",
        UpdateAdvertisementErrorCodes.Forbidden => "Forbidden",
        _ => "Internal Server Error"
    };

    public string GetDetail(string? errorCode) => errorCode switch
    {
        UpdateAdvertisementErrorCodes.AdvertisementNotFound => "Advertisement by provided id not found",
        UpdateAdvertisementErrorCodes.Forbidden => "No access to this advertisement",
        _ => "An error occurred"
    };
}