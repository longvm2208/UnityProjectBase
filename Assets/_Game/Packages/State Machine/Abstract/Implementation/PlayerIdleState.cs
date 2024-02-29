using UnityEngine;

namespace AbstractStateMachine.Implementation
{
    [System.Serializable]
    public class PlayerIdleState : PlayerBaseState
    {
        public override void EnterState(PlayerStateMachine playerStateMachine)
        {
            Debug.Log("Player: Enter Idle");
        }

        public override void UpdateState(PlayerStateMachine playerStateMachine)
        {
            Debug.Log("Player: Update Idle");

            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerStateMachine.SwitchState(playerStateMachine.AttackState);
            }
        }

        public override void ExitState(PlayerStateMachine playerStateMachine)
        {
            Debug.Log("Player: Exit Idle");
        }
    }
}
