using UnityEngine;

namespace HexCardGame.SharedData
{
    public interface ICardData
    {
        CardId Id { get; }
        string Name { get; }
        string Description { get; }
        Color Color { get; }
    }

    [CreateAssetMenu(menuName = "Data/Card")]
    public class CardData : ScriptableObject, ICardData
    {
        [SerializeField] Color color = Color.white;
        [SerializeField, Multiline] string description;
        [SerializeField] CardId id;
        [SerializeField] string nameCard;

        // -------------------------------------------------------------------------------------------------------------

        public CardId Id => id;
        public string Name => nameCard;
        public string Description => description;
        public Color Color => color;
    }
}