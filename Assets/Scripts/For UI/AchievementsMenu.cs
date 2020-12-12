using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementsMenu : MonoBehaviour
{
    void Start()
    {
        for (int i = 0; i < Achievements.achievements.Count - 1; i++)
        {
            GameObject Achievement = GameObject.Find("Achievment" + (i + 1));
            Transform Image = Achievement.transform.GetChild(0);

            if (Achievements.achievements[i].Achieved)
            {
                Image.transform.GetChild(1).gameObject.SetActive(true);
                if (!Achievements.achievements[i].moneyAwarded)
                {
                    Transform AwardButton = Achievement.transform.GetChild(4);
                    AwardButton.gameObject.SetActive(true);
                }
            }
            else
            {
                Image.transform.GetChild(1).gameObject.SetActive(false);
            }
        }
    }
}
