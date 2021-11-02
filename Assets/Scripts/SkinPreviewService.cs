using For_UI;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class SkinPreviewService : MonoBehaviour
    {
        [SerializeField] private RawImage _skinView;

        private void Awake()
        {
            var currentSkin = Skin.CurrentSkin;
            Texture skinTexture = currentSkin.texture;

            UpdateView(skinTexture);
            
            Skin.SkinChanged += OnSkinChanged;
        }

        private void OnSkinChanged(Skin skin)
        {
            Texture skinTexture = skin.texture;
            UpdateView(skinTexture);
        }

        private void UpdateView(Texture texture) => 
            _skinView.texture = texture;
    }
}