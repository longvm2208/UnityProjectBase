using System;
using UnityEngine;

namespace EventStateMachine.Implementation
{
    public class TestStateMachine : MonoBehaviour
    {
        [SerializeField] private StateMachine stateMachine = new StateMachine();

        private void Start()
        {
            stateMachine.ChangeState(IdleState);
        }

        private void Update()
        {
            stateMachine?.Execute();
        }

        #region State Machine
        private void IdleState(ref Action onEnter, ref Action onExecute, ref Action onExit)
        {
            onEnter = () =>
            {
                Debug.Log("Enter Idle");
            };

            onExecute = () =>
            {
                Debug.Log("Execute Idle");

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    stateMachine.ChangeState(AttackState);
                }
            };

            onExit = () =>
            {
                Debug.Log("Exit Idle");
            };
        }

        private void AttackState(ref Action onEnter, ref Action onExecute, ref Action onExit)
        {
            onEnter = () =>
            {
                Debug.Log("Enter Attack");
            };

            onExecute = () =>
            {
                Debug.Log("Execute Attack");

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    stateMachine.ChangeState(IdleState);
                }
            };

            onExit = () =>
            {
                Debug.Log("Exit Attack");
            };
        }
        #endregion
    }
}
