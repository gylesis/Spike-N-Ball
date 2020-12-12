using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievement
{
    public Action OnAchievedChanged;
    private bool achieved;
    public bool Achieved
    {
        get { return achieved; }
        set
        {
            
            if (achieved != value && !achieved && isInitialized) 
            {
                achieved = value;
                OnAchievedChanged();
                Debug.Log("OnAchievedChanged");
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
    private int awardAmount;
    private Sprite sprite;

    public Achievement(stats _stat, int _thresholdValue, string _header, string _text, Sprite _sprite, int _awardAmount)
    {
        stat = _stat;
        header = _header;
        text = _text;
        thresholdValue = _thresholdValue;
        sprite = _sprite;
        awardAmount = _awardAmount;
    }
    public Achievement(stats _stat, int _thresholdValue, string _header, string _text,bool _accordingToTime,Sprite _sprite, int _awardAmount)
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
        achieved = PlayerPrefs.GetInt(header + " Achieved", 0) == 0 ? false : true;
        moneyAwarded = PlayerPrefs.GetInt(header + " Awarded", 0) == 0 ? false : true;
        isInitialized = true;
    }

    public void Check()
    {
        OnAchievedChanged = Show;
        if (accordingToTime)
        {
            Achieved = (Stats.Instance.GetStat(stat) >= 5 && Stats.Instance.GetStat(stats.TimePerRun) <= 20);
        }
        else
        {
            Achieved = (Stats.Instance.GetStat(stat) >= thresholdValue && !achieved);
        }
    }
    
    public void Show()
    {
        Debug.Log("dsds");
        ListOfTextures.Instance.IconSprt.sprite = sprite;
        ListOfTextures.Instance.Header.text = header;
        ListOfTextures.Instance.Description.text = text;
        AnimatorController.Instance.PlateReveal.SetBool("achiv", true);

        AnimatorController.Instance.slow2();
        //AnimatorController.Instance.PlateReveal.SetBool("achiv", false);

    }
    public void SaveAchievement()
    {
        PlayerPrefs.SetInt(header + " Achieved", achieved ? 1 : 0);
        PlayerPrefs.SetInt(header + " Awarded", moneyAwarded ? 1 : 0);
    }
    public virtual void SaveAchievement(string saved)
    {
        if ("Achieved" == saved) PlayerPrefs.SetInt(header + " Achieved", achieved ? 1 : 0);
        else PlayerPrefs.SetInt(header + " Awarded", moneyAwarded ? 1 : 0);
    }
    public void GetAward()
    {
        Debug.Log(Money.Instance.money);
        Money.Instance.money += awardAmount;
        moneyAwarded = true;
        SaveAchievement("Только сохрани эти самые ну ты понял");
        PlayerPrefs.SetInt("CrystallsScore", Money.Instance.money);
    }
}

public static class Achievements
{
    public static readonly Achievement timeBronze = new Achievement(stats.TimeSpend, 5, "It looks like you liked it", "Play game during 10 minutes", ListOfTextures.Instance.Images[0],300);
    public static readonly Achievement timeSilver = new Achievement(stats.TimeSpend, 30, "Still here?", "Play game during 1 hour", ListOfTextures.Instance.Images[1],1500);
    public static readonly Achievement timeGold = new Achievement(stats.TimeSpend, 50, "Seriously?", "Play game during 5 hours", ListOfTextures.Instance.Images[2],2000);

    public static readonly Achievement CrystalBronze = new Achievement(stats.Crystals, 100, "Initial Capital", "Collect 100 crystals overall", ListOfTextures.Instance.Images[6],50);
    public static readonly Achievement CrystalSilver = new Achievement(stats.Crystals, 1000, "Crystal Fever", "Collect 1000 crystals overall", ListOfTextures.Instance.Images[7],150);
    public static readonly Achievement CrystalGold = new Achievement(stats.Crystals, 3000, "Rich B**ch", "Collect 5000 crystals overall", ListOfTextures.Instance.Images[8],400);

    public static readonly Achievement MetresBronze = new Achievement(stats.Metres, 1000, "First Steps", "Overcome 1000 meters overall", ListOfTextures.Instance.Images[3],100);
    public static readonly Achievement MetresSilver = new Achievement(stats.Metres, 5000, "Next Steps", "Overcome 5000 meters overall", ListOfTextures.Instance.Images[4],500);
    public static readonly Achievement MetresGold = new Achievement(stats.Metres, 10000, "Last Steps", "Overcome 10000 meters overall", ListOfTextures.Instance.Images[5],2000);

    public static readonly Achievement CrystalsPerTime = new Achievement(stats.Crystals, 10, "Speedrun", "Coollect 10 crystals in 20 seconds", true, ListOfTextures.Instance.Images[2],500);
  //public static readonly Achievement Suicide = new Achievement(stats.TimePerRun, 1, "Suicide", "Die after first jump");

    public static List<Achievement> achievements = new List<Achievement>()
    {
    timeBronze,
        timeSilver, timeGold,
    CrystalBronze, CrystalSilver, CrystalGold,
    MetresBronze ,MetresSilver , MetresGold,

    CrystalsPerTime
    };



}

