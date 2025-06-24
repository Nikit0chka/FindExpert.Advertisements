using API.Contracts;
using Application.CQRS.Advertisements.Get;

namespace API.Endpoints.Advertisements.Get;

public sealed class GetAdvertisementErrorMapper:IErrorMapper
{
    public int GetStatusCode(string? errorCode) => errorCode switch
    {
        GetAdvertisementErrorCodes.AdvertisementNotFound => StatusCodes.Status404NotFound,
        _ => StatusCodes.Status500InternalServerError
    };

    public string GetTitle(string? errorCode) => errorCode switch
    {
        GetAdvertisementErrorCodes.AdvertisementNotFound => "Advertisement Not Found",
        _ => "Internal Server Error"
    };

    public string GetDetail(string? errorCode) => errorCode switch
    {
        GetAdvertisementErrorCodes.AdvertisementNotFound => "Advertisement by provided id not found",
        _ => "An error occurred"
    };
}