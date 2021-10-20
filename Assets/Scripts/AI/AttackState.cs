using DG.Tweening;
using UnityEngine;

namespace AI
{
    public class AttackState : IState
    {
        private readonly Transform _transform;
        private PlayerControl _player;
        private readonly Rigidbody _rigidbody;
        private Speed _speed;

        private Sequence _toAtack;

        public AttackState(Transform transform, PlayerControl player, Speed speed)
        {
            _speed = speed;

            _transform = transform;
            _player = player;

            _rigidbody = _transform.GetComponent<Rigidbody>();
            _toAtack = _transform.GetComponent<BiteBallAI>().ToAttack;
        }

        private void Attack()
        {
            var directionToPlayer = (_player.transform.position - _transform.position).normalized;

            Vector3 transformUp = _transform.up;
            
            Vector3 rotateDirection = Vector3.Lerp(-transformUp, directionToPlayer,0.02f);
            
            _transform.LookTo(_transform.position + rotateDirection, -90);

            _rigidbody.velocity = -transformUp * _speed.Value;
        }

        public void Tick()
        {
            Attack();
        }

        public void OnEnter()
        {
            Debug.Log("Attack player");
           // OnPositionChange(Vector3.up);
            
            _toAtack.Play();
          //  _player.PositionChanged += OnPositionChange;
        }


        public void OnExit()
        {
            _rigidbody.DOKill();
          //  _player.PositionChanged -= OnPositionChange;
        }
    }
}