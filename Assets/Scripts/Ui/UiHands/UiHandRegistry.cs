using System.Collections.Generic;
using Game.Ui;
using HexCardGame.Runtime;
using Tools.UI.Card;
using UnityEngine;

namespace HexCardGame.UI
{
    [RequireComponent(typeof(UiCardHandSelector))]
    public class UiHandRegistry : UiEventListener
    {
        readonly Dictionary<IUiCard, CardHand> _registry = new Dictionary<IUiCard, CardHand>();
        [SerializeField] GameObject cardPrefab;
        [SerializeField] PlayerId id;
        [SerializeField] Transform libraryPosition;
        ObjectPooler Pooler => ObjectPooler.Instance;
        UiCardHandSelector CardHandSelector { get; set; }
        public PlayerId Id => id;

        protected override void Awake()
        {
            base.Awake();
            CardHandSelector = GetComponent<UiCardHandSelector>();
            CardHandSelector.OnCardPlayed += RequestPlayCard;
        }

        void RequestPlayCard(IUiCard uiCardPlayed)
        {
            var cardHand = _registry[uiCardPlayed];
            GameData.CurrentGameInstance.PlayCard(PlayerId.User, cardHand);
        }

        public void CreateCardFromLibrary(CardHand cardHand) =>
            CreateUiCard(cardHand, libraryPosition.position);
        
        public void Clear() => _registry.Clear();

        void CreateUiCard(CardHand card, Vector3 position)
        {
            var uiCard = Pooler.Get<IUiCard>(cardPrefab);
            var cardTransform = uiCard.transform;
            cardTransform.SetParent(CardHandSelector.transform);
            cardTransform.position = position;
            uiCard.Initialize();
            uiCard.SetAndUpdateView(card.Data);
            CardHandSelector.AddCard(uiCard);
            _registry.Add(uiCard, card);
        }

        public void RemoveUiCard(CardHand card)
        {
            IUiCard removed = null;
            foreach (var key in _registry.Keys)
                if (_registry[key] == card)
                    removed = key;

            if (removed != null)
                _registry.Remove(removed);

            Pooler.Release(removed?.gameObject);
        }
    }
}