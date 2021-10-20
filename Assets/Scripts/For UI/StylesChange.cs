using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace For_UI
{
    public class StylesChange
    {
        private Material _background;
        private Material Walls;
        public Material Spikes;
        public Material Particles;
        public bool bought;
        private int cost;
        public string name;
        public bool isChoosen;

        public StylesChange(Material background, Material walls, Material spikes, string name, int cost)
        {
            _background = background;
            Walls = walls;
            Spikes = spikes;
            this.name = name;
            this.cost = cost;
        }

        public StylesChange(Material background, Material walls, Material spikes, Material particles, string name,
            int cost)
        {
            _background = background;
            Walls = walls;
            Spikes = spikes;
            this.name = name;
            Particles = particles;
            this.cost = cost;
        }

        public void Initialize()
        {
            if (name == "Field") bought = true;
            else bought = PlayerPrefs.GetInt(name, 0) == 1;

            isChoosen = PlayerPrefs.GetInt(name + "1", 0) == 1;

            if (isChoosen)
            {
                ListOfStyles.CurrentParticles = Particles;
                ListOfStyles.Instance.ChangeMaterialColor(_background.color,Walls.color,Spikes.color,Particles);
            }
        }

        public void SaveStyle()
        {
            PlayerPrefs.SetInt(name, bought ? 1 : 0);
        }

        public IEnumerator ChangeStyle()
        {
            if (bought)
            {
                ListOfStyles.Instance.ChangeMaterialColor(_background.color,Walls.color,Spikes.color,Particles);
                
                foreach (var style in Styles.Stili)
                {
                    if (style.name == name)
                    {
                        style.isChoosen = true;
                    }
                    else
                    {
                        style.isChoosen = false;
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
                        ConfirmationToBuy.Instance.answer = 3;
                        Money.Instance.money -= cost;
                        Money.Instance.UpdateView();
                        PlayerPrefs.SetInt("CrystallsScore", Money.Instance.money);

                        bought = true;
                        SaveStyle();
                        GameObject.Find(name).transform.GetChild(2).gameObject.SetActive(false);

                        /*foreach (var style in Styles.Stili)
                        {
                            if (style.name == name)
                            {
                                style.isChoosen = true;
                            }
                            else
                            {
                                style.isChoosen = false;
                            }
                        }*/
                    }
                    else ConfirmationToBuy.Instance.answer = 3;
                }
                else
                {
                    MenuScript.Instance.NotEnoughSign.SetActive(true);
                    MenuScript.Instance.StartCarutine();
                }
            }

            foreach (var style in Styles.Stili)
            {
                PlayerPrefs.SetInt(style.name + "1", style.isChoosen ? 1 : 0);
            }

            PlayerPrefs.Save();
        }
    }


    public static class Styles
    {
        private static readonly StylesChange Field = new StylesChange(ListOfStyles.Instance.ListOfMaterials[9],
            ListOfStyles.Instance.ListOfMaterials[10], ListOfStyles.Instance.ListOfMaterials[11], "Field", 500);

        private static readonly StylesChange Default = new StylesChange(ListOfStyles.Instance.ListOfMaterials[0],
            ListOfStyles.Instance.ListOfMaterials[1], ListOfStyles.Instance.ListOfMaterials[2],
            ListOfStyles.Instance.ListOfParticles[1], "Default", 500);

        private static readonly StylesChange Jungle = new StylesChange(ListOfStyles.Instance.ListOfMaterials[15],
            ListOfStyles.Instance.ListOfMaterials[16], ListOfStyles.Instance.ListOfMaterials[17], "Jungle", 700);

        private static readonly StylesChange Vampire = new StylesChange(ListOfStyles.Instance.ListOfMaterials[18],
            ListOfStyles.Instance.ListOfMaterials[19], ListOfStyles.Instance.ListOfMaterials[20], "Vampire", 700);

        private static readonly StylesChange Ice = new StylesChange(ListOfStyles.Instance.ListOfMaterials[6],
            ListOfStyles.Instance.ListOfMaterials[7], ListOfStyles.Instance.ListOfMaterials[8],
            ListOfStyles.Instance.ListOfParticles[0], "Ice", 700);

        private static readonly StylesChange Futuristic = new StylesChange(ListOfStyles.Instance.ListOfMaterials[12],
            ListOfStyles.Instance.ListOfMaterials[13], ListOfStyles.Instance.ListOfMaterials[14], "Futuristic", 1000);

        private static readonly StylesChange Sea = new StylesChange(ListOfStyles.Instance.ListOfMaterials[3],
            ListOfStyles.Instance.ListOfMaterials[4], ListOfStyles.Instance.ListOfMaterials[5],
            ListOfStyles.Instance.ListOfParticles[2], "Sea", 1000);

        private static readonly StylesChange Magic = new StylesChange(ListOfStyles.Instance.ListOfMaterials[21],
            ListOfStyles.Instance.ListOfMaterials[22], ListOfStyles.Instance.ListOfMaterials[23], "Magic", 1000);

        private static readonly StylesChange RGB = new StylesChange(ListOfStyles.Instance.ListOfMaterials[24],
            ListOfStyles.Instance.ListOfMaterials[25], ListOfStyles.Instance.ListOfMaterials[26], "RGB", 500);

        public static List<StylesChange> Stili = new List<StylesChange>()
        {
            Field, Default, Jungle, Vampire, Ice, Futuristic, Sea, Magic, RGB
        };
    }
}