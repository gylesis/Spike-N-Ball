using DG.Tweening;
using UnityEngine;

namespace AI
{
    public class BackToStartState : IState
    {
        private readonly Vector3 _startPos;
        private readonly Transform _transform;
        private readonly Speed _speed;
        private readonly Rigidbody _rigidbody;

        public BackToStartState(Transform transform, Speed speed)
        {
            _transform = transform;
            _speed = speed;
            _startPos = transform.position;

            _rigidbody = _transform.GetComponent<Rigidbody>();
        }

        public void Tick()
        {
            var directionToPlayer = (_startPos - _transform.position).normalized;

            Vector3 transformUp = _transform.up;
            
            Vector3 rotateDirection = Vector3.Lerp(-transformUp, directionToPlayer,0.02f);
            
            _transform.LookTo(_transform.position + rotateDirection, -90);

            _rigidbody.velocity = -transformUp * _speed.Value;
        }

        public void OnEnter()
        {
        }

        public void OnExit() { }
    }
}