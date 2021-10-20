using For_UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace For_Unique_Objects
{
    public class Crystal : MonoBehaviour
    {
        [SerializeField] private GameObject _particleSystem;
        
        private void Start()
        {
            SpawnCrystall();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                OnPickup();
            }
        }

        private void OnPickup()
        {
            GameObject particles = Instantiate(_particleSystem,transform.position,Quaternion.identity);
            Destroy(particles, 5 * Time.timeScale);
            
            AudioManager.Instance.PlayCrystalPick(0.02f);
            Score.Instance.AddingCrystall();
            gameObject.SetActive(false);
        }

        void SpawnCrystall()
        {
            int rnd = (int) (Random.value * 10);
            if (rnd > 5)
            {
                gameObject.SetActive(false);
            }
        
        }

    }
}

