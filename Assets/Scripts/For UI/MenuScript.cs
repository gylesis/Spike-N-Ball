using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuScript : MonoBehaviour 
{
    public static MenuScript Instance { get; private set; }

    public bool GameIsPaused = false;
    public bool GameIsStarted = false;

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
    public GameObject ConfirmPanel;
    public GameObject NotEnoughSign;
  
    public bool ShopIsPressed = false;


    void SetComponentsFromPlayerActive(bool _bool)
    {
        PlayerObject.GetComponent<MeshRenderer>().enabled = _bool;
        PlayerObject.GetComponent<Rigidbody>().isKinematic = !_bool;
        PlayerObject.GetComponent<PlayerControl>().enabled = _bool;
    }
    void Start()
    {
        Instance = this;
        SwipeToPlay.SetActive(true);
        
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
        StartCoroutine(numeratro());
    }

    IEnumerator numeratro()
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

        StartPlatform.SetActive(false);
        MainMenu.SetActive(false);

        SetComponentsFromPlayerActive(false);
       

        PlayButton.SetActive(false);
        SwipeToPlay.SetActive(false);
        //  SwipeToPlaySign.SetActive(false);

        EnviromentAct.Instance.GameIsStarted = false;
        ShopPanel.SetActive(true);

        foreach (Skin skin in Skins.skins)
        {
            skin.Initialize();
        }
        Skins.blue.bought = true;
        ListOfSkins.LoadBoughtSkins();
    }
    public void ShopQuit()
    {
        GameIsPaused = false;
        ShopPanel.SetActive(false);
        PlayButton.SetActive(true);
        StartPlatform.SetActive(true);
        MainMenu.SetActive(true);

        SetComponentsFromPlayerActive(true);
       

        SwipeToPlay.SetActive(true);
        // SwipeToPlaySign.SetActive(true);

        
    }
    public void SettingsRelease()
    {

        GameIsPaused = true;
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
    }
    public void SettingsHideFromMainMenu()
    {
        SettingsObj.SetActive(false);
        
    }
    public void QuitToMainMenu()
    {
        foreach (Achievement achievment in Achievements.achievements)
        {
            achievment.SaveAchievement();
        }
        PlayerPrefs.Save();
        SceneManager.LoadScene(0);
        
        //PlayerControl.Instance.DeathScreen.SetActive(false);
    }
    public void AchievmentsMenuRelease()
    {
        AchievmentMenu.SetActive(true);
        GameIsPaused = true;

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
        EnviromentAct.Instance.GameIsStarted = true;
       
    }   
    public void Pause() 
    {
        PlayerPrefs.Save();
        PauseMenuUI.SetActive(true);     
        GameIsPaused = true;
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
       
    }
}
    
