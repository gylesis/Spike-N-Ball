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


    [SerializeField] GameObject point_left;
    [SerializeField] GameObject point_right;
    [SerializeField] GameObject circle;
    [SerializeField] Text sign_skins;
    [SerializeField] Text sign_styles;
    [SerializeField] GameObject button_to_switch;
    public bool current_side = true;
 


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
    public void SwitchPanel()
    {
        if (current_side)
        {
            StartCoroutine(MoveCircleRight());
        }
        else StartCoroutine(MoveCircleLeft());
    }
    public IEnumerator MoveCircleLeft()
    {
        MenuScript.Instance.ChangeToSkinMenu();
        button_to_switch.SetActive(false);
        while (circle.transform.position.x > point_left.transform.position.x && circle.transform.position.x > point_left.transform.position.x + 0.05f)
        {      
            sign_styles.color -= new Color(0, 0, 0, 0.14f);
            sign_skins.color += new Color(0, 0, 0, 0.14f);
            circle.transform.position -= new Vector3(0.1f, 0, 0);
            yield return new WaitForSeconds(0.1f);
        }
        current_side = true;
        button_to_switch.SetActive(true);
    }

     public IEnumerator MoveCircleRight()
     {
        MenuScript.Instance.ChangeToStyleMenu();       
        button_to_switch.SetActive(false);
        while (circle.transform.position.x < point_right.transform.position.x && circle.transform.position.x < point_right.transform.position.x - 0.05f )
        {
             
            sign_skins.color -= new Color(0,0,0,0.14f);
            sign_styles.color += new Color(0, 0, 0, 0.14f);
            circle.transform.position += new Vector3(0.1f, 0, 0);
            yield return new WaitForSeconds(0.1f);
        }
        current_side = false;
        button_to_switch.SetActive(true);
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