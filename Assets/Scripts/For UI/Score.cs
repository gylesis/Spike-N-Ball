using System;
using UnityEngine;
using UnityEngine.UI;

namespace For_UI
{
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

        public int CurrentHighScore = 0;

        float count;
        float maxCount;

        private Action IncreaseDifficultyTo2;
        private Action IncreaseDifficultyTo3;
        public Action OnScoreChange;
        public Action OnSignificantScoreChange;

        private const int MaxHighscoreToKostya = 80;
        private const int ScoreFor2NdDifficulty = 20;
        private const int ScoreFor3RdDifficulty = 40;


        int CurrentScore
        {
            get => (int)count;
            set
            {
                var shiftToMax = (1 - (float) CurrentHighScore / MaxHighscoreToKostya);
                if (value >= ScoreFor2NdDifficulty * shiftToMax) IncreaseDifficultyTo2();
                if (value >= ScoreFor3RdDifficulty * shiftToMax) IncreaseDifficultyTo3();
                count = value;
            }
            
        }

        Action callOnce(Action action)
        {
            var alreadyCalled = new alreadyCalled();
            return () =>
            {
                if (alreadyCalled.flag) return;
                action();
                alreadyCalled.flag = true;
            };
        }
        struct alreadyCalled
        {
            public bool flag;
        }

        private void Start()
        {
            IncreaseDifficultyTo2 = callOnce(() => OnScoreChange());
            IncreaseDifficultyTo3 = callOnce(() => OnSignificantScoreChange());
        }

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

            Stats.Instance.CrystalsPerRunEqualTo(CrystallCount);
            Stats.Instance.MetresPerRunEqualTo(CurrentHighScore);
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

            Stats.Instance.AddMetres(CurrentScore);
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
                if(CurrentScore > CurrentHighScore)
                {
                    CurrentHighScore = CurrentScore;
                    PlayerPrefs.SetInt("CurrentHighScore", CurrentHighScore);
                    BoolOnNewHighScore = true;
                    HighScoreCounter.text = "NEW HIGHSCORE: " + CurrentHighScore;
                }
                else
                {
                    HighScoreCounter.text = "YOUR SCORE : " + CurrentScore;
                }
            }
        }
        public float ScoreCount()
        {
            var height = Mathf.Round(gameObject.transform.localPosition.y/2);
            if (maxCount < height)
            {
                maxCount = height;
                CurrentScore = (int)height;
                ScoreO.text = CurrentScore.ToString();
            }
            
            return count;
        }

   
    }
}
