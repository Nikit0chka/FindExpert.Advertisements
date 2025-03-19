using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Application.CQRS.Advertisements.CreateAdvertisement;

public readonly record struct CreateAdvertisementCommand(string Name, string Description, int AdvertisementCategoryId, int UserId) : ICommand<Result>;