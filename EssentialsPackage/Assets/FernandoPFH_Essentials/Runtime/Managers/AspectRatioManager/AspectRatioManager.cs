using UnityEngine;

namespace FernandoPFH_Essentials_Runtime
{
    public class AspectRatioManager : MonoBehaviour
    {
        [SerializeField] private Vector2 targerAspectRatio = new(16f,9f);

#if UNITY_EDITOR
        private Vector2Int lastResolution;
#endif

        void Start()
        {
            Adjust();

#if UNITY_EDITOR
            lastResolution = new(Screen.width,Screen.height);
#endif
        }

#if UNITY_EDITOR
        void Update()
        {
            Vector2Int currentScreenSize = new(Screen.width,Screen.height);

            if (lastResolution != currentScreenSize) {
                Adjust();

                lastResolution = currentScreenSize;
            }
        }
#endif

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
