using UnityEngine;
using UnityEngine.UI;

public class Crystal : MonoBehaviour
{
    // void Activation()
    // {
    //      gameObject.GetComponent<SpriteRenderer>().enabled = false;
    // }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Score.Instance.AddingCrystall();
            gameObject.SetActive(false);
        }
       
    }

    private void Start()
    {
        SpawnsCoins();

    }

    //public void AddingScore()
   // {      
   //     Score.Instance.CrystScore += 1;
   //     Score.Instance.CrystalCount.text = Score.Instance.CrystScore.ToString();
   // }

    void SpawnsCoins()
    {
        int rnd = (int)(Random.value * 10);     
        if (rnd > 5 ) 
        {        
            gameObject.SetActive(false);
        }
    }


}
