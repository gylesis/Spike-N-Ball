using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListOfTextures : MonoBehaviour
{
    public static ListOfTextures Instance { get; private set; }
    public List<Sprite> Images = new List<Sprite>();

    public SpriteRenderer IconSprt;
    public Text Header;
    public Text Description; 


    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        
    }

}
