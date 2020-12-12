using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSkin : MonoBehaviour
{
    [SerializeField] Texture[] ListOfTextures;
    [SerializeField] int IndexOfCurrentTexture;

    public void ChangingSkin(int _IndexOfCurrentTexture)
    {
        IndexOfCurrentTexture = _IndexOfCurrentTexture;
    }

   

}
