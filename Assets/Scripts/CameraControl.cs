using For_UI;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public static CameraControl Instance { get; private set; }

    public float Speed;
    public Score score;
    private Transform player;
    bool check1;
    private const float baseSize = 2.8125f;
    public float deathDistance;
    public GameObject deathCollider;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (Application.isMobilePlatform)
        {
            var resolution = Screen.currentResolution;
            var screenRatio = (float) resolution.height / resolution.width;
            GetComponent<Camera>().orthographicSize = baseSize * screenRatio;
            deathCollider.transform.position = new Vector3(0, -deathDistance * screenRatio, 0);
        }

        player = PlayerControl.Instance.transform;
    }

    void CameraMove()
    {
        float scoreCoef = Mathf.Min(score.ScoreCount(), 250)  / 800f;
        transform.Translate(0, (Speed + scoreCoef) * Time.deltaTime, 0);

        float distanceFromCamera = transform.position.y - player.position.y;

        if (distanceFromCamera < -0)
        {
            Speed = Mathf.Max(Mathf.Pow(distanceFromCamera - 1, 2) / 10, 0.2f);
        }
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