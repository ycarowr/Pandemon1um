﻿using System.Collections.Generic;
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
        CardHand SelectedCard { get; set; }
        ObjectPooler Pooler => ObjectPooler.Instance;
        UiCardHandSelector CardHandSelector { get; set; }
        public PlayerId Id => id;

        protected override void Awake()
        {
            base.Awake();
            CardHandSelector = GetComponent<UiCardHandSelector>();
            CardHandSelector.OnCardSelected += SelectCard;
            CardHandSelector.OnCardUnSelect += Unselect;
        }

        void OnDestroy()
        {
            CardHandSelector.OnCardSelected -= SelectCard;
            CardHandSelector.OnCardUnSelect -= Unselect;
        }

        public void CreateCardFromLibrary(CardHand cardHand) =>
            CreateUiCard(cardHand, libraryPosition.position);

        public void RemoveCard(CardHand cardHand)
        {
            CardHandSelector.PlaySelected();
            RemoveUiCard(cardHand);
        }

        public void Clear()
        {
            _registry.Clear();
            SelectedCard = null;
        }

        void CreateUiCard(CardHand card, Vector3 position)
        {
            var uiCard = Pooler.Get<IUiCard>(cardPrefab);
            var cardTransform = uiCard.transform;
            cardTransform.SetParent(CardHandSelector.transform);
            cardTransform.position = position;
            uiCard.SetAndUpdateView(card.Data);
            uiCard.Initialize();
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

        void SelectCard(IUiCard uiCard) => SelectedCard = _registry[uiCard];

        void Unselect() => SelectedCard = null;
    }
}