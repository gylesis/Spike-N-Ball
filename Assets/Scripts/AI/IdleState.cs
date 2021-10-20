using UnityEngine;

namespace AI
{
    public class IdleState : IState
    {
        private GameObject _gameObject;
        private Animator _animator;

        public IdleState(GameObject gameObject)
        {
            _gameObject = gameObject;

            _animator = _gameObject.GetComponent<Animator>();
        }
        
        public void Tick()
        {
            
        }

        public void OnEnter()
        {
            // play idle anim
        }

        public void OnExit()
        {
        }
    }
}