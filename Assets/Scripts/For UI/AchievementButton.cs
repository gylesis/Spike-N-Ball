using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace For_UI
{
    public class AchievementButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] Text _awardAmountText;

        [SerializeField] GameObject onGetAwardParticlesPrefab;

        private Achievement _achievement;

        public void Initialize(Achievement achievement)
        {
            _achievement = achievement;

            if (achievement.moneyAwarded)
            {
                gameObject.SetActive(false);
                return;
            }
            
            _awardAmountText.text = achievement.awardAmount.ToString();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _achievement.GetAward();

            Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            point.z = 0;
            
            GameObject particles = Instantiate(onGetAwardParticlesPrefab, point,Quaternion.identity);

            Destroy(particles,2 * Time.timeScale);

            gameObject.SetActive(false);
        }
    }
}