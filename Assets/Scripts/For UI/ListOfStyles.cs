using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListOfStyles : MonoBehaviour
{
    public static ListOfStyles Instance { get; private set; }
    public Material[] ListOfMaterials;
    public Material[] ListOfParticles;

    public Material CurrentMaterialForBg;
    public Material CurrentMaterialForWalls;
    public Material CurrentMaterialForSpikes;
    public static Material CurrentParticles;

   

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        foreach (StylesChange style in Styles.Stili)
        {
            style.Initialize();
        }
    }
    public static void LoadBoughtStyles()
    {
        foreach (StylesChange style in Styles.Stili)
        {
            if (style.bought)
            {
                GameObject obj = GameObject.Find(style.name);
             //   Debug.Log("POshel nahui " + style.name);
                obj.transform.GetChild(2).gameObject.SetActive(false);
            }
        }
    }
    


}
