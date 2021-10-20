using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace For_UI
{
    public class AchievementView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _header;
        [SerializeField] private TMP_Text _description;

        [SerializeField] private Image _icon;

        [SerializeField] private AchievementButton _achievementButton;
        private Achievement _achievement;

        public void Initialize(Achievement achievement)
        {
            _achievement = achievement;
            _header.text = achievement.header;
            _description.text = achievement.text;

            _achievementButton.Initialize(achievement);
            
            UpdateAchievement();
        }

        public void UpdateAchievement()
        {
            if (_achievement.Achieved)
            {
                _icon.sprite = _achievement.sprite;
                
                _icon.gameObject.SetActive(true);

                if (_achievement.moneyAwarded == false)
                {
                    _achievementButton.gameObject.SetActive(true);
                }
            }

            else
            {
                _icon.gameObject.SetActive(false);
                _achievementButton.gameObject.SetActive(false);
            }
            
        }
        
    }
}