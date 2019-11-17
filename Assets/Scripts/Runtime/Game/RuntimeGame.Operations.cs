using UnityEngine;

namespace HexCardGame.Runtime.Game
{
    public partial class RuntimeGame
    {
        #region Operations

        public void StartGame() => GameMechanics.StartGame.Execute();
        public void PreStartGame() => GameMechanics.PreStartGame.Execute();
        public void StartPlayerTurn() => GameMechanics.StartPlayerTurn.Execute();
        public void FinishPlayerTurn() => GameMechanics.FinishPlayerTurn.Execute();
        public void DrawCard(PlayerId playerId) => GameMechanics.HandLibrary.DrawCard(playerId);

        public void PlayCard(PlayerId playerId, CardHand cardHand) =>
            GameMechanics.HandGraveyard.PlayCard(playerId, cardHand);

        public void ExecuteAiTurn(PlayerId id)
        {
        }

        public void ForceWin(PlayerId id)
        {
            var player = TurnLogic.GetPlayer(id);
            GameMechanics.FinishGame.Execute(player);
        }

        #endregion
    }
}