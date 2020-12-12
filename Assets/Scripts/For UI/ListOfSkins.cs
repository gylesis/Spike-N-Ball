using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListOfSkins : MonoBehaviour
{
    public static ListOfSkins Instance { get; private set; }
    public Texture[] skinTextures;

    [SerializeField]GameObject _objToFindSkins; //Find skins easier
    void Start()
    {
        Instance = this;
    }
    public static void LoadBoughtSkins()
    {
        foreach (Skin skin in Skins.skins)
        {
            if (skin.bought)
            {
                GameObject obj = GameObject.Find(skin.name);
                obj.transform.GetChild(2).gameObject.SetActive(false);
            }
        }
    }


}
