using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace For_UI
{
    public class Skin
    {
        public Texture texture;
        public string name;
        public int cost;
        public bool bought;
        public static event Action<Skin> SkinChanged;
        public static Skin CurrentSkin;
        
        public Skin(string _name, Texture _texture, int _cost)
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
            bought = PlayerPrefs.GetInt(name) == 1;
        }

        public void SetSkin()
        {
            Debug.Log("Change skin");   
            SkinInitializer.skinRenderer.material.mainTexture = texture;
            PlayerControl.Instance.trail.sharedMaterial.mainTexture = texture;
            CurrentSkin = this;
            SkinChanged?.Invoke(this);
        }

        public IEnumerator ChangeSkin()
        {
            Debug.Log("ChangeSkin");
            if (bought)
            {
                SetSkin();
                
                for (int i = 0; i < Skins.skins.Count; i++)
                {
                    if (Skins.skins[i].name == name)
                    {
                        PlayerPrefs.SetInt("currentSkin", i);
                    }
                }
            }
            else
            {
                if (Money.Instance.money >= cost)
                {
                    yield return ConfirmationToBuy.Instance.FalseOrTrue();
                    if (ConfirmationToBuy.Instance.answer == 1)
                    {
                        Debug.Log("Kupil");
                        ConfirmationToBuy.Instance.answer = 3;
                        Money.Instance.money -= cost;
                        Money.Instance.UpdateView();
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
        private static readonly Skin Yellow = new Skin("Yellow", ListOfSkins.Instance.skinTextures[0], 300);
        private static readonly Skin DarkBlue = new Skin("DarkBlue", ListOfSkins.Instance.skinTextures[1], 300);
        private static readonly Skin Gradient = new Skin("Gradient", ListOfSkins.Instance.skinTextures[2], 300);
        public static readonly Skin Blue = new Skin("Blue", ListOfSkins.Instance.skinTextures[3], 300);
        private static readonly Skin Transparent = new Skin("Transparent", ListOfSkins.Instance.skinTextures[4], 500);
        private static readonly Skin Wheel = new Skin("Wheel", ListOfSkins.Instance.skinTextures[5], 500);
        private static readonly Skin Stone = new Skin("Stone", ListOfSkins.Instance.skinTextures[6], 700);
        private static readonly Skin Rainbow = new Skin("Rainbow", ListOfSkins.Instance.skinTextures[7], 900);
        private static readonly Skin Orange = new Skin("Orange", ListOfSkins.Instance.skinTextures[8], 500);
        private static readonly Skin Ball = new Skin("Ball", ListOfSkins.Instance.skinTextures[9], 700);
        private static readonly Skin NineLines = new Skin("NineLines", ListOfSkins.Instance.skinTextures[10], 700);
        private static readonly Skin Retro = new Skin("Retro", ListOfSkins.Instance.skinTextures[11], 900);
        private static readonly Skin Earth = new Skin("Earth", ListOfSkins.Instance.skinTextures[12], 900);
        private static readonly Skin JingJang = new Skin("Jing-jang", ListOfSkins.Instance.skinTextures[13], 900);
        private static readonly Skin Gear = new Skin("Gear", ListOfSkins.Instance.skinTextures[14], 500);
        private static readonly Skin Cheese = new Skin("Cheese", ListOfSkins.Instance.skinTextures[15], 700);

        public static readonly List<Skin> skins = new List<Skin>
        {
            Yellow, DarkBlue, Gradient, Blue,
            Transparent, Wheel, Retro,
            Ball, NineLines, Orange, Gear,
            Earth, JingJang, Stone, Rainbow, Cheese
        };
    }
}