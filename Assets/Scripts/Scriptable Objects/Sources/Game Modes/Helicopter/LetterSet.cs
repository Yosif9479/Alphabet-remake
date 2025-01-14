using System;
using GameModes.Helicopter;
using UnityEngine;

namespace ScriptableObjects.Sources.GameModes.Helicopter
{
    [CreateAssetMenu(fileName = "LetterSet", menuName = "Info Sets/Helicopter/Letter Set")]
    public class LetterSet : ScriptableObject
    {
        public LetterInfo[] LetterInfos;
    }
}