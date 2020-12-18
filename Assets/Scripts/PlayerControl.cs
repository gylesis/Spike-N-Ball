using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    public static PlayerControl Instance { get; private set; }
    public static int deathCount = 0;


    public Rigidbody rb;
    private Vector2 StartPos;
    public Vector2 direction;
    public float speed;
    public float maxDistance = 0.500000000000000000001f;
    public GameObject Arrow;
    public GameObject RotationCenter;
    private float ArrowSize;
    public GameObject JumpSound;
    private Vector3 offset;
    private bool isGroundedToMP;
    private bool isInMagneticSphere;
    public bool BoolOnDeath = false;
    public bool BoolOnDeath2 = false;
   [SerializeField] public GameObject DeathScreen;

    #region ExperimentalFields
    private float angularOffset;
    private Transform MvPlt;
    private bool allowToJump;
    public Vector3 vel;

    bool prank;
    #endregion

    private void Awake()
    {
        Instance = this;
        Time.timeScale = 5f;
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
   
    private void FixedUpdate()
    {
        if (EnviromentAct.Instance.GameIsStarted && Input.GetMouseButtonDown(0) && !MenuScript.Instance.GameIsPaused);
        {
            CountVel();
            ChangeArrow();
        }
        Fall();
      

    }
    
    #region Trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MagneticSphere"))
        {
            isInMagneticSphere = true;
        }
           
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("MoveablePlatform"))
        {
            Death();
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("SwitchablePlatform"))
        {
            other.transform.GetComponentInParent<SwitchablePlatform>().SwitchPlatforms();
        }
        if (other.gameObject.CompareTag("MagneticSphere"))
        {
            isInMagneticSphere = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("MagneticSphere"))
        {
            MagneticSphere ms = other.GetComponent<MagneticSphere>();
            if (Input.GetKeyUp(KeyCode.Mouse0) && ms.isEnabled)
            {
                StartCoroutine(ms.Disappear());
                StartCoroutine(ms.Recreate());
            }
        }
    }
    #endregion

    #region Collision
    private void OnCollisionEnter(Collision collision)
    {
        allowToJump = false;
        if (collision.gameObject.CompareTag("Ground") 
            || collision.gameObject.CompareTag("MoveablePlatform")
            || collision.gameObject.CompareTag("SwitchablePlatform"))
        {
            rb.velocity = Vector3.zero;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Death();
        }
        if (collision.gameObject.CompareTag("MoveablePlatform"))
        {
            CalculateOffset(collision);

            MoveablePlatform moveablePlatform = collision.transform.GetComponentInParent<MoveByWaypoints>();
            if (moveablePlatform == null)
            {
                moveablePlatform = collision.transform.GetComponentInParent<Rotation>();
            }
            moveablePlatform.StartMovement();

            isGroundedToMP = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        Vector3 newVelocity = rb.velocity;
        rb.velocity = newVelocity.normalized * speed * Time.deltaTime;
        if (collision.gameObject.CompareTag("MoveablePlatform"))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -0.75f);
        }
        if (collision.gameObject.CompareTag("MoveablePlatform"))
        {
            isGroundedToMP = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("MoveablePlatform"))
        {
            Decelerate(0.02f);
        }
        if (collision.gameObject.CompareTag("Ice"))
        {
            Decelerate(0.002f);
        }
        if (collision.gameObject.CompareTag("MoveablePlatform"))
        {
            Decelerate(0.02f);
            FollowMoveablePlatform(isGroundedToMP, collision);
        }
    }
    #endregion

    public void Death()
    {
        BoolOnDeath = true;
        BoolOnDeath2 = true;

        rb.isKinematic = true;
        EnviromentAct.Instance.GameIsStarted = false;
        DeathScreen.SetActive(true); 

        Score.Instance.WriteCollectedCrystalls();

        deathCount++;
        Stats.Instance.AddAttempts(1);
        Stats.Instance.SaveStats();
        Stats.Instance.TimePerRunZero();
      //  deathCount++;
       // Debug.Log(deathCount);
       // Advertisements.ShowAd(deathCount);
        foreach (Achievement achievment in Achievements.achievements)
        {
            achievment.SaveAchievement();
        }

        PlayerPrefs.Save();

       // Advertisements.ShowAd(deathCount);
    }
    bool CheckGround()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, maxDistance);
        bool check = false;
        for (int i = 0; i <= hitColliders.Length - 1; i++)
        {
            if (hitColliders[i].gameObject.tag == "Ground" 
                || hitColliders[i].gameObject.tag == "Ice"
                || hitColliders[i].gameObject.tag == "SwitchablePlatform"
                || hitColliders[i].gameObject.tag == "MoveablePlatform"
                || isInMagneticSphere) 
                check = true;
        }
        return check;
    }

    private void ChangeArrow()
    {
        if (Input.GetKey(KeyCode.Mouse0) && CheckGround() && !MenuScript.Instance.GameIsPaused)
        {
            if (direction.y >= 0)
            {
                RotationCenter.transform.eulerAngles = new Vector3(0, 0, -Mathf.Asin(direction.x) * 180 / Mathf.PI);
            }
            else
            {
                RotationCenter.transform.eulerAngles = new Vector3(0, 0, 180 + Mathf.Asin(direction.x) * 180 / Mathf.PI);
            }
            Arrow.transform.localPosition = new Vector3(0, ArrowSize / 900 * 5.5f, 0.5f);
            Arrow.transform.localScale = new Vector3(0.8f, ArrowSize / 600, 0.1f);
            Arrow.SetActive(true);
        }
        else
        {
            Arrow.SetActive(false);
        }
    }
    
    void CountVel()
    {
        if (!MenuScript.Instance.GameIsPaused )
        {
            if (Input.GetMouseButtonDown(0))
            {
                prank = true;

                StartPos = Input.mousePosition;
            }
            if (Input.GetMouseButton(0))
            {
                Vector2 CurrentPos = Input.mousePosition;
                direction = StartPos - CurrentPos;
                direction.Normalize();
                if (direction.magnitude > 0 && ArrowSize <= 250)
                {
                    ArrowSize += 450 * Time.deltaTime;
                    if (prank) Stats.Instance.AddJumps(1); //it's just prank bro
                    prank = false;
                }
            }

            if (Input.GetMouseButtonUp(0) && CheckGround())
            {
                allowToJump = true;
                rb.velocity = new Vector3(direction.x, direction.y, 0) * Time.deltaTime * speed;
                ArrowSize = 0;
            }
        }
    }

    void FollowMoveablePlatform(bool isGrounded, Collision collision)
    {
        CalculateAngularOffset(collision);

        if (isGrounded && collision.transform.CompareTag("MoveablePlatform"))
        {
            Vector3 destination = collision.transform.position + offset;
            if (!allowToJump)
            {
                destination.x = Mathf.Clamp(destination.x, -2.09f, 2.09f);
                transform.position = destination;
                if (Mathf.Abs(destination.x) > 2.08f)
                {
                    CalculateOffset(collision);
                }
            }
            else if (allowToJump && rb.velocity.magnitude < 0.5f)
            {
                CalculateOffset(collision);
                allowToJump = false;
                rb.velocity = Vector3.zero;
            }
        }
    }

    public void Decelerate(float decCoeff)
    {
        rb.velocity -= rb.velocity.normalized * decCoeff;
    }

    void Fall()
    {
        if(rb.velocity.magnitude < 0.01f && !CheckGround())
        {
            rb.AddForce(-Vector3.up, ForceMode.VelocityChange);
        }
    }

    private void CalculateAngularOffset(Collision collision)
    {
        Vector3 platformVector = new Vector3(Mathf.Sin(collision.transform.eulerAngles.z * (Mathf.PI)),
                                             Mathf.Cos(collision.transform.eulerAngles.z * (Mathf.PI)));
        float dotProduct = Vector3.Dot(offset, platformVector);
        angularOffset = Mathf.Acos(dotProduct / (offset.magnitude * platformVector.magnitude)) * (180 / Mathf.PI);
        
    }

    void CalculateOffset(Collision collision)
    {
        offset = transform.position - collision.transform.position;
    }


   



}