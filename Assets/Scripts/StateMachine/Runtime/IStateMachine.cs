using System;

namespace UGL.StateMachine
{
    public interface IStateMachine
    {
        void Start();
        void Update();
        
        void Apply<T>() where T : State;
        void Apply(Type type);

        public static IStateMachineBuilder Builder()
        {
            return new StateMachineBuilderImpl();
        }
    }
}