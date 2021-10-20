using System.Collections;
using UnityEngine;

namespace For_UI
{
    public class ConfirmationToBuy : MonoBehaviour
    {
        public byte answer = 3;
        bool _answer;
        public static ConfirmationToBuy Instance { get; set; }
        private void Start()
        {
            Instance = this;
        }
        public void SetActive(GameObject obj, bool bule)
        {
            obj.SetActive(bule);
        }

        public void GetConfrimation(bool bule)
        {
            if (bule) answer = 1;
            else answer = 0;
            MenuScript.Instance.ConfirmPanel.SetActive(false);
        }
        public IEnumerator FalseOrTrue()
        {
            MenuScript.Instance.ConfirmPanel.SetActive(true);
            yield return waitForAnswerGet();
            yield return ReloadNumbers();
        }
        private IEnumerator ReloadNumbers()
        {
            if (answer == 1) MenuScript.Instance.ConfirmPanel.SetActive(false);
            else if (answer == 0) MenuScript.Instance.ConfirmPanel.SetActive(false);    
            yield return null;      
            print("ovesdfsdfdsfsdf");
        }
        private IEnumerator waitForAnswerGet()
        {
            bool done = false;
            while (!done) 
            {
            
                if (answer == 0 || answer == 1)
                {
                    done = true; 
                }
                yield return null; 
            }
        } 

    
    
    
    }
}
