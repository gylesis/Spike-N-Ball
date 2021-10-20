using UnityEngine;

namespace For_Unique_Objects
{
    public class MoveablePlatform : MonoBehaviour
    {
        public bool isStarted {private set; get; } = false;

        public void StartMovement()
        {
            isStarted = true;
        }
    }
}
