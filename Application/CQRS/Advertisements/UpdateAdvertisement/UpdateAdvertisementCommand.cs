using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Application.CQRS.Advertisements.UpdateAdvertisement;

public readonly record struct UpdateAdvertisementCommand(int Id, string Name, string Description, int AdvertisementCategoryId, int UserId):ICommand<Result>;