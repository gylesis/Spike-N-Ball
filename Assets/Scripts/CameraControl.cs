using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public static CameraControl Instance { get; private set; }

    public float Speed;   
    public Score score;
    private Transform player;
    bool check1;
    

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        
        player = PlayerControl.Instance.transform;
       // Screen.SetResolution(1080, 1976, true);
    }

    void CameraMove()
    {
        float scoreCoef = score.ScoreCount() / 800f;
        transform.Translate(0, (Speed + scoreCoef) * Time.deltaTime, 0);

        if (transform.position.y - player.position.y < -2.5)
            Speed = (player.position.y - transform.position.y)/10;
        else
            Speed = 0.2f;
        
    }     
    void Update()
    {
        
        if (EnviromentAct.Instance.GameIsStarted)
        {
            
            CameraMove();
        }
     
    }
}
