using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveablePlatform : MonoBehaviour
{
    public bool isStarted {private set; get; } = false;

    public void StartMovement()
    {
        isStarted = true;
    }
}
