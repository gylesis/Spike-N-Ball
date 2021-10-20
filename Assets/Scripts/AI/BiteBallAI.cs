using System;
using DG.Tweening;
using UnityEngine;

namespace AI
{
    public class BiteBallAI : MonoBehaviour
    {
        [SerializeField] private float _agreDistance = 5f;
        [SerializeField] private Transform _upperPoint;
        [SerializeField] private Speed _speed;
        [SerializeField] private Transform _enemyTop;
        [SerializeField] private Transform _enemyBot;
        
        private Tween _kusAnimation;

        private StateMachine _stateMachine;
        private PlayerControl _player;
        public Sequence ToAttack;
        [SerializeField] private Vector3 atackRotation;


        private void InitKusAnimation(float duration)
        {
            ToAttack = DOTween.Sequence();
            ToAttack.SetLoops(-1,loopType:LoopType.Yoyo);
            ToAttack.Append(_enemyTop.DOLocalRotate(atackRotation, duration));
            ToAttack.Join(_enemyBot.DOLocalRotate(-atackRotation, duration));
            ToAttack.Goto(0);
            ToAttack.Pause();
        }
        private void Start()
        {
            _player = PlayerControl.Instance;
            _stateMachine = new StateMachine();
            InitKusAnimation(0.1f);

            var idle = new IdleState(gameObject);
            var attack = new AttackState(transform, _player,_speed);
            var foundPlayer = new FoundPlayerState(_stateMachine, attack);
            var backToStart = new BackToStartState(transform, _speed);

            _stateMachine.AddTransition(idle, foundPlayer, IsInRange());
            _stateMachine.AddTransition(attack, backToStart, IsOutOfRange());

            _stateMachine.SetState(idle);
        }

        private void Update() =>
            _stateMachine.Tick();

        Func<bool> IsInRange() => () =>
        {
            var distance = Vector3.Distance(transform.position, _player.transform.position);

            if (distance <= _agreDistance) return true;

            return false;
        };

        Func<bool> IsOutOfRange() => () =>
        {
            if (_player.transform.position.y >= _upperPoint.position.y) return true;

            return false;
        };

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position,_agreDistance);
        }
    }

    [Serializable]
    public class Speed
    {
        public float Value = 1.5f;
    }
}