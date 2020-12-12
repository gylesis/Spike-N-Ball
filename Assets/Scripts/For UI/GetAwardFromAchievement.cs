using UnityEngine;


public class GetAwardFromAchievement : MonoBehaviour
{
    [HideInInspector]public Transform getAward;
    public static GetAwardFromAchievement Instance { get; set; }
    void Start()
    {
        getAward = GetComponent<Transform>();
        Instance = this;      
        for (int i = 0; i < Achievements.achievements.Count; i++)
        {
            if (Achievements.achievements[i].Achieved) // Imaging achieved achievements
            {
                var obj = GameObject.Find("Achievment" + (i + 1));
                var obj1 = obj.transform.GetChild(0);
                obj1.transform.GetChild(1).gameObject.SetActive(true);
            }
            if (Achievements.achievements[i].Achieved) print("this is ok"+(i+1));
           
            /*if (Achievements.achievements[i].moneyAwarded && Achievements.achievements[i].Achieved) // Imaging moneyAwarded achievements
            {
                var obj = GameObject.Find("Achievment" + (i + 1));             
                getAward = obj.transform.GetChild(4);
                getAward.transform.GetChild(0).gameObject.SetActive(false);
                getAward.transform.GetChild(1).gameObject.SetActive(false);
            }*/
        }
    }
}
