using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Customization : MonoBehaviour
{
    public static Customization Instance { get; private set; }
    [SerializeField] Texture[] CurrentTexture;
    [SerializeField] Material[] ListOfMaterials;
    [SerializeField]Material CurrentMaterialForBg;
    public Material CurrentMaterialForWalls;
    [SerializeField]Material CurrentMaterialForSpikes;
    [SerializeField] ParticleSystem.MainModule CrystallPickUp;
    [SerializeField] ParticleSystem mainMod;
    
    public int ID_particle;

    [SerializeField]int IndexOfCurrentTexture = 0;
    private int IndexOfCurrentStyleBG;
    private int IndexOfCurrentStyleWalls;
    private int IndexOfCurrentStyleSpikes;

    Vector2[] OffsetOfTexture;
    Vector2[] TilingOfTexture;

    Color BgGreenTheme = new Color(43, 130, 96);
    Color WallsGreenTheme = new Color(21,107, 73);
    Color DefaultWallsColorTheme = new Color(200, 147, 35);
    Color DefaultBgColorTheme = new Color(221,171,93);
    [HideInInspector] public Color SeaWallsColor = new Color(67,143,166);
    Color SeaBgColor = new Color();
    [HideInInspector]public Renderer Cr;  
    
    
    [SerializeField]Material Mater;
    private void Awake()
    {
        Instance = this;

    }

    private void Start()
    {
        CrystallPickUp = mainMod.main;
        Cr = GetComponent<Renderer>();
        IndexOfCurrentTexture = PlayerPrefs.GetInt("Current skin", 0);

        IndexOfCurrentStyleBG = PlayerPrefs.GetInt("Current styleBG", 0);
        IndexOfCurrentStyleWalls = PlayerPrefs.GetInt("Current styleWalls", 0);
        IndexOfCurrentStyleSpikes = PlayerPrefs.GetInt("Current styleSpikes", 0);

        Cr.material.mainTexture = CurrentTexture[IndexOfCurrentTexture];
       // CirclePaticle.color = ListOfMaterials[IndexOfCurrentStyleWalls].color;

        ID_particle = PlayerPrefs.GetInt("Current particleID", 0);     
    }

    #region StyleChange
   
   /* public void ChangeToSea()
    {
      //  int Cost = ;


        ID_particle = 2;

        IndexOfCurrentStyleBG = 3;
        IndexOfCurrentStyleWalls = 4;
        IndexOfCurrentStyleSpikes = 5;

        CurrentMaterialForBg.color = ListOfMaterials[IndexOfCurrentStyleBG].color;
        CurrentMaterialForWalls.color = ListOfMaterials[IndexOfCurrentStyleWalls].color;
        CurrentMaterialForSpikes.color = ListOfMaterials[IndexOfCurrentStyleSpikes].color;

        // CrystallPickUp.startColor = ListOfMaterials[IndexOfCurrentStyleWalls].color;
        PlayerPrefs.SetInt("Current particleID", ID_particle);
        PlayerPrefs.SetInt("Current styleBG", IndexOfCurrentStyleBG);
        PlayerPrefs.SetInt("Current styleWalls", IndexOfCurrentStyleWalls);
        PlayerPrefs.SetInt("Current styleSpikes", IndexOfCurrentStyleSpikes);
        SceneManager.LoadScene(0);
    }
    public void ChangeToDefault()
    {
        ID_particle = 0;

        IndexOfCurrentStyleBG = 0;
        IndexOfCurrentStyleWalls = 1;
        IndexOfCurrentStyleSpikes = 2;

        CurrentMaterialForBg.color = ListOfMaterials[IndexOfCurrentStyleBG].color;
        CurrentMaterialForWalls.color = ListOfMaterials[IndexOfCurrentStyleWalls].color;
        CurrentMaterialForSpikes.color = ListOfMaterials[IndexOfCurrentStyleSpikes].color;


        //CrystallPickUp.startColor = ListOfMaterials[IndexOfCurrentStyleWalls].color;
        PlayerPrefs.SetInt("Current particleID", ID_particle);
        PlayerPrefs.SetInt("Current styleBG", IndexOfCurrentStyleBG);
        PlayerPrefs.SetInt("Current styleWalls", IndexOfCurrentStyleWalls);
        PlayerPrefs.SetInt("Current styleSpikes", IndexOfCurrentStyleSpikes);
    }
    public void ChangeToIce()
    {

        ID_particle = 1;

        IndexOfCurrentStyleBG = 6;
        IndexOfCurrentStyleWalls = 7;
        IndexOfCurrentStyleSpikes = 8;

        CurrentMaterialForBg.color = ListOfMaterials[IndexOfCurrentStyleBG].color;
        CurrentMaterialForWalls.color = ListOfMaterials[IndexOfCurrentStyleWalls].color;
        CurrentMaterialForSpikes.color = ListOfMaterials[IndexOfCurrentStyleSpikes].color;
    
     
        PlayerPrefs.SetInt("Current particleID", ID_particle);
        PlayerPrefs.SetInt("Current styleBG", IndexOfCurrentStyleBG);
        PlayerPrefs.SetInt("Current styleWalls", IndexOfCurrentStyleWalls);
        PlayerPrefs.SetInt("Current styleSpikes", IndexOfCurrentStyleSpikes);
        SceneManager.LoadScene(0);
    }
    public void ChangeToVampire()
    {
       // ID_particle = 0;

        IndexOfCurrentStyleBG = 18;
        IndexOfCurrentStyleWalls = 19;
        IndexOfCurrentStyleSpikes = 20;

        CurrentMaterialForBg.color = ListOfMaterials[IndexOfCurrentStyleBG].color;
        CurrentMaterialForWalls.color = ListOfMaterials[IndexOfCurrentStyleWalls].color;
        CurrentMaterialForSpikes.color = ListOfMaterials[IndexOfCurrentStyleSpikes].color;


        //CrystallPickUp.startColor = ListOfMaterials[IndexOfCurrentStyleWalls].color;
        PlayerPrefs.SetInt("Current particleID", ID_particle);
        PlayerPrefs.SetInt("Current styleBG", IndexOfCurrentStyleBG);
        PlayerPrefs.SetInt("Current styleWalls", IndexOfCurrentStyleWalls);
        PlayerPrefs.SetInt("Current styleSpikes", IndexOfCurrentStyleSpikes);
    }
    public void ChangeToJungle()
    {
       // ID_particle = 0;

        IndexOfCurrentStyleBG = 15;
        IndexOfCurrentStyleWalls = 16;
        IndexOfCurrentStyleSpikes = 17;

        CurrentMaterialForBg.color = ListOfMaterials[IndexOfCurrentStyleBG].color;
        CurrentMaterialForWalls.color = ListOfMaterials[IndexOfCurrentStyleWalls].color;
        CurrentMaterialForSpikes.color = ListOfMaterials[IndexOfCurrentStyleSpikes].color;


        //CrystallPickUp.startColor = ListOfMaterials[IndexOfCurrentStyleWalls].color;
        PlayerPrefs.SetInt("Current particleID", ID_particle);
        PlayerPrefs.SetInt("Current styleBG", IndexOfCurrentStyleBG);
        PlayerPrefs.SetInt("Current styleWalls", IndexOfCurrentStyleWalls);
        PlayerPrefs.SetInt("Current styleSpikes", IndexOfCurrentStyleSpikes);
    }
    public void ChangeToFuturistic()
    {
       // ID_particle = 0;

        IndexOfCurrentStyleBG = 12;
        IndexOfCurrentStyleWalls = 13;
        IndexOfCurrentStyleSpikes = 14;

        CurrentMaterialForBg.color = ListOfMaterials[IndexOfCurrentStyleBG].color;
        CurrentMaterialForWalls.color = ListOfMaterials[IndexOfCurrentStyleWalls].color;
        CurrentMaterialForSpikes.color = ListOfMaterials[IndexOfCurrentStyleSpikes].color;


        //CrystallPickUp.startColor = ListOfMaterials[IndexOfCurrentStyleWalls].color;
        PlayerPrefs.SetInt("Current particleID", ID_particle);
        PlayerPrefs.SetInt("Current styleBG", IndexOfCurrentStyleBG);
        PlayerPrefs.SetInt("Current styleWalls", IndexOfCurrentStyleWalls);
        PlayerPrefs.SetInt("Current styleSpikes", IndexOfCurrentStyleSpikes);
    }
    public void ChangeToField()
    {
       // ID_particle = 0;

        IndexOfCurrentStyleBG = 9;
        IndexOfCurrentStyleWalls = 10;
        IndexOfCurrentStyleSpikes = 11;

        CurrentMaterialForBg.color = ListOfMaterials[IndexOfCurrentStyleBG].color;
        CurrentMaterialForWalls.color = ListOfMaterials[IndexOfCurrentStyleWalls].color;
        CurrentMaterialForSpikes.color = ListOfMaterials[IndexOfCurrentStyleSpikes].color;


        //CrystallPickUp.startColor = ListOfMaterials[IndexOfCurrentStyleWalls].color;
        PlayerPrefs.SetInt("Current particleID", ID_particle);
        PlayerPrefs.SetInt("Current styleBG", IndexOfCurrentStyleBG);
        PlayerPrefs.SetInt("Current styleWalls", IndexOfCurrentStyleWalls);
        PlayerPrefs.SetInt("Current styleSpikes", IndexOfCurrentStyleSpikes);
    }*/
    #endregion
    #region SkinChange 
    public void ChangeToBlue()
    {
        IndexOfCurrentTexture = 0;
        Cr.material.mainTexture = CurrentTexture[IndexOfCurrentTexture];
        PlayerPrefs.SetInt("Current skin", IndexOfCurrentTexture);
    }
    public void ChangeToLeopard()
    {
        IndexOfCurrentTexture = 3;
        Cr.material.mainTexture = CurrentTexture[IndexOfCurrentTexture];        
        PlayerPrefs.SetInt("Current skin", IndexOfCurrentTexture);

    }
    public void ChangeToPink()
    {
        IndexOfCurrentTexture = 5;
        Cr.material.mainTexture = CurrentTexture[IndexOfCurrentTexture];
        PlayerPrefs.SetInt("Current skin", IndexOfCurrentTexture);
    }
    public void ChangeToYellow()
    {
        IndexOfCurrentTexture = 2;
        Cr.material.mainTexture = CurrentTexture[IndexOfCurrentTexture];
        PlayerPrefs.SetInt("Current skin", IndexOfCurrentTexture);
    }
    public void ChangeToCorona1()
    {
        IndexOfCurrentTexture = 1;
        Cr.material.mainTexture = CurrentTexture[IndexOfCurrentTexture];
        PlayerPrefs.SetInt("Current skin", IndexOfCurrentTexture);
    }
    public void ChangeToCorona2()
    {
        IndexOfCurrentTexture = 4;
        Cr.material.mainTexture = CurrentTexture[IndexOfCurrentTexture];
        PlayerPrefs.SetInt("Current skin", IndexOfCurrentTexture);
    }
    public void ChangeToInvis()
    {
        IndexOfCurrentTexture = 6;
        Cr.material.mainTexture = CurrentTexture[IndexOfCurrentTexture];
        PlayerPrefs.SetInt("Current skin", IndexOfCurrentTexture);
    }
    public void ChangeToChinaCoin()
    {
        IndexOfCurrentTexture = 8;
        Cr.material.mainTexture = CurrentTexture[IndexOfCurrentTexture];
        PlayerPrefs.SetInt("Current skin", IndexOfCurrentTexture);
    }
    public void ChangeToSalad()
    {
        IndexOfCurrentTexture = 7;
        Cr.material.mainTexture = CurrentTexture[IndexOfCurrentTexture];
        PlayerPrefs.SetInt("Current skin", IndexOfCurrentTexture);
    }
    #endregion





}
