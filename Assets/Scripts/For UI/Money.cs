using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    public static Money Instance { get; set; }
    public Text MoneyCounter;
    public int money = 0;

    private void Start()
    {
        Instance = this;
        money = PlayerPrefs.GetInt("CrystallsScore", 0);
        Debug.LogFormat("I have {1} crystals",money);
    }
    void Update()
    {
        MoneyCounter.text = money.ToString();
    }
}
