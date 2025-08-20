using System;
using UnityEngine;

namespace UGL.StateMachine
{
    public abstract class MonoStateMachine : MonoBehaviour
    {
        protected IStateMachine StateMachine;

        protected abstract IStateMachine BuildStateMachine(IStateMachineBuilder builder);
        protected abstract Type GetActualState();
        
        protected virtual void Awake()
        {
            StateMachine = BuildStateMachine(IStateMachine.Builder(gameObject));
        }

        protected virtual void Start() => StateMachine.Start();
        
        protected virtual void Update() => StateMachine.Update();

        protected virtual void ApplyActualState() => StateMachine.Apply(GetActualState());
    }
}