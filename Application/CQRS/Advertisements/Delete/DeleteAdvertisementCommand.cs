using Ardalis.SharedKernel;
using Domain.Utils;

namespace Application.CQRS.Advertisements.Delete;

public readonly record struct DeleteAdvertisementCommand(int Id, int UserId):ICommand<OperationResult>;