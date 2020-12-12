using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChangeStyle : MonoBehaviour,IPointerClickHandler
{    
    public int Money;
    public bool[] _isBought;
    [SerializeField] Text MoneyCounter;
  

    private void Awake()
    {
        Money = PlayerPrefs.GetInt("CrystallsScore", 0);
    }
    void Update()
    {
        MoneyCounter.text = Money.ToString();
    }
  

    public void OnPointerClick(PointerEventData eventData)
    {
        //eventData.pointerCurrentRaycast.gameObject.GetComponent<Material>().
    }

    
}