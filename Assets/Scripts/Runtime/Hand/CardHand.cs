﻿using HexCardGame.SharedData;

namespace HexCardGame.Runtime
{
    public class CardHand : ICard
    {
        public CardHand(ICardData data) => SetData(data);
        public ICardData Data { get; private set; }
        public void SetData(ICardData data) => Data = data;
    }
}