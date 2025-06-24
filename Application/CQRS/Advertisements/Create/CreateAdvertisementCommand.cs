using Ardalis.SharedKernel;
using Domain.Utils;

namespace Application.CQRS.Advertisements.Create;

public readonly record struct CreateAdvertisementCommand(string Name, string Description, int AdvertisementCategoryId, int UserId):ICommand<OperationResult>;