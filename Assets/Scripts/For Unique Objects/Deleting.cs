using UnityEngine;

namespace For_Unique_Objects
{
    public class Deleting : MonoBehaviour
    {
        public static Deleting Instance { get; private set; }

        [HideInInspector]public Animator animator;
        private void Awake()
        {
            animator = GetComponent<Animator>();
        }
        public void Delete()
        {
            gameObject.SetActive(false);
        }
    }
}
