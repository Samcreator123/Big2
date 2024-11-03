using MediatR;

namespace Big2.Application.Commands.SetNotReady;

public class SetNotReadyCommand : IRequest<bool>
{
    public Guid PlayerId { get; init; }
}
