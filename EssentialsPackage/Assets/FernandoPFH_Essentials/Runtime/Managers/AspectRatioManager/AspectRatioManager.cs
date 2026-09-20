using UnityEngine;

namespace FernandoPFH_Essentials_Runtime
{
    public class AspectRatioManager : MonoBehaviour
    {
        [SerializeField] private Vector2 targerAspectRatio = new(16f,9f);
        [SerializeField] private bool shouldAlwaysCheck;

        private Vector2Int lastResolution;

        void Start()
        {
            Adjust();

            lastResolution = new(Screen.width,Screen.height);
#if UNITY_EDITOR
            shouldAlwaysCheck = true;
#endif
        }

        void Update()
        {
            if (!shouldAlwaysCheck)
                return;

            Vector2Int currentScreenSize = new(Screen.width,Screen.height);

            if (lastResolution != currentScreenSize) {
                Adjust();

                lastResolution = currentScreenSize;
            }
        }

        public void Adjust()
        {
            float targetaspect = targerAspectRatio.x / targerAspectRatio.y;

            float windowaspect = (float)Screen.width / (float)Screen.height;

            float scaleheight = windowaspect / targetaspect;

            Camera camera = Camera.main;

            if (scaleheight < 1.0f)
            {
                Rect rect = camera.rect;

                rect.width = 1.0f;
                rect.height = scaleheight;
                rect.x = 0;
                rect.y = (1.0f - scaleheight) / 2.0f;

                camera.rect = rect;
            }
            else
            {
                float scalewidth = 1.0f / scaleheight;

                Rect rect = camera.rect;

                rect.width = scalewidth;
                rect.height = 1.0f;
                rect.x = (1.0f - scalewidth) / 2.0f;
                rect.y = 0;

                camera.rect = rect;
            }

        }
    }
}
