using UnityEngine;

namespace For_UI
{
    public class GetAwardFromAchievement : MonoBehaviour
    {
        [HideInInspector]public Transform getAward;
        public static GetAwardFromAchievement Instance { get; set; }
        /*void Start()
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
           
            }
        }*/
    }
}
