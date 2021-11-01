using UnityEngine;

namespace For_UI
{
    public class SkinInitializer : MonoBehaviour
    {
        public static Renderer skinRenderer;

        void Start()
        {
            skinRenderer = GetComponentInChildren<Renderer>();
            Invoke("Porno", Time.deltaTime);
        }

        public void Porno()
        {
            Skins.skins[PlayerPrefs.GetInt("currentSkin", 3)].SetSkin();
        }
    }
}