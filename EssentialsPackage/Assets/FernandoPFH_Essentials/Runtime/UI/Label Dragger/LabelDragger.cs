using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FernandoPFH_Essentials_Runtime
{
    public class LabelDragger : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private float dragMultiplier = 0.01f;
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private Texture2D dragCursorTexture = null;

        public static bool IsBeingDragged;

        public void OnDrag(PointerEventData eventData)
        {
            if (float.TryParse(inputField.text, out float parsedText))
            {
                parsedText += eventData.delta.x * dragMultiplier * (1920f / Screen.width);

                inputField.text = parsedText.ToString();

                inputField.onValueChanged.Invoke(inputField.text);
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
            => IsBeingDragged = true;

        public void OnEndDrag(PointerEventData eventData)
        {
            IsBeingDragged = false;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }

        public void OnPointerEnter(PointerEventData eventData)
            => Cursor.SetCursor(dragCursorTexture, dragCursorTexture ? new Vector2(dragCursorTexture.width, dragCursorTexture.height) / 2f : Vector2.zero, CursorMode.Auto);

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!IsBeingDragged)
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }
}