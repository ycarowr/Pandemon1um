﻿using HexCardGame.SharedData;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace HexCardGame.Runtime.Test
{
    public partial class Mechanics_Test
    {
        public class MockCardData : ICardData
        {
            public CardId Id { get; }
            public Sprite Artwork { get; }
            public string Name { get; }
            public string Description { get; }
            public Color Color { get; }
        }
    }
}