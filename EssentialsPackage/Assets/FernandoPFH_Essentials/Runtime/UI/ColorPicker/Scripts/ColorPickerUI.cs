using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.InputSystem;

namespace FernandoPFH_Essentials_Runtime
{
    public class ColorPickerUI : MonoBehaviour
    {
        [SerializeField] private Image colorPreview;
        [SerializeField] private Image alphaPreview;
        [SerializeField] private GameObject hdrLabel;
        [SerializeField] private GameObject windowPrefab;
        [SerializeField] private bool isHDR = true;

        public UnityEvent<Color> onValueChanged = new();

        private Color color = Color.red;

        void Start()
        {
            SetColor(color);
            ToggleHDRLabel(isHDR);
        }

        public void SetColor(Color color)
        {
            this.color = color;
            color.DecomposeHdrColor(out Color baseLinearColor, out float intensity);
            colorPreview.material.SetColor("_Color", baseLinearColor.OpaqueColor());
            colorPreview.material.SetFloat("_Intensity", intensity);
            alphaPreview.color = Color.white.MultiplyOpaqueColor(color.a);
        }

        public void ToggleHDRLabel(bool isEnabled)
        {
            isHDR = isEnabled;
            hdrLabel.SetActive(isEnabled);
        }

        public void HandlePress()
        {
            if (!ColorPickerUIWindow.Instance)
                Instantiate(windowPrefab);

            Vector2 mousePos = Mouse.current.position.ReadValue();

            ColorPickerUIWindow.Instance.SetupUI(new(mousePos.x, -mousePos.y), isHDR, color);

            ColorPickerUIWindow.Instance.onValueChanged.AddListener(UpdateColor);
        }

        private void UpdateColor(Color color)
        {
            SetColor(color);
            onValueChanged.Invoke(color);
        }
    }
}