using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace FernandoPFH_Essentials_Runtime
{
    public class TexturePickerUIWindow : Singleton<TexturePickerUIWindow>, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Transform contentHolder;
        [SerializeField] private GameObject listOfTexturesPrefab;
        public UnityEvent<Texture2D> onValueChanged = new();

        private Texture2D lastTexture;
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

            SetTexture(lastTexture);
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

        public void SetupUI(Vector2 screenPosition, Texture2D texture, SerializableDictionary<string, System.Collections.Generic.List<Texture2D>> listsOfTextures)
        {
            transform.SetParent(FindFirstObjectByType<Canvas>().transform);
            (transform as RectTransform).anchoredPosition = screenPosition * (new Vector2(1920f, 1080f) / new Vector2(Screen.width, Screen.height));

            foreach (Transform child in contentHolder)
                Destroy(child.gameObject);

            foreach (string textureListName in listsOfTextures.Keys.ToList())
            {
                GameObject listOfTexture = Instantiate(listOfTexturesPrefab, contentHolder);

                listOfTexture.GetComponent<TextureScrollList>().SetupUI(textureListName, listsOfTextures[textureListName], SetTexture);
            }

            gameObject.SetActive(true);

            (transform as RectTransform).localScale = Vector2.one;
            lastTexture = texture;
        }

        public void SetTexture(Texture2D texture2D)
            => onValueChanged.Invoke(texture2D);

        public void OnBeginDrag(PointerEventData eventData)
            => isBeingDragged = true;

        public void OnEndDrag(PointerEventData eventData)
            => isBeingDragged = false;

        public void OnDrag(PointerEventData eventData)
            => (transform as RectTransform).anchoredPosition += eventData.delta * (new Vector2(1920f, 1080f) / new Vector2(Screen.width, Screen.height));

        public void CloseWindow()
        {
            gameObject.SetActive(false);
            onValueChanged = new();
        }
    }
}