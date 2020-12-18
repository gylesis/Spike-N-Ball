using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EnviromentAct : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler
{
    public static EnviromentAct Instance { get; private set; }

    public GameObject Points;
    public float Speed = 2f;
    bool CheckGameForStart = false;
    public bool GameIsStarted = false;
    public bool BoolForAnim = false;


    bool isStartBegan;

    
    [SerializeField] GameObject PlayButton;
    [SerializeField] Animator SwipeToPlay;

    private void Awake()
    {
        Instance = this;

    }


    public void StartGame()
    {
        BoolForAnim = true;
        CheckGameForStart = true;
        PlayButton.SetActive(false);
    }

    public void TextSwipe()
    {
        SwipeToPlay.SetTrigger("Triggered");    
    }
    void Update()
    {
        if (GameIsStarted) Stats.Instance.AddTimeSpend(Time.deltaTime / Time.timeScale);
    }

    void ScoreReveal()
    {
        Points.SetActive(true);
        Score.Instance.CrystalCount.gameObject.SetActive(true);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject.tag == "Start" && !GameIsStarted)
        {
            StartGame();
            MusicControl.Instance.MusicStart();
            TextSwipe();
            isStartBegan = true;
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (isStartBegan)
        {
            GameIsStarted = true;
            ScoreReveal();
            isStartBegan = false;
        }

    }

   public void OnPointerEnter(PointerEventData eventData)
    {
       
       /* print(eventData.pointerCurrentRaycast.gameObject.name);
        if (eventData.pointerCurrentRaycast.gameObject.name == "PauseImage") 
        { 
           print("Touched");
           Joystick.Instance.OnIcon = true;
        }else Joystick.Instance.OnIcon = false;*/
    }
}