using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skin
{
    Texture texture;
    public string name;
    public int cost;
    public bool bought;

    public Skin(string _name, Texture _texture,int _cost)
    {
        cost = _cost;
        texture = _texture;
        name = _name;
    }
     
    void Save()
    {
        
        PlayerPrefs.SetInt(name, bought ? 1 : 0);
        if (name == Skins.skins[Skins.skins.Count - 1].name) PlayerPrefs.Save();
    }

    public void Initialize()
    {
        bought = PlayerPrefs.GetInt(name) == 1 ? true : false;     
    }
    public void SetSkin()
    {
        Skins_MonoBehaviour.skinRenderer.material.mainTexture = texture;
    }
    public IEnumerator ChangeSkin()
    {
        if (bought)
        {
            Skins_MonoBehaviour.skinRenderer.material.mainTexture = texture;
            for(int i = 0; i < Skins.skins.Count - 1; i++)
            {
                if (Skins.skins[i].name == name) 
                {
                    PlayerPrefs.SetInt("currentSkin",i);
                }
            }
        }
        else
        {
            if(Money.Instance.money >= cost)
            {
                yield return ConfirmationToBuy.Instance.FalseOrTrue();
                if (ConfirmationToBuy.Instance.answer == 1)
                {
                    Debug.Log("Kupil");
                    ConfirmationToBuy.Instance.answer = 3;
                    Money.Instance.MoneyCounter.text = Money.Instance.money.ToString();
                    Money.Instance.money -= cost;
                    PlayerPrefs.SetInt("CrystallsScore", Money.Instance.money);
                    bought = true;
                    GameObject.Find(name).transform.GetChild(2).gameObject.SetActive(false);
                    Save();
                }
                else ConfirmationToBuy.Instance.answer = 3;
            }
            else
            {
                ConfirmationToBuy.Instance.answer = 3;
                MenuScript.Instance.NotEnoughSign.SetActive(true);
                Debug.Log("Not Enough Money");
                MenuScript.Instance.StartCarutine();
            }

            
        }
        yield return 0;
    }
}
public static class Skins
{

    public static readonly Skin yellow = new Skin("Yellow", ListOfSkins.Instance.skinTextures[0],300);
    public static readonly Skin darkBlue = new Skin("DarkBlue", ListOfSkins.Instance.skinTextures[1],300);
    public static readonly Skin magenta = new Skin("Magenta", ListOfSkins.Instance.skinTextures[2],300);
    public static readonly Skin blue = new Skin("Blue", ListOfSkins.Instance.skinTextures[3],300);
    public static readonly Skin transparent = new Skin("Transparent", ListOfSkins.Instance.skinTextures[4],500);
    public static readonly Skin wheel = new Skin("Wheel", ListOfSkins.Instance.skinTextures[5],500);
    public static readonly Skin smile = new Skin("Smile", ListOfSkins.Instance.skinTextures[6], 700);
    public static readonly Skin rainbow = new Skin("Rainbow", ListOfSkins.Instance.skinTextures[7], 900);
    public static readonly Skin orange = new Skin("Orange", ListOfSkins.Instance.skinTextures[8], 500); 
    public static readonly Skin ball = new Skin("Ball", ListOfSkins.Instance.skinTextures[9],700);
    public static readonly Skin nineLines = new Skin("NineLines", ListOfSkins.Instance.skinTextures[10],700);
    public static readonly Skin retro  = new Skin("Retro", ListOfSkins.Instance.skinTextures[11], 900);
    public static readonly Skin earth = new Skin("Earth", ListOfSkins.Instance.skinTextures[12],900);
    public static readonly Skin jing_jang = new Skin("Jing-jang", ListOfSkins.Instance.skinTextures[13],900);
    public static readonly Skin gear = new Skin("Gear", ListOfSkins.Instance.skinTextures[14], 500);
    public static readonly Skin cheese = new Skin("Cheese", ListOfSkins.Instance.skinTextures[15], 700);

    public static List<Skin> skins = new List<Skin>
    {
        yellow,darkBlue,magenta,blue,
        transparent,wheel,retro,
        ball,nineLines,orange,gear,
        earth,jing_jang,smile,rainbow,cheese
    };
}
