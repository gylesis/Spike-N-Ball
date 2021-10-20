using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListOfTextures : MonoBehaviour
{
    public static ListOfTextures Instance { get; private set; }
    public List<Sprite> Images = new List<Sprite>();

    private void Awake()
    {
        Instance = this;
    }
    
}