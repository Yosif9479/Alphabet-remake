using Models.GameModes.Cards;
using UnityEngine;

namespace ScriptableObjects.Sources.GameModes.Cards
{
    [CreateAssetMenu(fileName = "CardSet", menuName = "Info Sets/Cards/Card Set")]
    public class CardSet : ScriptableObject
    {
        public CardInfo[] CardInfos;
    }
}
