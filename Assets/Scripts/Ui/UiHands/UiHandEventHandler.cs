using Game.Ui;
using HexCardGame.Runtime;
using HexCardGame.Runtime.Game;
using UnityEngine;

namespace HexCardGame.UI
{
    [RequireComponent(typeof(UiHandRegistry))]
    public class UiHandEventHandler : UiEventListener, IDrawCard, IRestartGame, IPlayCard
    {
        UiHandRegistry Registry { get; set; }

        void IDrawCard.OnDrawCard(PlayerId id, CardHand cardHand)
        {
            if (!IsMyEvent(id))
                return;

            Registry.CreateCardFromLibrary(cardHand);
        }

        void IPlayCard.OnPlayCard(PlayerId id, CardHand cardHand)
        {
            if (IsMyEvent(id))
                Registry.RemoveUiCard(cardHand);
        }

        void IRestartGame.OnRestart() => Registry.Clear();

        bool IsMyEvent(PlayerId id) => Registry.Id == id;

        protected override void Awake()
        {
            base.Awake();
            Registry = GetComponent<UiHandRegistry>();
        }
    }
}