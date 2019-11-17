using Game.Ui;
using HexCardGame;
using HexCardGame.UI;
using Tools.Input.Mouse;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Tools.UI.Card
{
    [RequireComponent(typeof(IMouseInput))]
    public class UiCardDrawerClick : UiGameDataRequester
    {
        IMouseInput Input { get; set; }

        protected override void Awake()
        {
            base.Awake();
            Input = GetComponent<IMouseInput>();
            Input.OnPointerClick += DrawCard;
        }

        void DrawCard(PointerEventData obj) => GameData.CurrentGameInstance.DrawCard(PlayerId.User);
    }
}