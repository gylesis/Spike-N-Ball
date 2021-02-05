using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    [SerializeField]
    Transform playerPosition;

    [SerializeField]
    float provocationRadius;

    [SerializeField]
    float accelerationFactor;

    Vector3 velocity = Vector3.zero;

    private void Start()
    {
        playerPosition = PlayerControl.Instance.transform;
    }

    void Update()
    {
        Vector3 distance = transform.position - playerPosition.position;
        if ((distance).magnitude < provocationRadius)
        {
            Vector3 acceleration = distance.normalized * accelerationFactor;
            velocity = velocity * 0.9f + acceleration * Time.deltaTime;
            transform.Translate(-velocity);
        }
    }
}
