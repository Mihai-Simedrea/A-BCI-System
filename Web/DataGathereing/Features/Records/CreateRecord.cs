using Carter;
using DataGathering.Api.Entities;
using DataGathering.Api.Persistence;
using Domain.Shared;
using FluentValidation;
using MediatR;
using static DataGathering.Api.Features.Records.CreateRecord;

namespace DataGathering.Api.Features.Records;
public static class CreateRecord
{
    public class Command : IRequest<Result<Guid>>
    {
        public IEnumerable<int> Data { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(r => r.Data).NotEmpty();
        }
    }

    internal sealed class Handler : IRequestHandler<Command, Result<Guid>>
    {
        private readonly IValidator<Command> _validator;
        private readonly IRepository<Record> _repository;

        public Handler(IValidator<Command> validator, IRepository<Record> repository)
        {
            _validator = validator;
            _repository = repository;
        }

        public async Task<Result<Guid>> Handle(Command request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
            {
                return Result.Failure<Guid>(new Error(
                    "CreateRecord.Validaiton",
                    validationResult.ToString()));
            }

            var record = new Record
            {
                Id = Guid.NewGuid(),
                Data = request.Data,
            };

            await _repository.AddAsync(record);

            return record.Id;
        }
    }
}

public class CreateRecordEndpoint : ICarterModule
{   
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/record", async (Command command, ISender sender) =>
        {
            var result = await sender.Send(command);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }

            return Results.Ok(result.Value);
        });
    }
}