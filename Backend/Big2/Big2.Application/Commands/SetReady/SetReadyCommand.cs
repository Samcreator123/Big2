namespace Big2.Application.Commands.SetReady;

public class SetReadyCommand : IRequest
{
    public Guid PlayerId { get; init; }
}
