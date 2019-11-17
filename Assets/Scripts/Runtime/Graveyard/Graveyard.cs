using Tools.FastStructures;
using Tools.Patterns.Observer;

namespace HexCardGame.Runtime
{
    [Event]
    public interface ICreateGraveyard
    {
        void OnCreateGraveyard(IGraveyard graveyard);
    }

    public interface IGraveyard
    {
        int Size { get; }
        void Clear();
        CardHand[] GetCards();
        void AddCard(CardHand cardHand);
    }

    public class Graveyard : IGraveyard
    {
        readonly FastList<CardHand> _register = new FastList<CardHand>();

        public Graveyard(IDispatcher dispatcher)
        {
            Dispatcher = dispatcher;
            OnCreateGraveyard();
        }

        IDispatcher Dispatcher { get; }
        public int Size => _register.Length;
        public void AddCard(CardHand cardHand) => _register.Add(cardHand);
        public CardHand[] GetCards() => _register.GetArray();
        public void Clear() => _register.Clear();
        void OnCreateGraveyard() => Dispatcher.Notify<ICreateGraveyard>(i => i.OnCreateGraveyard(this));
    }
}