using Tools.FastStructures;
using Tools.Patterns.Observer;

namespace HexCardGame.Runtime
{
    [Event]
    public interface ICreateHand
    {
        void OnCreateHand(IHand hand, PlayerId id);
    }

    public interface IHand
    {
        PlayerId Id { get; }
        int MaxHandSize { get; }
        CardHand[] Cards { get; }
        int Length { get; }
        void Add(CardHand card);
        bool Has(CardHand card);
        bool Remove(CardHand card);
        void Clear();
    }

    public class Hand : FastList<CardHand>, IHand
    {
        public Hand(PlayerId id, GameParameters gameParameters, IDispatcher dispatcher)
        {
            Id = id;
            Parameters = gameParameters;
            Dispatcher = dispatcher;
            OnCreateHand();
        }

        IDispatcher Dispatcher { get; }
        GameParameters Parameters { get; }
        public PlayerId Id { get; }
        public CardHand[] Cards => Array;
        public int MaxHandSize => Parameters.Hand.MaxHandSize;
        public void Add(CardHand card) => Add(card, true);
        void OnCreateHand() => Dispatcher.Notify<ICreateHand>(i => i.OnCreateHand(this, Id));
    }
}