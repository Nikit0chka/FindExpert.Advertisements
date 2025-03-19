using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Application.CQRS.Advertisements.DeleteAdvertisement;

public readonly record struct DeleteAdvertisementCommand(int Id, int UserId) : ICommand<Result>;