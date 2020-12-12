using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Vector2 StartPos;
    public Vector2 direction;
    public float speed;
    public float koef;
    public bool isGrounded=true;
    public float maxForce;
    public float maxSpeed;
    public float maxDist = 0.51f;
    public LayerMask Wall;
    public LayerMask Floor;
    public Collider[] hitColliders;
    public bool check;
    private bool PostGrounded;
    public float TrailFadeCoef;
    private TrailRenderer trail;
    public int score;
    public GameObject scoreField;
    public GameObject Arrow;
    public GameObject RotationCenter;
    private TextMeshProUGUI scr;

   

    void Start()
    {
        scr = scoreField.GetComponent<TextMeshProUGUI>();
        rb = GetComponent<Rigidbody>();
        trail = GetComponent<TrailRenderer>();
        rb.AddForce(new Vector3(0,1000,0));
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, maxDist);
    }
    void TrailControl()
    {
        if (isGrounded)
        {
            if (!(trail.time <= 0)) trail.time -= Time.deltaTime * TrailFadeCoef;      
        }
        else
        {
            trail.time = 0.4f;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        rb.velocity = Vector3.zero;
    }
    private void ChangeArrow()
    {
        if (Input.GetKey(KeyCode.Mouse0) && isGrounded || Input.GetKey(KeyCode.Mouse0) )
        {
            if (direction.y > 0)
            {
                RotationCenter.transform.eulerAngles = new Vector3(0, 0, -Mathf.Asin(direction.x) * 180 / Mathf.PI);
            }
            else
            {
                RotationCenter.transform.eulerAngles = new Vector3(0, 0, 180 + Mathf.Asin(direction.x) * 180 / Mathf.PI);
            }
            Arrow.transform.localPosition = new Vector3(0, speed / 900 * 2.5f, 0);
            Arrow.transform.localScale = new Vector3(0.1f, speed / 900, 0.1f);
            Arrow.SetActive(true);
        }
        else
        {
            Arrow.SetActive(false);
            Invoke("SpeedZero", 0.001f);
        }        
    }
    void CountVel()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartPos = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            Vector2 CurrentPos = Input.mousePosition;
            direction = StartPos - CurrentPos;  
            speed = direction.magnitude;
            speed = Mathf.Clamp(speed,0,maxForce);
            direction.Normalize();
        }

        if (Input.GetMouseButtonUp(0) )
        {
            print("d");
            rb.AddForce(direction * speed * koef, ForceMode.Impulse);
        }
            rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x,-maxSpeed, maxSpeed), Mathf.Clamp(rb.velocity.y, -maxSpeed, maxSpeed),0);           
    }
    void Stopani()
    {
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ 
                | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        rb.constraints =  RigidbodyConstraints.FreezePositionZ
                | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }
    void SpeedZero()
    {
        speed = 0;
    }
    void FixedUpdate()
    {
        TrailControl();
        scr.text = score.ToString();
        ChangeArrow();
    }

    private void Update()
    {
            
      
        CountVel();
    }

}
