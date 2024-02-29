using UnityEngine;

namespace InterfaceStateMachine.Implementation
{
    [System.Serializable]
    public class PlayerAttackState : IBaseState<PlayerStateMachine>
    {
        public void EnterState(PlayerStateMachine playerStateMachine)
        {
            Debug.Log("Player: Enter Attack");
        }

        public void UpdateState(PlayerStateMachine playerStateMachine)
        {
            Debug.Log("Player: Update Attack");

            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerStateMachine.SwitchState(playerStateMachine.IdleState);
            }
        }

        public void ExitState(PlayerStateMachine playerStateMachine)
        {
            Debug.Log("Player: Exit Attack");
        }
    }
}
