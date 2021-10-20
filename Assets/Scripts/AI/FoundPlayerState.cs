using DG.Tweening;
using UnityEngine;

namespace AI
{
    public class FoundPlayerState : IState
    {
        private StateMachine _stateMachine;
        private IState _attackState;

        public FoundPlayerState(StateMachine stateMachine, IState attackState)
        {
            _attackState = attackState;
            _stateMachine = stateMachine;
        }

        public void Tick()
        {
                            
        }

        public void OnEnter()
        {
            Debug.Log("Found player");
            DOVirtual.DelayedCall(0, Attack);
        }

        private void Attack() => 
            _stateMachine.SetState(_attackState);

        public void OnExit()
        {
        }
    }
}