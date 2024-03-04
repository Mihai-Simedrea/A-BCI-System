using Carter;
using DataGathering.Api.Contracts;
using DataGathering.Api.Entities;
using DataGathering.Api.Features.Records;
using DataGathering.Api.Persistence;
using Domain.Shared;
using Mapster;
using MediatR;

namespace DataGathering.Api.Features.Records
{
    public static class GetRecord
    {
        public sealed class Query : IRequest<Result<RecordResponse>>
        {
            public Guid Id { get; set; }
        }

        internal sealed class Handler : IRequestHandler<Query, Result<RecordResponse>>
        {
            private readonly IRepository<Record> _repository;

            public Handler(IRepository<Record> repository)
            {
                _repository = repository;
            }

            public async Task<Result<RecordResponse>> Handle(Query request, CancellationToken cancellationToken)
            {
                var record = await _repository.TryGetByIdAsync(request.Id, cancellationToken);

                return record.Adapt<RecordResponse>();
            }
        }
    }
}

public class GetRecordEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/record/{id}", async (Guid id, ISender sender) =>
        {
            var query = new GetRecord.Query { Id = id };

            var result = await sender.Send(query);

            if (result.IsFailure)
            {
                return Results.NotFound(result.Error);
            }

            return Results.Ok(result.Value);
        });
    }
}
