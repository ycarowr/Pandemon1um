﻿using Tools.Patterns.Observer;

namespace HexCardGame.Runtime.Game
{
    /// <summary> Small Part of a Turn. </summary>
    public abstract class BaseGameMechanics
    {
        protected BaseGameMechanics(IGame game) => Game = game;

        protected IGame Game { get; }

        protected IDispatcher Dispatcher => Game.Dispatcher;

        protected GameParameters Parameters => Game.Parameters;
    }
}