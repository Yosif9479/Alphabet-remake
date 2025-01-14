using Models.GameModes.Balloons;
using UnityEngine;

namespace ScriptableObjects.Sources.GameModes.Balloons
{
    [CreateAssetMenu(fileName = "BalloonSet", menuName = "Info Sets/Balloons/Ballon Set")]
    public class BalloonSet : ScriptableObject
    {
        public BalloonInfo[] BalloonInfos;
    }
}
