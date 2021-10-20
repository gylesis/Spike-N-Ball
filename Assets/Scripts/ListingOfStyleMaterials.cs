using For_UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class ListingOfStyleMaterials : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] string name;
    public string _styleNameToSwitch;

    public void OnPointerDown(PointerEventData eventData)
    {
        _styleNameToSwitch = eventData.pointerCurrentRaycast.gameObject.name;

        foreach (StylesChange style in Styles.Stili)
        {
            if (style.name == name)
            {
                StartCoroutine(style.ChangeStyle());
            }
        }
    }
}