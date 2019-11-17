namespace HexCardGame.Runtime.Game
{
    [Event]
    public interface IPlayCard
    {
        void OnPlayCard(PlayerId id, CardHand cardHand);
    }
 
    [Event]
    public interface IDiscardCard
    {
        void OnDiscardCard(CardHand cardHand);
    }

    public class HandGraveyard : BaseGameMechanics
    {
        public HandGraveyard(IGame game) : base(game)
        {
        }

        public void PlayCard(PlayerId playerId, CardHand cardHand)
        {
            if (!Game.IsGameStarted)
                return;
            if (!Game.TurnLogic.IsMyTurn(playerId))
                return;
            var playerHand = GetPlayerHand(playerId);
            if (!playerHand.Has(cardHand))
                return;

            playerHand.Remove(cardHand);
            Game.Graveyard.AddCard(cardHand);
            OnPlayCard(playerId, cardHand);
            OnDiscardCard(cardHand);
        }

        void OnPlayCard(PlayerId playerId, CardHand card) =>
            Dispatcher.Notify<IPlayCard>(i => i.OnPlayCard(playerId, card));

        void OnDiscardCard(CardHand cardHand) => 
            Dispatcher.Notify<IDiscardCard>(i => i.OnDiscardCard(cardHand));

        IHand GetPlayerHand(PlayerId id)
        {
            foreach (var i in Game.Hands)
                if (i.Id == id)
                    return i;
            return null;
        }
    }
}