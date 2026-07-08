using UGL.StateMachine;

namespace Example
{
    public class CharacterStateBase : State
    {
        public override void Enter()
        {
            print($"Enter: {GetType().Name}");
        }

        public override void Exit()
        {
            print($"Exit: {GetType().Name}");
        }
    }
}