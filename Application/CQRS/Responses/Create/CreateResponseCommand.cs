using Ardalis.SharedKernel;
using Domain.Utils;

namespace Application.CQRS.Responses.Create;

public readonly record struct CreateResponseCommand(string Comment, int AdvertisementId, int UserId):ICommand<OperationResult>;