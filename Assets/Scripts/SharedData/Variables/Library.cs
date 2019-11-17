using UnityEngine;

namespace HexCardGame.SharedData
{
    [CreateAssetMenu(menuName = "Variables/Library")]
    public class Library : ScriptableObject
    {
        [SerializeField] CardData[] deck;

        public CardData[] GetLibrary() => deck;
    }
}