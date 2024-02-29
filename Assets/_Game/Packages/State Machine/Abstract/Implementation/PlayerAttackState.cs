using UnityEngine;

namespace AbstractStateMachine.Implementation
{
    [System.Serializable]
    public class PlayerAttackState : PlayerBaseState
    {
        public override void EnterState(PlayerStateMachine playerStateMachine)
        {
            Debug.Log("Player: Enter Attack");
        }

        public override void UpdateState(PlayerStateMachine playerStateMachine)
        {
            Debug.Log("Player: Update Attack");

            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerStateMachine.SwitchState(playerStateMachine.IdleState);
            }
        }

        public override void ExitState(PlayerStateMachine playerStateMachine)
        {
            Debug.Log("Player: Exit Attack");
        }
    }
}
