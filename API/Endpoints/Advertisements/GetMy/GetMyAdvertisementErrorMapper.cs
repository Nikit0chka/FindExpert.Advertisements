using API.Contracts;

namespace API.Endpoints.Advertisements.GetMy;

public sealed class GetMyAdvertisementErrorMapper:IErrorMapper
{
    public int GetStatusCode(string? errorCode) => errorCode switch
    {
        _ => StatusCodes.Status500InternalServerError
    };

    public string GetTitle(string? errorCode) => errorCode switch
    {
        _ => "Internal Server Error"
    };

    public string GetDetail(string? errorCode) => errorCode switch
    {
        _ => "An error occurred"
    };
}