using Models.GameModes.MemoryGame;
using UnityEngine;

namespace ScriptableObjects.Sources.GameModes.MemoryGame
{
    [CreateAssetMenu(fileName = "CardSet", menuName = "Info Sets/Memory Game/Card Set")]
    public class CardSet : ScriptableObject
    {
        public CardInfo[] CardInfos;
    }
}
