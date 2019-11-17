using HexCardGame.SharedData;
using UnityEngine;

namespace HexCardGame.Runtime.Test
{
    public partial class Mechanics_Test
    {
        public class MockCardData : ICardData
        {
            public Sprite Artwork { get; }
            public CardId Id { get; }
            public string Name { get; }
            public string Description { get; }
            public Color Color { get; }
        }
    }
}