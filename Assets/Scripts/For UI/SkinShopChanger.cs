using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace For_UI
{
    public class SkinShopChanger : MonoBehaviour {

        [SerializeField]
        List<Sprite> skinSprites = new List<Sprite>();

        [SerializeField]
        Image image;

        private void OnEnable() {
            image.sprite = skinSprites[Random.Range(0, skinSprites.Count - 1)];
        }
    }
}
