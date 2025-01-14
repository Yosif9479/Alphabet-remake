using UnityEngine;

namespace Startup
{
    public class Bootstrap : MonoBehaviour
    {
        [Header("FPS Settings")]
        [SerializeField] private int _targetFps;
        
        private void Awake()
        {
            Application.targetFrameRate = _targetFps;
            QualitySettings.vSyncCount = 0;
        }
    }
}
