using MediatR;

namespace Big2.Application.Commands.SetNotReady;

public class SetNotReadyCommand : IRequest
{
    public Guid PlayerId { get; init; }
}
