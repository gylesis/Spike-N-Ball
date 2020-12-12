using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinLook : MonoBehaviour
{
    Renderer Rend;
    void Start()
    {
        Rend = GetComponent<Renderer>();
    }

    
    void Update()
    {
       Rend.material.mainTexture = Skins_MonoBehaviour.skinRenderer.material.mainTexture;
    }
}
