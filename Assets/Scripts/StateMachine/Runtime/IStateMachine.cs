using System;

namespace UGL.StateMachine
{
    public interface IStateMachine
    {
        void Start();
        void Tick();
        
        void Apply<T>() where T : State;
        void Apply(Type type);
    }
}