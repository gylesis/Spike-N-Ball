using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace For_UI
{
    public class Instruction : MonoBehaviour
    {
        [SerializeField] private GameObject Hand;
        [SerializeField] private GameObject Ball;
        [SerializeField] private GameObject Arrow;
        [SerializeField] private Transform ArrowRotation;
        [SerializeField] private GameObject Level1;
        [SerializeField] private GameObject Level2;
        [SerializeField] private TMP_Text Explanation;
    
        private Sequence JumpAnimation;
        private Sequence SlideAnimation;
    
        private bool _toBeClosed = false;

        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        void OnEnable()
        {
            _toBeClosed = false;
        
            JumpAnimation = DOTween.Sequence();
            StartAnimation(JumpAnimation,
                new Vector3(1,1,0) * 35,
                new Vector3(1,1,0) * 35,
                Quaternion.Euler(Vector3.forward * 45),
                "Tap in any place and pull it up to jump");
        }

        private void OnClick()
        {
            if (_toBeClosed)
            {
                StopAnimation(SlideAnimation, false);
                gameObject.SetActive(false);
            }
            else
            {
                ChangeAnimation();
            }
            _toBeClosed = true;
        }

        private void ChangeAnimation()
        {
            StopAnimation(JumpAnimation, true);

            SlideAnimation = DOTween.Sequence();
            StartAnimation(SlideAnimation,
                new Vector3(1, -0.2f, 0) * 35,
                Vector3.right * 35,
                Quaternion.Euler(Vector3.forward * -5),
                "Aim the ball to the wall or platform to slide");
        }

        private void StartAnimation(Sequence animation, Vector3 handDisplacement, Vector3 ballDisplacement, Quaternion arrowRotation, string explanation)
        {
            Explanation.text = explanation;
            ArrowRotation.rotation = arrowRotation;

            var handPos = Hand.transform.localPosition;
            var ballPos = Ball.transform.localPosition;
        
            animation.SetLoops(-1, LoopType.Restart);

            animation.Append(Hand.transform.DOLocalMove(handPos - handDisplacement, 1));
            animation.Append(Hand.transform.DOScale(35, 0.5f));
            animation.Append(Hand.transform.DOLocalMove(handPos + handDisplacement * 2, 0.5f));
            animation.Join(Arrow.transform.DOScaleY(50, 0.5f));
            animation.Append(Hand.transform.DOScale(40, 0.5f));
            animation.Join(Arrow.transform.DOScaleY(0, 0f));
            animation.Join(Ball.transform.DOLocalMove(ballPos + ballDisplacement * 5.143f, 0.3f));
            animation.Append(Hand.transform.DOLocalMove(handPos, 1));
        }

        private void StopAnimation(Tween animation, bool activeObstacles)
        {
            animation.Goto(0);
            animation.Kill();
            ChangeObstacles(activeObstacles);
        }

        private void ChangeObstacles(bool active)
        {
            Level1.SetActive(!active);
            Level2.SetActive(active);
        }
    }
}
