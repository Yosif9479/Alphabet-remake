using Models.GameModes.Balloons;
using UnityEngine;

namespace ScriptableObjects.Sources.GameModes.Balloons
{
    [CreateAssetMenu(fileName = "LetterSet", menuName = "Info Sets/Balloons/Letter Set")]
    public class LetterSet : ScriptableObject
    {
        public LetterInfo[] LetterInfos;
    }
}
