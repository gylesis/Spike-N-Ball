using UnityEngine;

namespace For_UI
{
    public class SkinInitializer : MonoBehaviour
    {
        public static Renderer skinRenderer;

        void Start()
        {
            skinRenderer = GetComponentInChildren<Renderer>();
            Invoke(nameof(InitSkin), Time.deltaTime);
        }

        public void InitSkin()
        {
            var skinIndex = PlayerPrefs.GetInt("currentSkin", 3);
            Skin skin = Skins.skins[skinIndex];
            skin.SetSkin();
        }
    }
}