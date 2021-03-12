using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StyleShopChanger : MonoBehaviour {

    [SerializeField]
    List<Sprite> stylesImages = new List<Sprite>();

    [SerializeField]
    Image image;

    private void OnEnable() {
        image.sprite = stylesImages[Random.Range(0, stylesImages.Count - 1)];
    }




}
