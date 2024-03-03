using Carter;
using DataGathering.Api.Contracts;
using DataGathering.Api.Features.Records;
using DataGathering.Api.Shared.Pagination;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DataGathering.Api.Features.Records
{
    public static class GetRecords
    {
        public sealed class Query : IRequest<Result<PagedResultDto<RecordResponse>>>
        {
            public PageDto Pagination { get; set; }
        }

        internal sealed class Handler : IRequestHandler<Query, Result<PagedResultDto<RecordResponse>>>
        {
            public async Task<Result<PagedResultDto<RecordResponse>>> Handle(Query request, CancellationToken cancellationToken)
            {
                // TODO: get from db

                return new PagedResultDto<RecordResponse>
                {
                    Page = request.Pagination.Page,
                    PageSize = request.Pagination.PageSize,
                };
            }
        }
    }
}

public class GetRecordsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/records", async ([FromBody] PageDto pagination, ISender sender) =>
        {
            var query = new GetRecords.Query
            {
                Pagination = pagination
            };

            var result = await sender.Send(query);

            if (result.IsFailure)
            {
                return Results.NotFound(result.Error);
            }

            return Results.Ok(result.Value);
        });
    }
}
