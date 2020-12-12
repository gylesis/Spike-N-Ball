using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MoveablePlatform
{
    [SerializeField] private float speed;
    [SerializeField] private bool rotateInfinitely;
    [SerializeField] private float treshold;

    private float clockWiseCoef;
    private float currentTreshold;
    private float currentRotation;
    private float primaryRotation;

    private void Start()
    {
        primaryRotation = transform.rotation.eulerAngles.z;
        currentRotation = primaryRotation;
        currentTreshold = treshold;
        if (!rotateInfinitely)
        {
            float difference = treshold - primaryRotation;
            clockWiseCoef = difference / Mathf.Abs(difference);
        }

    }

    void Update()
    {
        if (isStarted) Rotate();
    }

    private void Rotate()
    {
        if (rotateInfinitely)
            transform.Rotate(0, 0, speed * Time.deltaTime * clockWiseCoef);
        else
        {
            if (clockWiseCoef > 0)
            {
                //Вращение против часовой стрелки
                RotateByCondition(currentRotation < currentTreshold);
            }
            else
            {
                //Вращение по часовой стрелки
                RotateByCondition(currentRotation > currentTreshold);
            }
        }
    }

    private void RotateByCondition(bool condition)
    {
        if (condition)
        {
            float rotationZ = speed* Time.deltaTime* clockWiseCoef;
            transform.rotation *= Quaternion.Euler(0, 0, rotationZ);
            currentRotation += rotationZ;
        }
        else
        {
            clockWiseCoef *= -1;

            if (currentTreshold != treshold)
            {
                currentTreshold = treshold;
            }
            else
            {
                currentTreshold = primaryRotation;
            }
        }
    }
}
