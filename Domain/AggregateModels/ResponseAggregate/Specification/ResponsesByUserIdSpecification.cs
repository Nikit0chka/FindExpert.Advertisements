using Ardalis.Specification;

namespace Domain.AggregateModels.ResponseAggregate.Specification;

public sealed class ResponsesByUserIdSpecification:Specification<Response>
{
    public ResponsesByUserIdSpecification(int userId) { Query.Where(response => response.UserId == userId); }
}