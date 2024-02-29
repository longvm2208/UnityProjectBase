using UnityEngine;

namespace InterfaceStateMachine.Implementation
{
    public class PlayerStateMachine : MonoBehaviour
    {
        [SerializeField] private PlayerIdleState idleState;
        [SerializeField] private PlayerAttackState attackState;

        private IBaseState<PlayerStateMachine> currentState;

        public PlayerIdleState IdleState => idleState;
        public PlayerAttackState AttackState => attackState;

        private void Start()
        {
            SwitchState(idleState);
        }

        private void Update()
        {
            currentState?.UpdateState(this);
        }

        public void SwitchState(IBaseState<PlayerStateMachine> nextState)
        {
            if (currentState != nextState)
            {
                currentState?.ExitState(this);
                currentState = nextState;
                currentState?.EnterState(this);
            }
        }
    }
}
