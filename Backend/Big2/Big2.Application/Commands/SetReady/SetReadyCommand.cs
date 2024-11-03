namespace Big2.Application.Commands.SetReady;

public class SetReadyCommand : IRequest<bool>
{
    public Guid PlayerId { get; init; }
}
