using For_Unique_Objects;
using UnityEngine;
using UnityEngine.EventSystems;

namespace For_UI
{
    public class EnviromentAct : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler
    {
        public static EnviromentAct Instance { get; private set; }

        public GameObject Points;
        public float Speed = 2f;
        bool CheckGameForStart = false;
        public bool GameIsStarted = false;
        public bool BoolForAnim = false;
        [SerializeField] private GameObject FreeAd;
        
        public bool bigcock;

        public bool isStartBegan;

    
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
            if (eventData.pointerCurrentRaycast.gameObject.CompareTag("Start") && !GameIsStarted && !PlayerControl.Instance.BoolOnDeath)
            {
                bigcock = true;
                StartGame();
                MusicControl.Instance.MusicStart();
                FreeAd.GetComponent<EaseMover>().Move(FreeAd.GetComponent<RectTransform>());
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
}