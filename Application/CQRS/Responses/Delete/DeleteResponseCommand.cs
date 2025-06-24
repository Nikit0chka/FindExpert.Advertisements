using Ardalis.SharedKernel;
using Domain.Utils;

namespace Application.CQRS.Responses.Delete;

public readonly record struct DeleteResponseCommand(int Id, int UserId):ICommand<OperationResult>;