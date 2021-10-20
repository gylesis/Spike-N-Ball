using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace For_Unique_Objects
{
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
}