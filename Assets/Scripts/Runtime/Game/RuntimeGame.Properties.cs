﻿using HexCardGame.Runtime.GameTurn;
using Tools.Patterns.Observer;

namespace HexCardGame.Runtime.Game
{
    public partial class RuntimeGame
    {
        #region Properties

        public GameParameters Parameters { get; }
        public IDispatcher Dispatcher { get; }
        public bool IsGameStarted { get; set; }
        public bool IsGameFinished { get; set; }
        public bool IsTurnInProgress { get; set; }

        #endregion

        #region Game Data Structures

        public ILibrary Library { get; private set; }
        public IGraveyard Graveyard { get; private set; }
        public IPlayer[] Players { get; private set; }
        public IHand[] Hands { get; private set; }

        #endregion

        #region Turn Mechanics

        public ITurnLogic TurnLogic { get; private set; }
        public BattleFsm BattleFsm { get; private set; }
        public GameMechanics GameMechanics { get; }

        #endregion
    }
}