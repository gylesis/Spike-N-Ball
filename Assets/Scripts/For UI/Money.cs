using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace For_UI
{
    public class Money : MonoBehaviour
    {
        public static Money Instance { get; set; }
        [SerializeField] private List<Text> MoneyCounter;
        public int money = 0;

        private void Start()
        {
            Instance = this;
            money = PlayerPrefs.GetInt("CrystallsScore", 0);
            
            
        }

        private void Update()
        {
            UpdateView();
        }

        public void UpdateView()
        {
            foreach (var text in MoneyCounter)
            {
                text.text = money.ToString();
            }
        }
    }
}
