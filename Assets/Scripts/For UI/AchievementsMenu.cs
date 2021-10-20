using System;
using System.Collections.Generic;
using UnityEngine;

namespace For_UI
{
    public class AchievementsMenu : MonoBehaviour
    {
        private readonly List<AchievementView> _achievementViews = new List<AchievementView>();

        [SerializeField] private AchievementView _achievementViewPrefab;

        private void Awake()
        {
            foreach (Achievement achievement in Achievements.achievements)
            {
                AchievementView achievementView = Instantiate(_achievementViewPrefab, transform);
                achievementView.Initialize(achievement);
                _achievementViews.Add(achievementView);
            }
        }

        private void OnEnable()
        {
            foreach (AchievementView achievementView in _achievementViews)
            {
                achievementView.UpdateAchievement();
            }
        }

        private void OnDestroy()
        {
            foreach (Achievement achievement in Achievements.achievements)
            {
                achievement.SaveAchievement();
            }
        }
    }
}