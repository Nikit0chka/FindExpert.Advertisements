using Ardalis.SharedKernel;
using Domain.Utils;

namespace Application.CQRS.Advertisements.Update;

public readonly record struct UpdateAdvertisementCommand(int Id, string Name, string Description, int AdvertisementCategoryId, int UserId):ICommand<OperationResult>;