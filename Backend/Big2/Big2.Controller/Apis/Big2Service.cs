using Big2.Application.Quries;
using MediatR;

namespace Big2.Controller.Apis;

// 使用[AsParameters]而非[FromServices]可以不用寫建構子
public class Big2Service
{
    public required IMediator CommandMediator { get; set; }

    public required Big2Queries Queries { get; set; }

    public required ILogger<Big2Service> Logger { get; set; }
}
