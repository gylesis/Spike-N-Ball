using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skins_MonoBehaviour : MonoBehaviour
{
    public static Renderer skinRenderer;
    void Start()
    {
        skinRenderer = GetComponent<Renderer>();
        Invoke("Porno", Time.deltaTime);
    }

    public void Porno()
    {
        Skins.skins[PlayerPrefs.GetInt("currentSkin", 3)].SetSkin();
    }

}