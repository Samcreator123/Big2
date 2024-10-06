using Big2.Domain.Games;
using Big2.Domain.Games.Enums;
using Big2.Domain.Games.Exceptions;
using Big2.Helpers.Application;

namespace Big2.Application.CreateGame
{
    public class BuildGameUseCase(IBuildGameRepository repository, ILog log)
    {
        public BuildGameResponse Execute(BuildGameRequest request)
        {
            try
            {
                log.LogTrace("開始建立遊戲", request);

                Game game = Game.Create(request.GameName, request.IncludeJoker, request.MaxPlayers,
                            request.PlayUntilLast, request.Handtypes);

                Player creator = Player.CreateNewOne(request.CreatorName);

                game.JoinGame(creator);

                var response = GetSuccessfulResponse(game, creator);

                repository.SaveAsync(game);

                log.LogTrace("成功建立遊戲", request);

                return response;
            }
            catch (KeyNotFoundException ex)
            {
                return GetFailedResponse(request, BuildGameState.UnknownHandType, ex.ToString());
            }
            catch (InvalidPlayerCountException ex)
            {
                return GetFailedResponse(request, BuildGameState.InvalidPlayerCount, ex.ToString());
            }
            catch (Exception unknownException)
            {
                log.LogError("建立遊戲時發生意料外的錯誤", request, unknownException);

                return GetFailedResponse(request, BuildGameState.UnknownException, unknownException.ToString());
            }
        }

        private static BuildGameResponse GetFailedResponse(BuildGameRequest request, BuildGameState state, string additionalMessage = "")
        {
            return new(
                Guid.Empty,
                request.GameName,
                request.IncludeJoker,
                request.MaxPlayers,
                request.PlayUntilLast,
                request.Handtypes,
                Player.CreateNewOne(string.Empty),
                GameState.Waiting,
                state,
                additionalMessage);
        }

        private static BuildGameResponse GetSuccessfulResponse(Game game, Player creator, string additionalMessage = "")
        {
            return new(
                game.ID,
                game.Name,
                game.IncludeJoker,
                game.MaxPlayers,
                game.PlayUntilLast,
                game.ValidHands,
                creator,
                game.GameState,
                BuildGameState.Success,
                additionalMessage);
        }
    }
}
