using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
public class AnimatorController : MonoBehaviour
{
    public static AnimatorController Instance { get; private set; }
    public GameObject AnimOfPauseButton;
    public GameObject MenuAnim;
    [SerializeField] Animator MainMenuPlayAnimash;
    public Animator CrystallsAnimation;
    [SerializeField] Animator NewHighScoreAnim;
    public Animator TextCounterAnim;
    [SerializeField] ParticleSystem CrystallsParticles;
    [SerializeField] ParticleSystem.EmissionModule CrystallsParticles_Emission;
    public Animator PlateReveal;


    public bool Check = false;

    private void Start()
    {
        Instance = this;
        CrystallsParticles_Emission = CrystallsParticles.emission;

    }
    void slow()
    {
        PlateReveal.SetBool("achiv", false);
    }
    public void slow2()
    {
        Invoke("slow", 3f);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }

        if (Check)
        {
            TextCounterAnim.SetBool("TextScale", true);           
        }
        if (EnviromentAct.Instance.BoolForAnim)
        {
            MainMenuPlayAnimash.SetBool("MainMenuPlayAnimation", true);                  
        }
        if (Score.Instance.BoolOnNewHighScore)
        {
            NewHighScoreAnim.SetBool("HighScoreReached", true);
        }
        
        if (Score.Instance.BoolForAnimOver)
        {
            
            CrystallsParticles_Emission.rate = 0;
        }else CrystallsParticles.playbackSpeed = 0.05f * Mathf.Clamp(Score.Instance.CrystallCount, Score.Instance.CrystallCount + 50, 100) / 50;


       

       
    }  
     
}

