using System;
using UnityEngine;

namespace Models.GameModes.Cards
{
    [Serializable]
    public class CardInfo
    {
        public string Letter;
        public string Name;
        public Sprite Icon;
        public AudioClip AudioClip;
    }
}