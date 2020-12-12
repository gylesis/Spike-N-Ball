using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkinButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] string name;
    

   public void OnPointerDown(PointerEventData eventData)
    {
       
        foreach (Skin skin in Skins.skins)
        {
            if (skin.name == name)
            {
                print(skin.name);
                StartCoroutine(skin.ChangeSkin());
            }

        }

    }
}
