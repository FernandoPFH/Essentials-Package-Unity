using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace FernandoPFH_Essentials_Runtime
{
    public class TexturePickerUI : MonoBehaviour
    {
        [SerializeField] private Image texturePreview;
        [SerializeField] private GameObject windowPrefab;

        public SerializableDictionary<string, List<Texture2D>> ListsOfTextures;
        public UnityEvent<Texture2D> onValueChanged = new();

        private Texture2D texture;

        void Awake()
            => texture = ListsOfTextures[ListsOfTextures.Keys.First()].First();

        void Start()
            => SetTexture(texture);

        public void SetTexture(Texture2D texture)
        {
            texturePreview.sprite = Sprite.Create(
                texture,
                new Rect(0.0f, 0.0f, texture.width, texture.height),
                new Vector2(0.5f, 0.5f),
                100.0f
            );

            this.texture = texture;
        }

        public void HandlePress()
        {
            if (!TexturePickerUIWindow.Instance)
                Instantiate(windowPrefab);

            Vector2 mousePos = Mouse.current.position.ReadValue();

            TexturePickerUIWindow.Instance.SetupUI(new(mousePos.x, -mousePos.y), texture, ListsOfTextures);

            TexturePickerUIWindow.Instance.onValueChanged.AddListener(UpdateTexture);
        }

        private void UpdateTexture(Texture2D texture)
        {
            texturePreview.sprite = Sprite.Create(
                texture,
                new Rect(0.0f, 0.0f, texture.width, texture.height),
                new Vector2(0.5f, 0.5f),
                100.0f
            );
            onValueChanged.Invoke(texture);
        }
    }
}