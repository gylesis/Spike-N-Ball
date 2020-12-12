using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
 public enum stats
 {
     Metres, Jumps, Crystals, Attempts, TimeSpend, 
     MetresPerRun, CrystalsPerRun, TimePerRun
 }

public class Stats : MonoBehaviour
{
    public static Stats Instance { private set; get; }
    private int _metres;
    private int _jumps;
    private int _crystals;
    private int _attempts;
    private float _timeSpend;

    private int _metresPerRun;
    private float _timePerRun;
    private int _crystalsPerRun;

    private void Start()
    {
        Instance = this;
        InitializeStats();
        foreach (Achievement achievement in Achievements.achievements)
        {
            achievement.Initialize();
        }
    }

    void InitializeStats()
    {
        _metres = PlayerPrefs.GetInt("metres", 0);
        _jumps = PlayerPrefs.GetInt("jumps", 0);
        _crystals = PlayerPrefs.GetInt("crystals", 0);
        _attempts = PlayerPrefs.GetInt("attempts", 0);
        _timeSpend = PlayerPrefs.GetFloat("timeSpend", 0);
         _timePerRun = 0;
         _crystalsPerRun = 0;
    }

    public void AddMetres(int metres)
    {
        _metres += metres;
    }
    public void AddJumps(int jumps)
    {
        _jumps += jumps;
    }
    public void AddCrystals(int crystals)
    {
        _crystals += crystals;
    }
    public void AddAttempts(int attempts)
    {
        _attempts += attempts;
    }
    public void AddTimeSpend(float timeSpend)
    {
        _timeSpend += timeSpend;
        _timePerRun += timeSpend;
    }
    public void TimePerRunZero()
    {
        _timePerRun = 0;
    }
    public void CrystalsPerRunEqualTo (int crystalsPerRun)
    {
        _crystalsPerRun = crystalsPerRun;
    }
    public void MetresPerRunEqualTo(int MetresPerRun)
    {
        _metresPerRun = MetresPerRun;
    }
    public float GetStat(stats stat)
    {
        switch (stat)
        {
            case stats.Attempts:
                return _attempts;
            case stats.Crystals:
                return _crystals;
            case stats.Jumps:
                return _jumps;
            case stats.Metres:
                return _metres;
            case stats.TimeSpend:
                return _timeSpend;
        }
        return 404;
    }

    public void SaveStats()
    {
        PlayerPrefs.SetInt("metres", _metres);
        PlayerPrefs.SetInt("jumps", _jumps);
        PlayerPrefs.SetInt("crystals", _crystals);
        PlayerPrefs.SetInt("attempts", _attempts);
        PlayerPrefs.SetFloat("timeSpend", _timeSpend);
    }

    private void Update()
    {
        foreach (Achievement achievement in Achievements.achievements)
        {
            achievement.Check();
        }
    }
}
