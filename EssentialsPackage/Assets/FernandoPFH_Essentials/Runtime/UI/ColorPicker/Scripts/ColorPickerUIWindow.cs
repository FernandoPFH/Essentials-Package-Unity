using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace FernandoPFH_Essentials_Runtime
{
    public class ColorPickerUIWindow : Singleton<ColorPickerUIWindow>, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Image colorPreview;
        [SerializeField] private Image lastColorPreview;
        [SerializeField] private ColorPickerGradientUI colorPickerGradientUI;
        [SerializeField] private ColorPickerWheelUI colorPickerWheelUI;
        [SerializeField] private SliderAndTextInputHandler alphaHandler;
        [SerializeField] private SliderAndTextInputHandler intensityHandler;

        public UnityEvent<Color> onValueChanged = new();
        public Color Color => colorHSLAI.ToColor();
        public Color ColorWithoutHDR => colorHSLAI.ToColorWithoutHDR();

        private HSLAI colorHSLAI;
        private Color lastColor;
        private bool isMouseOverIt;
        private bool isBeingDragged;

        private void Update()
        {
            HandleEscPress();

            if (isBeingDragged)
                return;

            HandleMousePress();
        }

        private void HandleEscPress()
        {
            if (!Keyboard.current.escapeKey.isPressed)
                return;

            SetColor(lastColor);
            CloseWindow();
        }

        private void HandleMousePress()
        {
            if (isMouseOverIt || !Mouse.current.leftButton.IsPressed())
                return;

            CloseWindow();
        }

        public void OnPointerEnter(PointerEventData eventData)
            => isMouseOverIt = true;

        public void OnPointerExit(PointerEventData eventData)
            => isMouseOverIt = false;

        public void SetColor(Color color)
        {
            colorHSLAI = color.ToHSLAI();
            color.DecomposeHdrColor(out Color baseLinearColor, out float _);

            lastColor = color;
            baseLinearColor.a = color.a;
            lastColorPreview.color = baseLinearColor;

            UpdateUI();
        }

        private void UpdateUI()
        {
            colorPickerWheelUI.SetHue(colorHSLAI.H);
            colorPickerGradientUI.SetHue(colorHSLAI.H);
            colorPickerGradientUI.SetSaturationLightness(new(colorHSLAI.S, colorHSLAI.L));

            alphaHandler.SetupUI(colorHSLAI.A);
            alphaHandler.SetBackgroundColor(ColorWithoutHDR);

            intensityHandler.SetupUI(colorHSLAI.I);

            UpdateColorPreview();
        }

        public void UpdateHue(float hue)
        {
            colorHSLAI.H = hue;

            UpdateUI();
        }

        public void UpdateSaturationLightness(Vector2 saturationLightness)
        {
            colorHSLAI.S = saturationLightness.x;
            colorHSLAI.L = saturationLightness.y;

            UpdateUI();
        }

        public void UpdateAlpha(float alpha)
        {
            colorHSLAI.A = alpha;
            UpdateUI();
        }

        public void UpdateIntensity(float intensity)
        {
            colorHSLAI.I = intensity;
            UpdateUI();
        }

        public void UpdateColorPreview()
        {
            colorPreview.color = ColorWithoutHDR;
            onValueChanged.Invoke(Color);
        }

        public void SetupUI(Vector2 screenPosition, bool isHDR, Color color)
        {
            transform.SetParent(FindFirstObjectByType<Canvas>().transform);
            (transform as RectTransform).anchoredPosition = screenPosition * (new Vector2(1920f, 1080f) / new Vector2(Screen.width, Screen.height));
            gameObject.SetActive(true);

            Canvas.ForceUpdateCanvases();

            SetColor(color);

            ToggleHDR(isHDR);

            (transform as RectTransform).localScale = Vector2.one;
        }

        private void ToggleHDR(bool isHDR)
            => intensityHandler.gameObject.SetActive(isHDR);

        public void CloseWindow()
        {
            gameObject.SetActive(false);
            onValueChanged = new();
        }

        public void OnBeginDrag(PointerEventData eventData)
            => isBeingDragged = true;

        public void OnEndDrag(PointerEventData eventData)
            => isBeingDragged = false;

        public void OnDrag(PointerEventData eventData)
            => (transform as RectTransform).anchoredPosition += eventData.delta * (new Vector2(1920f, 1080f) / new Vector2(Screen.width, Screen.height));
    }
}