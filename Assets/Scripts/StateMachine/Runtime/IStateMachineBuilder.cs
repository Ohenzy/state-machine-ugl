namespace UGL.StateMachine
{
    public interface IStateMachineBuilder
    {
        IStateMachineBuilder Add(State state);
        IStateMachineBuilder StartWith<T>() where T : State;
        IStateMachine Build();
    }
}