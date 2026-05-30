using AutoMapper;
using MediatR;
using Persistence;

namespace Application;

public class CreateActivity
{
    public class Command : IRequest<ReadActivityDto>
    {
        public required CreateActivityDto Activity { get; set; }
    }

    public class Handler(
        IActivityRepo repo,
        IMapper mapper
    ) : IRequestHandler<Command, ReadActivityDto>
    {
        public async Task<ReadActivityDto> Handle(Command cmd, CancellationToken ct)
        {
            var mappedActivity = mapper.Map<Domain.Activity>(cmd.Activity);
            return mapper.Map<ReadActivityDto>(await repo.CreateActivity(mappedActivity, ct));
        }
    }
}