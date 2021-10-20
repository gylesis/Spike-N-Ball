using System.Collections;
using System.Collections.Generic;
using For_UI;
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
       Rend.material.mainTexture = SkinInitializer.skinRenderer.material.mainTexture;
    }
}
