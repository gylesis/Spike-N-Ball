using System;
using System.Collections.Generic;
using UnityEngine;

namespace For_UI
{
    public class Achievement
    {
        private bool achieved;

        public bool Achieved
        {
            get { return achieved; }
            set
            {
                if (achieved != value && isInitialized)
                {
                    achieved = value;
                    Show();
                }
            }
        }

        private stats stat;
        public string header;
        public string text;
        private int thresholdValue;
        private bool accordingToTime;
        private bool isInitialized;
        public bool moneyAwarded;
        public int awardAmount;
        public Sprite sprite;

        public Achievement(stats _stat, int _thresholdValue, string _header, string _text, Sprite _sprite,
            int _awardAmount)
        {
            stat = _stat;
            header = _header;
            text = _text;
            thresholdValue = _thresholdValue;
            sprite = _sprite;
            awardAmount = _awardAmount;
        }

        public Achievement(stats _stat, int _thresholdValue, string _header, string _text, bool _accordingToTime,
            Sprite _sprite, int _awardAmount)
        {
            stat = _stat;
            header = _header;
            text = _text;
            thresholdValue = _thresholdValue;
            accordingToTime = _accordingToTime;
            sprite = _sprite;
            awardAmount = _awardAmount;
        }

        public void Initialize()
        {
            achieved = PlayerPrefs.GetInt(header + " Achieved", 0) != 0;
            moneyAwarded = PlayerPrefs.GetInt(header + " Awarded", 0) != 0;

            isInitialized = true;
        }

        public void Check()
        {
            if (achieved) return;

            if (accordingToTime)
            {
                Achieved = Stats.Instance.GetStat(stat) >= thresholdValue &&
                           Stats.Instance.GetStat(stats.TimePerRun) <= 10;
            }
            else
            {
                var isAchieved = Stats.Instance.GetStat(stat) >= thresholdValue;

                Achieved = isAchieved;
            }
        }

        public void Show()
        {
            SaveAchievement();

            AchievementNotifier.Instance.Show(this);
        }

        public void SaveAchievement()
        {
            PlayerPrefs.SetInt(header + " Achieved", achieved ? 1 : 0);
            PlayerPrefs.SetInt(header + " Awarded", moneyAwarded ? 1 : 0);
        }

        public void GetAward()
        {
            Money.Instance.money += awardAmount;
            moneyAwarded = true;
            SaveAchievement();
            PlayerPrefs.SetInt("CrystallsScore", Money.Instance.money);
        }
    }

    public static class Achievements
    {
        public static readonly Achievement timeBronze = new Achievement(stats.TimeSpend, 600,
            "It looks like you liked it",
            "Play game for 10 minutes", ListOfTextures.Instance.Images[0], 300);

        public static readonly Achievement timeSilver = new Achievement(stats.TimeSpend, 3600, "Still here?",
            "Play game for 1 hour", ListOfTextures.Instance.Images[1], 1000);

        public static readonly Achievement timeGold = new Achievement(stats.TimeSpend, 10800, "Seriously?",
            "Play game for 3 hours", ListOfTextures.Instance.Images[2], 2000);

        public static readonly Achievement CrystalBronze = new Achievement(stats.Crystals, 100, "Initial Capital",
            "Collect 100 crystals overall", ListOfTextures.Instance.Images[6], 50);

        public static readonly Achievement CrystalSilver = new Achievement(stats.Crystals, 1000, "Crystal Fever",
            "Collect 1000 crystals overall", ListOfTextures.Instance.Images[7], 150);

        public static readonly Achievement CrystalGold = new Achievement(stats.Crystals, 3000, "Rich B**ch",
            "Collect 3000 crystals overall", ListOfTextures.Instance.Images[8], 400);

        public static readonly Achievement MetresBronze = new Achievement(stats.Metres, 1000, "First Steps",
            "Overcome 1000 meters overall", ListOfTextures.Instance.Images[3], 100);

        public static readonly Achievement MetresSilver = new Achievement(stats.Metres, 5000, "Next Steps",
            "Overcome 5000 meters overall", ListOfTextures.Instance.Images[4], 500);

        public static readonly Achievement MetresGold = new Achievement(stats.Metres, 10000, "Last Steps",
            "Overcome 10000 meters overall", ListOfTextures.Instance.Images[5], 2000);

        public static readonly Achievement HighscoreBronze = new Achievement(stats.MetresPerRun, 50, "Olympian",
            "Score more than 50 points", ListOfTextures.Instance.Images[10], 100);

        public static readonly Achievement HighscoreSilver = new Achievement(stats.MetresPerRun, 100, "Almost Everest",
            "Score more than 100 points", ListOfTextures.Instance.Images[11], 500);

        public static readonly Achievement HighscoreGold = new Achievement(stats.MetresPerRun, 250, "Ball God",
            "Score more than 250 points", ListOfTextures.Instance.Images[12], 2000);

        public static readonly Achievement TheBeginning = new Achievement(stats.TimeSpend, 10, "Hello world!",
            "Play for 10 seconds", ListOfTextures.Instance.Images[9], 100);

        public static readonly Achievement CrystalsPerTime = new Achievement(stats.CrystalsPerRun, 15, "Speedrun",
            "Collect 15 crystals in 10 first seconds", true, ListOfTextures.Instance.Images[2], 500);

        //public static readonly Achievement Suicide = new Achievement(stats.TimePerRun, 1, "Suicide", "Die after first jump");
        public static readonly List<Achievement> achievements = new List<Achievement>()
        {
            TheBeginning,
            timeBronze, timeSilver,
            timeGold, CrystalsPerTime, CrystalBronze,
            CrystalSilver, CrystalGold, MetresBronze,
            MetresSilver, MetresGold, HighscoreBronze, HighscoreSilver,
            HighscoreGold
        };
    }
}