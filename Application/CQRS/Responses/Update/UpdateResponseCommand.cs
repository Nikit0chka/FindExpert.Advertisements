using Ardalis.SharedKernel;
using Domain.Utils;

namespace Application.CQRS.Responses.Update;

public readonly record struct UpdateResponseCommand(int Id, string Comment, int UserId):ICommand<OperationResult>;