using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class KUSALKA : MonoBehaviour
{
    [SerializeField] private Rigidbody2D me;
    [SerializeField] private float радиусАгра;
    [SerializeField] private float atackCD;
    [SerializeField] private float idleCD;
    [SerializeField] private float Distance;
    [SerializeField] private SpriteRenderer Chain;

    private bool IsSleeping = true;
    private Timer TimerAttack;
    private Timer TimerIdle;
    private Sequence ToAttack;
    [SerializeField] private Transform EnemyTop;
    [SerializeField] private Transform EnemyBot;
    private Vector2 DirectionToAttack;

    private bool IsAwared;

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, Distance);
        Gizmos.DrawWireSphere(transform.position, радиусАгра);
    }


    void Start()
    {
        Vector3 atackRotation = new Vector3(0, 0, 5);

        TimerAttack = new Timer(atackCD);
        TimerIdle = new Timer(idleCD);

        ToAttack = DOTween.Sequence();
        ToAttack.Append(EnemyTop.DOLocalRotate(atackRotation, 1));
        ToAttack.Join(EnemyBot.DOLocalRotate(atackRotation, 1));
        ToAttack.Goto(0.8f);
        ToAttack.Pause();
        
        TimerIdle._action = FindPlayer;
    }

    void FindPlayer() => DirectionToAttack =
        (Vector2) PlayerControl.Instance.transform.position - (Vector2) transform.position;

    void Update()
    {
        TimerAttack.UpdateTime();
        TimerIdle.UpdateTime();

        var playerPosition = PlayerControl.Instance.transform.position;
        var directionToPlayer = (Vector2) playerPosition - (Vector2) transform.position;
        var isPlayerClose = directionToPlayer.magnitude < радиусАгра;

        Sleep();
        if (isPlayerClose && IsSleeping) ExitSleep();
        if (IsSleeping) return;
        if (TimerAttack.Check()) Attack(DirectionToAttack);
        if (TimerIdle.Check()) HoldIdlePosition();
        
        me.transform.LookTo(transform.position, 90);
        Chain.transform.parent.LookTo(me.transform.position, -90);
        ToAttack.SetAnimationFrame(transform.position,
            transform.position + (Vector3) directionToPlayer.normalized * Distance * 1.5f,
            me.transform.position);

        ChangeChainSize();
    }

    private void ChangeChainSize()
    {
        var size = Chain.size;
        size.y = Vector3.Distance(transform.position, me.transform.position) * -1 + 0.35f;
        Chain.size = size;
    }


    void Attack(Vector3 direction)
    {
        TimerAttack.Stop(TimerIdle.targetTime);
        TimerIdle.Start();
        
        //AwareMove
        var awareDirection = (Vector2)transform.position - me.position;
        if (awareDirection.magnitude > 0.2f && !IsAwared)
        {
            me.velocity = awareDirection.normalized * 0.4f;
            return;
        }
        else if (awareDirection.magnitude <= 0.2f && !IsAwared) IsAwared = true;
        
        //AtackMove
        Vector3 attackPosition = transform.position + direction.normalized * Distance;
        Vector3 attackDirection = attackPosition - me.transform.position;
        if (Vector3.Distance(attackPosition, me.transform.position) < 0.05f)
            me.velocity = Vector3.zero;
        else
            me.velocity = attackDirection.normalized * 3;
    }

    void HoldIdlePosition()
    {
        TimerIdle.Stop(TimerAttack.targetTime);
        TimerAttack.Start();

        if (IsAwared) IsAwared = false;
        var holdPosition = PlayerControl.Instance.transform.position - transform.position;
        holdPosition = transform.position + holdPosition.normalized * 0.7f;
        var directionToHoldPosition = holdPosition - me.transform.position;
        me.velocity = directionToHoldPosition.normalized * 
                      Mathf.Min(directionToHoldPosition.magnitude, 0.9f) * 2f;
    }

    void ExitSleep()
    {
        TimerIdle.time = TimerIdle.targetTime;
        TimerIdle.Start();
        IsSleeping = false;
    }

    void Sleep()
    {
    }
}

public static class Extensions
{
    public static void SetAnimationFrame(this Tween tween, Vector3 stPosition, Vector3 ndPosition,
        Vector3 currentPosition)
    {
        float distance = Vector3.Distance(stPosition, ndPosition);
        float positionDistance = Vector3.Distance(stPosition, currentPosition);
        float time = positionDistance / distance;
        tween.Goto(time);
    }


    public static void LookTo(this Transform transform, Vector3 to, float angularOffset)
    {
        Vector3 direction = to - transform.position;
        float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotation - angularOffset);
    }
}

internal class Timer
{
    public float time;
    public float targetTime;
    public bool _isStarted;
    public Action _action;

    public Timer(float _targetTime)
    {
        targetTime = _targetTime;
        time = 0;
    }

    public void UpdateTime()
    {
        if (_isStarted) time += Time.deltaTime / Time.timeScale;
    }

    public void Stop(float t)
    {
        if (time >= targetTime + t)
        {
            time = 0;
            _isStarted = false;
            _action();
        }
    }

    public void Start()
    {
        _isStarted = true;
    }

    public bool Check() => time > targetTime;
}