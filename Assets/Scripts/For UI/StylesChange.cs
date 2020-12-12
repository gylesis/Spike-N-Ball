using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StylesChange : MonoBehaviour
{
    private Material Bg;
    private Material Walls;
    public Material Spikes;
    public Material Particles;
    public bool bought;
    private int cost;
    public string name;

    public StylesChange(Material _BG, Material _Walls, Material _Spikes, string _name, int _cost)
    {
        Bg = _BG;
        Walls = _Walls;
        Spikes = _Spikes;
        name = _name;
        cost = _cost;
    }
    public StylesChange(Material _BG, Material _Walls, Material _Spikes, Material _Particles, string _name, int _cost)
    {
        Bg = _BG;
        Walls = _Walls;
        Spikes = _Spikes;
        name = _name;
        Particles = _Particles;
        cost = _cost;
    }  
    public void Initialize()
    {
        bought = PlayerPrefs.GetInt(name, 0) == 1 ? true : false;
    }
    public void SaveStyle()
    {
        PlayerPrefs.SetInt(name, bought ? 1 : 0);
    }
    
    public IEnumerator ChangeStyle()
    {
        Debug.Log("sstart");
        if (bought)
        {
            ListOfStyles.Instance.CurrentMaterialForBg.color = Bg.color;
            ListOfStyles.Instance.CurrentMaterialForWalls.color = Walls.color;
            ListOfStyles.Instance.CurrentMaterialForSpikes.color = Spikes.color;
            ListOfStyles.CurrentParticles = Particles;


           /* for (int i = 0; i < Styles.Stili.Count - 1; i++)
            {             
                if (Styles.Stili[i].Particles == Particles)
                {
                    PlayerPrefs.SetInt("currentParticles", ListOfStyles.CurrentParticles ? 1) ;
                }
            }*/
        }
        else
        {
            if (Money.Instance.money >= cost)
            {
                Debug.Log("Hvatilo");
                yield return ConfirmationToBuy.Instance.FalseOrTrue();
                if (ConfirmationToBuy.Instance.answer == 1)
                {                  
                    Debug.Log("Kupil");
                    ConfirmationToBuy.Instance.answer = 3;
                    Money.Instance.MoneyCounter.text = Money.Instance.money.ToString();
                    Money.Instance.money -= cost;
                    PlayerPrefs.SetInt("CrystallsScore", Money.Instance.money);
                    bought = true;                    
                    SaveStyle();
                    GameObject.Find(name).transform.GetChild(2).gameObject.SetActive(false);
                }
                else ConfirmationToBuy.Instance.answer = 3;               
            }
            else
            {
                MenuScript.Instance.NotEnoughSign.SetActive(true);
                Debug.Log("Not Enough Money");
                MenuScript.Instance.StartCarutine();
            }
        }
    }
    
   
}

public static class Styles
{
    public static StylesChange Field = new StylesChange(ListOfStyles.Instance.ListOfMaterials[9], ListOfStyles.Instance.ListOfMaterials[10], ListOfStyles.Instance.ListOfMaterials[11], "Field", 500);
    //public static StylesChange Ice = new StylesChange(ListOfStyles.Instance.ListOfMaterials[6], ListOfStyles.Instance.ListOfMaterials[7], ListOfStyles.Instance.ListOfMaterials[8],"Field");
    public static StylesChange Default = new StylesChange(ListOfStyles.Instance.ListOfMaterials[0], ListOfStyles.Instance.ListOfMaterials[1], ListOfStyles.Instance.ListOfMaterials[2], ListOfStyles.Instance.ListOfParticles[1], "Default", 500);

    public static StylesChange Jungle = new StylesChange(ListOfStyles.Instance.ListOfMaterials[15], ListOfStyles.Instance.ListOfMaterials[16], ListOfStyles.Instance.ListOfMaterials[17], "Jungle", 750);
    public static StylesChange Vampire = new StylesChange(ListOfStyles.Instance.ListOfMaterials[18], ListOfStyles.Instance.ListOfMaterials[19], ListOfStyles.Instance.ListOfMaterials[20], "Vampire", 750);
    public static StylesChange Ice = new StylesChange(ListOfStyles.Instance.ListOfMaterials[6], ListOfStyles.Instance.ListOfMaterials[7], ListOfStyles.Instance.ListOfMaterials[8], ListOfStyles.Instance.ListOfParticles[0], "Ice", 750);

    public static StylesChange Futuristic = new StylesChange(ListOfStyles.Instance.ListOfMaterials[12], ListOfStyles.Instance.ListOfMaterials[13], ListOfStyles.Instance.ListOfMaterials[14], "Futuristic", 1000);
    public static StylesChange Sea = new StylesChange(ListOfStyles.Instance.ListOfMaterials[3], ListOfStyles.Instance.ListOfMaterials[4], ListOfStyles.Instance.ListOfMaterials[5], ListOfStyles.Instance.ListOfParticles[2], "Sea", 1000);
    public static StylesChange Magic = new StylesChange(ListOfStyles.Instance.ListOfMaterials[21], ListOfStyles.Instance.ListOfMaterials[22], ListOfStyles.Instance.ListOfMaterials[23], "Magic", 1000);
    public static StylesChange RGB = new StylesChange(ListOfStyles.Instance.ListOfMaterials[24], ListOfStyles.Instance.ListOfMaterials[25], ListOfStyles.Instance.ListOfMaterials[26], "RGB", 500);

    //public static StylesChange Ice = new StylesChange(ListOfStyles.Instance.ListOfMaterials[6], ListOfStyles.Instance.ListOfMaterials[7], ListOfStyles.Instance.ListOfMaterials[8],"Field");

    public static List<StylesChange> Stili = new List<StylesChange>()
    {
        Field,Default,Jungle,Vampire,Ice,Futuristic,Sea,Magic,RGB
    };

}

