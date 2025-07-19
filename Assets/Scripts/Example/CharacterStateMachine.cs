using System;
using UGL.StateMachine;

namespace Example
{
    public class CharacterStateMachine : MonoStateMachine
    {
        private bool _isMove;
        private bool _isCombat;
        private bool _isDodge;
        
        protected override IStateMachine BuildStateMachine(IStateMachineBuilder builder)
        {
            return builder.StartWith<CharacterIdleState>()
                .Add(new CharacterDodgeState())
                .Add(new CharacterState()
                    .Add(new CharacterIdleState())
                    .Add(new CharacterMovementState())
                )
                .Add(new CharacterCombatState()
                    .Add(new CharacterCombatIdleState())
                    .Add(new CharacterCombatMovementState())
                )
                .Build();
        }

        protected override Type GetActualState()
        {
            if (_isDodge)
            {
                return typeof(CharacterDodgeState);
            }
            
            if (_isCombat)
            {
                return _isMove 
                    ? typeof(CharacterCombatMovementState) 
                    : typeof(CharacterCombatIdleState);
            }
            
            return _isMove 
                ? typeof(CharacterMovementState) 
                : typeof(CharacterIdleState);
        }

        private void OnCharacterDodge(bool isDodge)
        {
            _isDodge = isDodge;
            ApplyActualState();
        }
        
        private void OnCharacterCombat(bool isCombat)
        {
            _isCombat = isCombat;
            ApplyActualState();
        }

        private void OnCharacterMovement(bool isMove)
        {
            _isMove = isMove;
            ApplyActualState();
        }
    }
}