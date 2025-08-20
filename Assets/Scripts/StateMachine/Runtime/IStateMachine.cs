using System;
using UnityEngine;

namespace UGL.StateMachine
{
    public interface IStateMachine
    {
        void Start();
        void Update();
        
        void Apply<T>() where T : State;
        void Apply(Type type);

        public static IStateMachineBuilder Builder(GameObject gameObject = null)
        {
            return new StateMachineBuilderImpl(gameObject);
        }
    }
}