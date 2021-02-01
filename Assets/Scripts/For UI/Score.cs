using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TextCore;


 public class Score : MonoBehaviour
{
    public static Score Instance { get; private set; }
    [SerializeField] Text ScoreO;
    public Text CrystalCount;
    [SerializeField] Text CollectedCrystalls;
    [SerializeField] Text HighScoreCounter;
  

    public bool BoolForAnimOver = false;
    public bool BoolOnNewHighScore = false;


    [HideInInspector] public int CrystScore = 0; //Overall crystalls
    public int CrystallCount = 0; // Crystall over run
    int Counter; //Previous crystalls
    int NewHighScore = 0;
   
    public int CurrentHighScore = 0;

    float count;
    float PrevCount;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            print("Deleting data...");
            foreach (var gg in Achievements.achievements) {
                gg.Achieved = false;
            }
            PlayerPrefs.DeleteAll();
        }

        HighScoreCount();

        CrystalCount.text = CrystallCount.ToString();
        ScoreCount();            
    }
    private void Awake()
    {
        Instance = this;
        CrystallCount = 0;

        Counter = PlayerPrefs.GetInt("CrystallsScore", 0);   

        CurrentHighScore = PlayerPrefs.GetInt("CurrentHighScore", 0);      
        HighScoreCounter.text = "HIGHSCORE: " + CurrentHighScore;

        

    }
   
    void AnimOfCrystallText()
    {
        if (CrystallCount > 0)
        {
            
            Counter++;
            CrystallCount--;         
            CollectedCrystalls.text = Counter + " + " + CrystallCount;
            
        }
        if (CrystallCount == 0)
        {
            AnimatorController.Instance.Check = true;
            CollectedCrystalls.text = Counter.ToString();
            BoolForAnimOver = true;
            PlayerControl.Instance.BoolOnDeath = false;                    
            
        }
     
    }
    public void WriteCollectedCrystalls()
    {
        Counter = PlayerPrefs.GetInt("CrystallsScore", 0); 
        CrystScore = PlayerPrefs.GetInt("CrystallsScore", 0) + CrystallCount;

        PlayerPrefs.SetInt("CrystallsScore", CrystScore);

        Stats.Instance.AddMetres(NewHighScore);
        Stats.Instance.AddCrystals(CrystallCount);

        if (!BoolForAnimOver)
        {
            InvokeRepeating("AnimOfCrystallText", 1.5f, 10f / CrystallCount);
        }     
        AnimatorController.Instance.Check = false;
    }
    public void AddingCrystall()
    {      
        CrystallCount++;       
    }
    void HighScoreCount()
    {
        if (PlayerControl.Instance.BoolOnDeath)
        {
            if(NewHighScore > CurrentHighScore)
            {
                CurrentHighScore = NewHighScore;
                PlayerPrefs.SetInt("CurrentHighScore", CurrentHighScore);
                BoolOnNewHighScore = true;
                HighScoreCounter.text = "NEW HIGHSCORE: " + CurrentHighScore;

            }
        }

       
    }
    public float ScoreCount()
    {
        count = Mathf.Round(gameObject.transform.localPosition.y/2);
        if (PrevCount < count)
        {
            ScoreO.text = count.ToString();
            NewHighScore = (int)count;
        }
        PrevCount = count;
        return count;
    }

   
}
