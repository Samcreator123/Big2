using Big2.Domain.Players;
using Big2.Domain.Services.Scoring;
using Big2.Helpers.Application;

namespace Big2.Application.Score
{
    public class ScoreUseCase(ILog log, IScoreRepository repository)
    {
        public ScoreResponse Execute(ScoreRequest request)
        {
            try
            {
                log.LogInfo($"玩家 {request.FinishedPlayerID} 於遊戲 {request.GameID} 出牌結束，開始計分", request);

                Player finishedPlayer = repository.GetFinishedPlayer(request.FinishedPlayerID);
                
                List<Player> unfinishedPlayers = repository.GetUnFinishPlayers(request.GameID);

                int score = ScoringHandler.CalculateScore(finishedPlayer, unfinishedPlayers);

                finishedPlayer.AddScore(score);

                repository.Save(finishedPlayer);

                log.LogInfo($"玩家 {request.FinishedPlayerID} 於遊戲 {request.GameID} 計分結束", request);

                return GetSuccessfulResponse(request, score);
            }
            catch (Exception unknownException)
            {
                log.LogError("加入遊戲時發生意料外的錯誤", request, unknownException);

                return GetFailedResponse(request, ScoreState.UnknownException, unknownException.ToString());
            }
        }

        private static ScoreResponse GetFailedResponse(ScoreRequest request, ScoreState state, string additionalMessage = "")
        {
            return new(
                request.GameID,
                request.FinishedPlayerID,
                0,
                state,
                additionalMessage);
        }

        private static ScoreResponse GetSuccessfulResponse(ScoreRequest request,int scoreInGame,  string additionalMessage = "")
        {
            return new(
                request.GameID,
                request.FinishedPlayerID,
                scoreInGame,
                ScoreState.Success,
                additionalMessage);
        }
    }
}
