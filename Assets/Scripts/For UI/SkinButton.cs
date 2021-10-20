using UnityEngine;
using UnityEngine.EventSystems;

namespace For_UI
{
    public class SkinButton : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] string name;


        public void OnPointerDown(PointerEventData eventData)
        {
            foreach (Skin skin in Skins.skins)
            {
                if (skin.name == name)
                {
                    StartCoroutine(skin.ChangeSkin());
                }
            }
        }
    }
}