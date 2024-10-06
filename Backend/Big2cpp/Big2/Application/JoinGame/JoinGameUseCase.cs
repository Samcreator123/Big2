using Big2.Domain.Games;
using Big2.Domain.Games.Enums;
using Big2.Domain.Games.Exceptions;
using Big2.Helpers.Application;

namespace Big2.Application.JoinGame
{
    public class JoinGameUseCase(IJoinGameRepository repository, ILog log)
    {
        public JoinGameResponse Execute(JoinGameRequest request)
        {
            try
            {
                log.LogTrace($"玩家 {request.GamerName} 嘗試加入遊戲 {request.GameID}", request);

                Game game = repository.FindGameByID(request.GameID);

                Player player = Player.CreateNewOne(request.GamerName);

                game.JoinGame(player);

                repository.AddAGamer(game);

                log.LogTrace($"玩家 {request.GamerName} 成功加入遊戲 {request.GameID}", request);

                return GetSuccessfulResponse(game);
            }
            catch (PlayerLimitExceededException ex)
            {
                return GetFailedResponse(request, JoinGameState.PlayerLimitExceeded, ex.ToString());
            }
            catch (GameInProgressException ex)
            {
                return GetFailedResponse(request, JoinGameState.GameInProgress, ex.ToString());
            }
            catch (Exception unknownException)
            {
                log.LogError("加入遊戲時發生意料外的錯誤", request, unknownException);

                return GetFailedResponse(request, JoinGameState.UnknownException, unknownException.ToString());
            }
        }

        private static JoinGameResponse GetFailedResponse(JoinGameRequest request, JoinGameState state, string additionalMessage = "")
        {
            return new(
                request.GameID,
                string.Empty,
                false,
                0,
                false,
                new(),
                new(),
                GameState.Waiting,
                state,
                additionalMessage);
        }

        private static JoinGameResponse GetSuccessfulResponse(Game game, string additionalMessage = "")
        {
            return new(
                game.ID,
                game.Name,
                game.IncludeJoker,
                game.MaxPlayers,
                game.PlayUntilLast,
                game.ValidHands,
                game.Players,
                game.GameState,
                JoinGameState.Success,
                additionalMessage);
        }
    }
}
