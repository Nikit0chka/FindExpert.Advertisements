namespace API.Endpoints.Responses.Dto;

public readonly record struct ResponseInfo(int Id, string Comment, int UserId, int AdvertisementId, DateTime ResponseDate);