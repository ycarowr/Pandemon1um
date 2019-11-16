﻿using HexCardGame.Runtime.GamePool;

namespace HexCardGame.Runtime.Game
{
    /// <summary> Broadcast of the starter player to the Listeners. </summary>
    [Event]
    public interface IStartGame
    {
        void OnStartGame(IPlayer starter);
    }

    [Event]
    public interface IRevealPool
    {
        void OnRevealPool(IPool<CardPool> pool);
    }

    /// <summary> Start Game Step Implementation. </summary>
    public class StartGame : BaseGameMechanics
    {
        public StartGame(IGame game) : base(game)
        {
        }

        /// <summary> Execution of start game</summary>
        public void Execute()
        {
            if (Game.IsGameStarted) return;

            Game.IsGameStarted = true;

            Game.TurnLogic.DecideStarterPlayer();

            DrawStartingHands();
            RevealPool();

            OnGameStarted(Game.TurnLogic.StarterPlayer);
        }

        void DrawStartingHands()
        {
            foreach (var player in Game.TurnLogic.Players)
                for (var i = 0; i < Parameters.Hand.StartingHandCount; i++)
                    Game.DrawCardFromLibrary(player.Id);
        }

        void RevealPool()
        {
            var library = Game.Library;
            var pool = Game.Pool;
            var positions = PoolPositionUtility.GetAllIndices();
            foreach (var i in positions)
            {
                var data = library.GetRandomData();
                var cardPool = new CardPool(data);
                pool.AddCardAt(cardPool, i);
            }

            OnRevealPool(pool);
        }

        void OnRevealPool(IPool<CardPool> pool) => Dispatcher.Notify<IRevealPool>(i => i.OnRevealPool(pool));
        void OnGameStarted(IPlayer starterPlayer) => Dispatcher.Notify<IStartGame>(i => i.OnStartGame(starterPlayer));
    }
}