using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

namespace FernandoPFH_Essentials_Runtime
{
    public class SliderAndTextInputHandler : MonoBehaviour
    {
        [SerializeField] private int decimalPoints = 2;
        [SerializeField] private Slider valueSlider;
        [SerializeField] private Image valueSliderBackground;
        [SerializeField] private TMP_InputField valueInputField;
        [SerializeField] private UnityEvent<float> onValueChanged;

        private float lastValue;

        public void UpdateUI()
        {
            valueInputField.text = lastValue.ToString($"n{decimalPoints}");
            valueSlider.value = lastValue;
        }
        
        public void SetupUI(float value)
        {
            lastValue = value;
            UpdateUI();
        }

        public void UpdateValue(float value)
        {
            onValueChanged?.Invoke(value);
            SetupUI(value);
        }

        private void HandlerValueTextChanged(string value,bool shouldResetIfNotNumber=false)
        {
            if (float.TryParse(value.Replace(".",","), out float parsedValue))
                if (valueSlider.minValue <= parsedValue && parsedValue <= valueSlider.maxValue)
                    UpdateValue(parsedValue);
                else if (shouldResetIfNotNumber)
                    UpdateValue(Mathf.Clamp(parsedValue,valueSlider.minValue,valueSlider.maxValue));
            else if (shouldResetIfNotNumber)
                UpdateUI();
        }

        public void OnValueChanged(string value)
            => HandlerValueTextChanged(value);

        public void OnValueSubmitted(string value)
            => HandlerValueTextChanged(value,shouldResetIfNotNumber:true);

        public void SetBackgroundColor(Color color)
            => valueSliderBackground.material.SetColor("_Color", color);
    }
}
