using Big2.Domain.Games;
using Big2.Domain.Games.Exceptions;
using Big2.Domain.Players.Exceptions;
using Big2.Domain.Services.Decks;
using Big2.Helpers.Application;

namespace Big2.Application.StartGame
{
    public class StartGameUseCase(IStartGameRepository repository, ILog log)
    {
        public StartGameResponse Execute(StartGameRequest request)
        {
            try
            {
                log.LogInfo($"遊戲 {request.GameID} 準備完成，開始進行發牌與其他布局環節", request);

                Game game = repository.FindGameByID(request.GameID);
                game.SetDealing();

                Deck deck = Deck.Create(game.IncludeJoker);
                deck.Shuffle();

                List<Domain.Players.Player> players = repository.FindAllPlayersInGame(request.GameID);

                var playersAndCards = deck.DealTheCard(new LinkedList<Guid>(game.Players.Select(p => p.ID)));

                players.ForEach(player =>
                {
                    player.InitialCards(playersAndCards[player.ID]);
                    player.SetPlaying();
                });

                game.SetPlaying();

                repository.Save(game);
                repository.Save(players);

                log.LogInfo($"遊戲 {request.GameID} 佈局完畢，開始進行遊戲", request);

                return GetSuccessfulResponse(game, players);
            }
            catch (InvalidGameStateException ex)
            {
                log.LogWarning(ex.ToString());
                return GetFailedResponse(StartGameState.InvalidGameStateException, ex.ToString());
            }
            catch (InvalidPlayerStateException ex)
            {
                log.LogWarning(ex.ToString());
                return GetFailedResponse(StartGameState.InvalidPlayerStateException, ex.ToString());
            }
            catch (Exception unknownException)
            {
                log.LogError($"{request.GameID} 開始進行遊戲發生錯誤", request, unknownException);
                return GetFailedResponse(StartGameState.UnknownException, unknownException.ToString());
            }
        }

        private static StartGameResponse GetFailedResponse(StartGameState state, string additionalMessage = "")
        {
            Game game = Game.Create(string.Empty, false, 2, false, new());

            return new(game, new(), state, additionalMessage);
        }

        private static StartGameResponse GetSuccessfulResponse(Game game, List<Domain.Players.Player> players, string additionalMessage = "")
        {
            return new(game, players, StartGameState.Success, additionalMessage);
        }
    }
}
