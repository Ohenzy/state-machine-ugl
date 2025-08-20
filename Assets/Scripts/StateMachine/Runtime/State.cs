using System.Collections.Generic;
using UnityEngine;

namespace UGL.StateMachine
{
    public abstract class State
    {
        internal int Depth;
        
        internal State Parent { get; private set; }
        internal Queue<State> Children { get; private set; }

        protected internal virtual void Init(GameObject gameObject)
        {
        }
        
        public virtual void Enter()
        {
        }

        public virtual void Exit()
        {
        }

        public virtual void Update()
        {
        }

        public State Add(State state)
        {
            state.Parent = this;

            Children ??= new Queue<State>();
            Children.Enqueue(state);
            return this;
        }
    }
}