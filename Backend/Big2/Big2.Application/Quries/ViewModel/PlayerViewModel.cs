namespace Big2.Application.Quries.ViewModel;
public record PlayerViewModel(
    Guid Id,
    Guid GameId,
    string Name,
    string State,
    List<CardViewModel> Cards,
    ValidHandViewModel? CurrentCard,
    int Score,
    bool HasPass)
{
}




