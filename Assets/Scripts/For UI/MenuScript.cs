using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace For_UI
{
    public class MenuScript : MonoBehaviour
    {
        public static MenuScript Instance { get; private set; }

        public static bool GameIsPaused = false;
        public static bool GameIsStarted = false;

        [SerializeField] GameObject PauseMenuUI;
        [SerializeField] GameObject pauseButton;
        [SerializeField] GameObject SettingsObj;
        [SerializeField] GameObject ShopPanel;

        [SerializeField] GameObject StartPlatform;
        [SerializeField] GameObject PlayerObject;
        [SerializeField] GameObject SwipeToPlay;
        [SerializeField] GameObject MainMenu;
        [SerializeField] GameObject PlayButton;

        [SerializeField] GameObject StyleMenu;
        [SerializeField] GameObject SkinMenu;

        [SerializeField] GameObject AchievmentMenu;
        [SerializeField] private GameObject Instruction;
        [SerializeField] private GameObject AchievementNotification;
        
        
        public GameObject ConfirmPanel;
        public GameObject NotEnoughSign;

        [SerializeField] private GameObject HighScoreMainScreen;
        

        public bool ShopIsPressed = false;

        public event Action<bool> OnGamePause;

        void SetComponentsFromPlayerActive(bool _bool)
        {
            PlayerObject.SetActive(_bool);
        }

        private void Awake()
        {
            Instance = this;
        }

        private void OnEnable()
        {
            GameIsPaused = false;
        }

        void Start()
        {
            ActivateGuide();
            ActivateAchievementNotification();
            
            SwipeToPlay.SetActive(true);
            HighScoreMainScreen.GetComponent<Text>().text = "highscore : " + Score.Instance.CurrentHighScore;
        }

        private void ActivateAchievementNotification()
        {
            var anyReward = false;
            foreach (var achievement in Achievements.achievements)
            {
                if (!achievement.moneyAwarded && achievement.Achieved)
                {
                    anyReward = true;
                }
            }

            AchievementNotification.SetActive(anyReward);
            AchievementNotification.transform.DOMoveY(AchievementNotification.transform.position.y + 0.3f,0.75f).SetLoops(-1 ,LoopType.Yoyo);
        }

        private void ActivateGuide()
        {
            if (PlayerPrefs.GetInt("isFirstStart") == 0)
            {
                Instruction.SetActive(true);
                PlayerPrefs.SetInt("isFirstStart", 1);
            }
        }

        public void OnClickGame()
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        public void StartCarutine()
        {
            StartCoroutine(elNumeratro());
        }

        IEnumerator elNumeratro()
        {
            yield return new WaitForSecondsRealtime(1f);
            NotEnoughSign.SetActive(false);
        }

        public void ChangeToStyleMenu()
        {
            StyleMenu.SetActive(true);
            SkinMenu.SetActive(false);
            ListOfStyles.LoadBoughtStyles();
        }

        public void ChangeToSkinMenu()
        {
            StyleMenu.SetActive(false);
            SkinMenu.SetActive(true);
        }

        public void ShopRelease()
        {
            GameIsPaused = true;

            OnGamePause?.Invoke(GameIsPaused);

            StartPlatform.SetActive(false);
            MainMenu.SetActive(false);

            SetComponentsFromPlayerActive(false);


            PlayButton.SetActive(false);
            SwipeToPlay.SetActive(false);

            EnviromentAct.Instance.GameIsStarted = false;
            ShopPanel.SetActive(true);

            
            foreach (Skin skin in Skins.skins)
            {
                skin.Initialize();
            }
            Skins.Blue.bought = true;
            ListOfSkins.LoadBoughtSkins();
        }

        public void ShopQuit()
        {
            GameIsPaused = false;
            OnGamePause?.Invoke(GameIsPaused);

            ShopPanel.SetActive(false);
            PlayButton.SetActive(true);
            StartPlatform.SetActive(true);
            MainMenu.SetActive(true);

            SetComponentsFromPlayerActive(true);


            SwipeToPlay.SetActive(true);
        }

        public void SettingsRelease()
        {
            GameIsPaused = true;

            OnGamePause?.Invoke(GameIsPaused);
            SettingsObj.SetActive(true);
            EnviromentAct.Instance.GameIsStarted = false;
            PlayButton.SetActive(false);
        }

        public void SettingsHide()
        {
            SettingsObj.SetActive(false);
            if (GameIsPaused)
            {
                EnviromentAct.Instance.GameIsStarted = false;
                PlayButton.SetActive(true);
                GameIsPaused = false;
            }
            else
            {
                EnviromentAct.Instance.GameIsStarted = true;
                GameIsPaused = false;
            }

            OnGamePause?.Invoke(GameIsPaused);
        }

        public void SettingsHideFromMainMenu()
        {
            SettingsObj.SetActive(false);
        }

        public void QuitToMainMenu()
        {
            PlayerPrefs.Save();
            SceneManager.LoadScene(0);
        }

        public void AchievmentsMenuRelease()
        {
            AchievementNotification.SetActive(false);
            AchievmentMenu.SetActive(true);
            GameIsPaused = true;

            OnGamePause?.Invoke(GameIsPaused);
            StartPlatform.SetActive(false);
            MainMenu.SetActive(false);
            PlayerObject.SetActive(false);
            PlayButton.SetActive(false);
            SwipeToPlay.SetActive(false);

            EnviromentAct.Instance.GameIsStarted = false;
        }

        public void AchievmentsMenuQuit()
        {
            AchievmentMenu.SetActive(false);
            GameIsPaused = false;

            OnGamePause?.Invoke(GameIsPaused);
            PlayButton.SetActive(true);
            StartPlatform.SetActive(true);
            MainMenu.SetActive(true);
            PlayerObject.SetActive(true);

            SwipeToPlay.SetActive(true);
        }

        public void Resume()
        {
            PauseMenuUI.SetActive(false);
            GameIsPaused = false;
            OnGamePause?.Invoke(GameIsPaused);
            EnviromentAct.Instance.GameIsStarted = true;
        }

        public void Pause()
        {
            PlayerPrefs.Save();
            PauseMenuUI.SetActive(true);
            GameIsPaused = true;
            OnGamePause?.Invoke(GameIsPaused);
            EnviromentAct.Instance.GameIsStarted = false;
        }

        private void Update()
        {
            if (EnviromentAct.Instance.GameIsStarted)
            {
                pauseButton.SetActive(true);
            }
            else
            {
                pauseButton.SetActive(false);
            }

            if (Input.GetKeyUp(KeyCode.Escape))
            {
                var BackButtons = GameObject.FindGameObjectsWithTag("BackButton");
                foreach (var backButton in BackButtons)
                {
                    if (backButton.activeSelf) backButton.GetComponent<Button>().onClick.Invoke();
                }
            } 
        }
    }
}