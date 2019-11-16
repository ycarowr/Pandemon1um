using UnityEngine;
using UnityEngine.Tilemaps;

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
        [SerializeField] CardId id;
        [SerializeField] string nameCard;
        [SerializeField, Multiline] string description;
        [SerializeField] Color color = Color.white;
 
        // -------------------------------------------------------------------------------------------------------------

        public CardId Id => id;
        public string Name => nameCard;
        public string Description => description;
        public Color Color => color;
    }
}