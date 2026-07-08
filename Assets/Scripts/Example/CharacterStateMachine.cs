using System;
using UGL.StateMachine;
using UnityEngine;

namespace Example
{
    public class CharacterStateMachine : MonoStateMachine
    {
        [SerializeField] private bool isMove;
        [SerializeField] private bool isCombat;
        [SerializeField] private bool isDodge;

        private void OnValidate()
        {
            if (StateMachine == null)
            {
                return;
            }
            ApplyActualState();
        }

        protected override Type GetActualState()
        {
            if (isDodge)
            {
                return typeof(CharacterDodgeState);
            }
            
            if (isCombat)
            {
                return isMove 
                    ? typeof(CharacterCombatMovementState) 
                    : typeof(CharacterCombatIdleState);
            }
            
            return isMove 
                ? typeof(CharacterMovementState) 
                : typeof(CharacterIdleState);
        }
    }
}