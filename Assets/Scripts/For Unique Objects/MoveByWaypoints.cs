using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveByWaypoints : MoveablePlatform
{
    public float speed;
    public float WaitingTime;
    public bool StopOnlyAtTheEdges;
    public bool incremental;

    Vector3 direction;
    Transform MoveablePlat;
    float CurrentWaitingTime;

    List<Transform> Waypoints = new List<Transform> { };
    int MoveTo;

    void Start()
    {
        InitializeListOfWaypoints();
        AllignTriggerBox(0.2f);
    }

    void Update()
    {
        if (EnviromentAct.Instance.GameIsStarted && isStarted) Move();
    }

    private void InitializeListOfWaypoints()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).CompareTag("MoveablePlatform"))
            {
                MoveablePlat = transform.GetChild(i);
            }
            if (transform.GetChild(i).CompareTag("Waypoint"))
            {
                Waypoints.Add(transform.GetChild(i));
            }
        }
    }

    private void AllignTriggerBox(float offset)
    {
        BoxCollider[] colliders = MoveablePlat.GetComponents<BoxCollider>();
        Vector3 scale = MoveablePlat.localScale;
        foreach (BoxCollider collider in colliders)
        {
            if (collider.isTrigger)
            {
                Vector3 newSize;
                newSize.x = 1 - offset / scale.x;
                newSize.y = 1 - offset / scale.y;
                newSize.z = 1 - offset / scale.z;
                collider.size = newSize;
            }
        }
    }

    private void Move()
    {
        if (CurrentWaitingTime > 0)
        {
            CurrentWaitingTime -= Time.deltaTime;
        }
        else
        {            
            direction = Waypoints[MoveTo].position - MoveablePlat.position;
            MoveablePlat.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
            if (direction.magnitude < 0.1 && Waypoints.Count - 1 == MoveTo)
            {
                incremental = false;
                if (StopOnlyAtTheEdges) CurrentWaitingTime = WaitingTime;
            }
            else if (direction.magnitude < 0.1 && 0 == MoveTo)
            {
                incremental = true;
                if (StopOnlyAtTheEdges) CurrentWaitingTime = WaitingTime;
            }
            if (direction.magnitude < 0.1 && incremental)
            {
                MoveTo++;
                if(!StopOnlyAtTheEdges) CurrentWaitingTime = WaitingTime;
            }
            else if (direction.magnitude < 0.1 && !incremental)
            {
                MoveTo--;
                if (!StopOnlyAtTheEdges) CurrentWaitingTime = WaitingTime;
            }
        }
    }
}
