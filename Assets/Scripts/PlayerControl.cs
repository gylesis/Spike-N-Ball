using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using For_UI;
using For_Unique_Objects;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    public static PlayerControl Instance { get; private set; }
    private static int deathCount = 0;

    [SerializeField] public GameObject DeathScreen;
    [SerializeField] private float _jumpSoundVolume = 0.1f;
    [SerializeField] private GameObject _deathParticles;

    public Rigidbody Rigidbody;
    public Vector2 direction;
    public float speed;
    public float maxDistance = 0.500000000000000000001f;
    public GameObject Arrow;
    public GameObject RotationCenter;
    public bool BoolOnDeath;
    public bool BoolOnDeath2;
    public ParticleSystem deathVFX;
    public TrailRenderer trail;


    private Vector2 _startPos;
    private Vector3 _offset;
    private MeshDeformation _deformation;
    private float _arrowSize;
    private bool _isGroundedToMp;
    private bool _isInMagneticSphere;

    #region ExperimentalFieldsOfRice

    private float angularOffset;
    private Transform MvPlt;
    private bool allowToJump;

    bool prank;

    public event Action OnDeath;

    public event Action<Vector3> PositionChanged;

    #endregion

    private void Awake()
    {
        Instance = this;
        Time.timeScale = 5f;
    }

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        _deformation = GetComponent<MeshDeformation>();
    }

    private void FixedUpdate()
    {
        CountVel();
        if (EnviromentAct.Instance.GameIsStarted || EnviromentAct.Instance.isStartBegan)
        {
            ChangeArrow();
        }

        Fall();

        LateCollisionExit();
    }

    private void LateCollisionExit()
    {
        if (!CheckGround())
        {
            Vector3 newVelocity = Rigidbody.velocity;
            Rigidbody.velocity = newVelocity.normalized * speed * Time.deltaTime;
        }
    }

    #region Trigger

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MagneticSphere"))
        {
            _isInMagneticSphere = true;
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
            _isInMagneticSphere = false;
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
            Rigidbody.velocity = Vector3.zero;
            var deformDirection = -collision.GetContact(0).normal;
            _deformation.Deform(deformDirection,0.5f, 0.08f);
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

            _isGroundedToMp = true;
        }

        if (collision.gameObject.CompareTag("Ice"))
        {
            Vector3 newVelocity = Rigidbody.velocity;
            Rigidbody.velocity = newVelocity.normalized * speed * Time.deltaTime * 0.75f;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ice"))
        {
            Vector3 newVelocity = Rigidbody.velocity;
            Rigidbody.velocity = newVelocity.normalized * speed * Time.deltaTime * 0.75f;
        }

        if (collision.gameObject.CompareTag("MoveablePlatform"))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -0.75f);
        }

        if (collision.gameObject.CompareTag("MoveablePlatform"))
        {
            _isGroundedToMp = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("MoveablePlatform"))
        {
            Decelerate(0.08f);
        }

        if (collision.gameObject.CompareTag("MoveablePlatform"))
        {
            Decelerate(0.08f);
            FollowMoveablePlatform(_isGroundedToMp, collision);
        }

        PositionChanged?.Invoke(transform.position);
    }

    #endregion

    public void Death()
    {
        AudioManager.Instance.PlayDeathSound();

        Stats.Instance.AddDeath();

        Score.Instance.WriteCollectedCrystalls();

        deathCount++;
        Stats.Instance.AddAttempts(1);
        Stats.Instance.SaveStats();
        Stats.Instance.TimePerRunZero();

        MenuScript.GameIsPaused = true;
        Rigidbody.isKinematic = true;
        BoolOnDeath = true;
        BoolOnDeath2 = true;

        PlayerPrefs.Save();

        transform.DOScale(Vector3.zero, 0.5f).OnComplete(ShowDeathScreen);

        Instantiate(_deathParticles, transform.position,Quaternion.identity);
        
        //deathVFX.Play();

        void ShowDeathScreen()
        {
            DeathScreen.SetActive(true);

            AdController.Instance.TryPlayAdByDeatCount(deathCount);
        }
    }

    bool CheckGround()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, maxDistance);
        bool check = false;
        for (int i = 0; i <= hitColliders.Length - 1; i++)
        {
            if (hitColliders[i].gameObject.CompareTag("Ground")
                || hitColliders[i].gameObject.CompareTag("Ice")
                || hitColliders[i].gameObject.CompareTag("SwitchablePlatform")
                || hitColliders[i].gameObject.CompareTag("MoveablePlatform")
                || _isInMagneticSphere)
                check = true;
        }

        return check;
    }

    private void ChangeArrow()
    {
        if (Input.GetKey(KeyCode.Mouse0) && CheckGround() && !MenuScript.GameIsPaused)
        {
            if (direction.y >= 0)
            {
                RotationCenter.transform.eulerAngles = new Vector3(0, 0, -Mathf.Asin(direction.x) * 180 / Mathf.PI);
            }
            else
            {
                RotationCenter.transform.eulerAngles =
                    new Vector3(0, 0, 180 + Mathf.Asin(direction.x) * 180 / Mathf.PI);
            }

            Arrow.transform.localPosition = new Vector3(0, _arrowSize * 0.0035f, -1f);
            Arrow.transform.localScale = new Vector3(_arrowSize * 0.0015f, 0.3f, 0.1f);
            if (_arrowSize > 50) Arrow.SetActive(true);
        }
        else
        {
            Arrow.SetActive(false);
        }
    }

    void CountVel()
    {
        if (!MenuScript.GameIsPaused)
        {
            if (Input.GetMouseButtonDown(0))
            {
                prank = true;

                _startPos = Input.mousePosition;
            }

            if (!(EnviromentAct.Instance.GameIsStarted || EnviromentAct.Instance.isStartBegan)) return;
            if (Input.GetMouseButton(0))
            {
                Vector2 CurrentPos = Input.mousePosition;
                direction = CurrentPos - _startPos;
                direction.Normalize();
                if (direction.magnitude > 0 && _arrowSize <= 250)
                {
                    _arrowSize += 450 * Time.deltaTime;
                    if (prank) Stats.Instance.AddJumps(1); //it's just prank bro
                    prank = false;
                }
            }

            if (Input.GetMouseButtonUp(0) && CheckGround())
            {
                AudioManager.Instance.PlayJump(_jumpSoundVolume);
                allowToJump = true;
                Rigidbody.velocity = new Vector3(direction.x, direction.y, 0) * Time.deltaTime * speed;
                _arrowSize = 50;
            }
        }
    }

    void FollowMoveablePlatform(bool isGrounded, Collision collision)
    {
        CalculateAngularOffset(collision);

        if (isGrounded && collision.transform.CompareTag("MoveablePlatform"))
        {
            Vector3 destination = collision.transform.position + _offset;
            if (!allowToJump)
            {
                destination.x = Mathf.Clamp(destination.x, -2.09f, 2.09f);
                transform.position = destination;
                if (Mathf.Abs(destination.x) > 2.08f)
                {
                    CalculateOffset(collision);
                }
            }
            else if (allowToJump && Rigidbody.velocity.magnitude < 0.5f)
            {
                CalculateOffset(collision);
                allowToJump = false;
                Rigidbody.velocity = Vector3.zero;
            }
        }
    }

    public void Decelerate(float decCoeff)
    {
        Rigidbody.velocity -= Rigidbody.velocity.normalized * decCoeff;
        if (Rigidbody.velocity.magnitude < 0.1f) Rigidbody.velocity = Vector3.zero;
    }

    void Fall()
    {
        if (Rigidbody.velocity.magnitude < 0.01f && !CheckGround())
        {
            Rigidbody.AddForce(-Vector3.up, ForceMode.VelocityChange);
        }
    }

    private void CalculateAngularOffset(Collision collision)
    {
        Vector3 platformVector = new Vector3(Mathf.Sin(collision.transform.eulerAngles.z * (Mathf.PI)),
            Mathf.Cos(collision.transform.eulerAngles.z * (Mathf.PI)));
        float dotProduct = Vector3.Dot(_offset, platformVector);
        angularOffset = Mathf.Acos(dotProduct / (_offset.magnitude * platformVector.magnitude)) * (180 / Mathf.PI);
    }

    void CalculateOffset(Collision collision)
    {
        _offset = transform.position - collision.transform.position;
    }
}