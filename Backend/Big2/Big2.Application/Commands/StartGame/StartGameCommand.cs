﻿using MediatR;

namespace Big2.Application.Commands.StartGame;

public class StartGameCommand : IRequest<bool>
{
    public required Guid GameId { get; init; }
}