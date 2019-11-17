using HexCardGame.Runtime.GameTurn;
using Tools.Patterns.Observer;
using UnityEngine;

namespace HexCardGame.Runtime.Game
{
    /// <summary> A turn-based game interface. </summary>
    public interface ITurnMechanics
    {
        ITurnLogic TurnLogic { get; }
        BattleFsm BattleFsm { get; }
        bool IsTurnInProgress { get; set; }
        void PreStartGame();
        void StartGame();
        void StartPlayerTurn();
        void FinishPlayerTurn();
    }

    /// <summary> A game interface. </summary>
    public interface IGame : ITurnMechanics, ICardGame
    {
        GameParameters Parameters { get; }
        IDispatcher Dispatcher { get; }
        bool IsGameStarted { get; set; }
        bool IsGameFinished { get; set; }
        IHand[] Hands { get; }
        ILibrary Library { get; }
        void ExecuteAiTurn(PlayerId id);
        void ForceWin(PlayerId id);
    }

    public interface ICardGame
    {
        void DrawCard(PlayerId playerId);
        void PlayCard(PlayerId playerId, CardHand cardHand);
    }
}