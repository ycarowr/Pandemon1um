﻿using System.Collections.Generic;
using HexCardGame.Runtime.GameTurn;
using HexCardGame.SharedData;
using Tools.Patterns.Observer;

namespace HexCardGame.Runtime.Game
{
    /// <summary>  Game Model Implementation. </summary>
    public partial class RuntimeGame : IGame
    {
        public RuntimeGame(GameArgs args)
        {
            Dispatcher = args.Dispatcher;
            Parameters = args.GameParameters;
            GameMechanics = new GameMechanics(this);
            InitializeGameDataStructures(args);
            InitializeTurnBasedStructures(args);
        }

        void InitializeGameDataStructures(GameArgs args)
        {
            {
                //Create Players
                var userId = args.GameParameters.Profiles.UserPlayer.id;
                var aiId = args.GameParameters.Profiles.AiPlayer.id;
                var user = new Player(userId, args.GameParameters, args.Dispatcher);
                var ai = new Player(aiId, args.GameParameters, args.Dispatcher);
                Players = new IPlayer[] {user, ai};

                //Create Hands
                Hands = new IHand[]
                {
                    new Hand(user.Id, args.GameParameters, Dispatcher),
                    new Hand(ai.Id, args.GameParameters, Dispatcher)
                };
            }

            {
                //Create Library
                var libData = new Dictionary<PlayerId, CardData[]>
                {
                    {PlayerId.User, args.GameParameters.library.GetLibrary()},
                    {PlayerId.Ai, args.GameParameters.library.GetLibrary()}
                };

                Library = new Library(libData, Dispatcher);
            }

            Graveyard = new Graveyard(Dispatcher);
        }

        void InitializeTurnBasedStructures(GameArgs args)
        {
            TurnLogic = new TurnLogic(Players);
            BattleFsm = new BattleFsm(args, this);
        }

        public struct GameArgs
        {
            public IDispatcher Dispatcher;
            public IGameController Controller;
            public GameParameters GameParameters;
        }
    }

    public struct GameMechanics
    {
        public StartGame StartGame { get; }
        public FinishGame FinishGame { get; }
        public HandLibrary HandLibrary { get; }
        public PreStartGame PreStartGame { get; }
        public HandGraveyard HandGraveyard { get; }
        public StartPlayerTurn StartPlayerTurn { get; }
        public FinishPlayerTurn FinishPlayerTurn { get; }

        public GameMechanics(IGame game)
        {
            StartGame = new StartGame(game);
            FinishGame = new FinishGame(game);
            HandLibrary = new HandLibrary(game);
            PreStartGame = new PreStartGame(game);
            HandGraveyard = new HandGraveyard(game);
            StartPlayerTurn = new StartPlayerTurn(game);
            FinishPlayerTurn = new FinishPlayerTurn(game);
        }
    }
}