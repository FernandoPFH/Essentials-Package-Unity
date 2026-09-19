using UnityEngine;

namespace FernandoPFH_Essentials_Runtime
{
    public class FPSLimitManager : MonoBehaviour
    {
        [SerializeField] private int targetFPS = 60;

        void Awake()
        {
            // Disable VSync
            QualitySettings.vSyncCount = 0;

            Application.targetFrameRate = targetFPS;
        }
    }
}