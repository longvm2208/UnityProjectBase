using UnityEngine;

namespace InterfaceStateMachine.Implementation
{
    [System.Serializable]
    public class PlayerIdleState : IBaseState<PlayerStateMachine>
    {
        public void EnterState(PlayerStateMachine playerStateMachine)
        {
            Debug.Log("Player: Enter Idle");
        }

        public void UpdateState(PlayerStateMachine playerStateMachine)
        {
            Debug.Log("Player: Update Idle");

            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerStateMachine.SwitchState(playerStateMachine.AttackState);
            }
        }

        public void ExitState(PlayerStateMachine playerStateMachine)
        {
            Debug.Log("Player: Exit Idle");
        }
    }
}
