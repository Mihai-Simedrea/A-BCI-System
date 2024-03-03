using Carter;
using DataGathering.Api.Contracts;
using DataGathering.Api.Features.Records;
using Domain.Shared;
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
            public async Task<Result<RecordResponse>> Handle(Query request, CancellationToken cancellationToken)
            {
                // TODO: get from db

                return new RecordResponse
                {
                    Id = Guid.NewGuid(),
                    Data = Enumerable.Empty<int>()
                };
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
