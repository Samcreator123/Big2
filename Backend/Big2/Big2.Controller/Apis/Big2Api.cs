using Big2.Application.Commands.LeaveGameAndDeleteGameIfEmpty;
using Big2.Application.Commands.PlayCardAndEndGameIfPossible;
using Big2.Domain.Entities;
using MediatR;

namespace Big2.Controller.Apis;

public class Big2Api : Hub
{
    static readonly Rooms _rooms = new();

    public async Task CreateGameAsync(
        CreateGameDto request,
        [AsParameters] Big2Service services,
        CancellationToken cancellationToken)
    {
        Guid newGameId = Guid.NewGuid();

        CreateGameCommand createGameCommand = new(
            newGameId,
            request.GameName,
            request.IncludeJoker,
            request.MaxPlayers,
            request.PlayUntilLast,
            request.CreatorName,
            request.Handtypes);

        await services.CommandMediator.Send(createGameCommand, cancellationToken);

        var game = await services.Queries.GetGame(newGameId, cancellationToken);

        await Groups.AddToGroupAsync(Context.ConnectionId, game.Id.ToString(), cancellationToken);
    }

    public async Task DeleteGameAsync(
        Guid gameId,
        [AsParameters] Big2Service services,
        CancellationToken cancellationToken)
    {
        await services.CommandMediator.Send(new DeleteGameCommand(gameId), cancellationToken);

        await Clients.Group(gameId.ToString()).SendAsync(ToClientMethodName.GameDeleted, cancellationToken);
    }

    public async Task JoinGame(
        JoinGameDto request,
        [AsParameters] Big2Service services,
        CancellationToken cancellationToken)
    {
        Guid joinedPlayerId = await services.CommandMediator.Send(
            new JoinGameCommand(request.GameId, request.GameName), cancellationToken);

        var game = await services.Queries.GetGame(request.GameId, cancellationToken);

        await Groups.AddToGroupAsync(Context.ConnectionId, request.GameId.ToString(), cancellationToken);

        await Clients.Group(request.GameId.ToString()).SendAsync(
            ToClientMethodName.PlayerJoined,
            joinedPlayerId,
            game,
            cancellationToken);
    }

    public async Task LeaveGame(
        LeaveGameDto request,
        [AsParameters] Big2Service services,
        CancellationToken cancellationToken)
    {
        LeaveGameAndDeleteGameIfEmptyState state = await services.CommandMediator.Send(
            new LeaveGameAndDeleteGameIfEmptyCommand(request.GameId, request.PlayerId), cancellationToken);

        if (state == LeaveGameAndDeleteGameIfEmptyState.OnlyPlayerLeft)
        {
            var game = await services.Queries.GetGame(request.GameId, cancellationToken);

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, request.GameId.ToString(), cancellationToken);

            await Clients.Group(request.GameId.ToString()).SendAsync(ToClientMethodName.PlayerLeft, game, cancellationToken);
        }
    }

    public async Task PlayCard(
        PlayCardDto request,
        [AsParameters] Big2Service services,
        CancellationToken cancellationToken)
    {
        PlayCardAndEndGameIfPossibleCommand command = new(
            request.GameId,
            request.PlayerId,
            request.Cards,
            request.HasPassed);

        PlayCardAndEndGameIfPossibleState state = await services.CommandMediator.Send(command, cancellationToken);

        string methodName = state == PlayCardAndEndGameIfPossibleState.OnlyCardPlayed ?
            ToClientMethodName.CardPlayed : 
            ToClientMethodName.CardPlayedAndGameEnded;

        var game = services.Queries.GetGame(request.GameId, cancellationToken);

        await Clients.Group(request.GameId.ToString()).SendAsync(methodName, game, cancellationToken);
    }



    public override Task OnConnectedAsync()
    {
        return base.OnConnectedAsync();
    }
}
