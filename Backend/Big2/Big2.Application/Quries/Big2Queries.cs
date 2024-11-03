namespace Big2.Application.Quries;
public class Big2Queries(IRepository<Game> gameRepository, IRepository<Player> playerRepository)
{
    public async Task<GameViewModel> GetGame(Guid gameId, CancellationToken cancellationToken)
    {
        var game = await gameRepository.FindById(gameId, cancellationToken)
        ?? throw new NotFoundGameException($"找不到遊戲 {gameId}");

        return Mapper.MappingGameViewModel(game);
    }

    public async Task<PlayerViewModel> GetPlayer(Guid playerId, CancellationToken cancellationToken)
    {
        var player = await playerRepository.FindById(playerId, cancellationToken)
            ?? throw new NotFoundPlayerException($"找不到玩家 {playerId}");

        return Mapper.MappingPlayerViewModel(player);
    }

    public async Task<bool> CanGameStart(Guid gameId, CancellationToken cancellationToken)
    {
        // 查詢物件的準備人數與最大人數是否相符
        var game = await gameRepository.FindById(gameId, cancellationToken)
        ?? throw new NotFoundGameException($"找不到遊戲 {gameId}");

        return game.CanStart();
    }

    public async Task<bool> CanGameEnd(Guid gameId, CancellationToken cancellationToken)
    {
        // 查詢物件的準備人數與最大人數是否相符
        var game = await gameRepository.FindById(gameId, cancellationToken)
        ?? throw new NotFoundGameException($"找不到遊戲 {gameId}");

        return game.CanEnd();
    }
}
