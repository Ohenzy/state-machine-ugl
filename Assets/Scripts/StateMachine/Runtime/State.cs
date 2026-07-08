using UnityEngine;

namespace UGL.StateMachine
{
    public abstract class State : MonoBehaviour
    {
        internal int Depth;
        internal State Parent; 
        
        public virtual void Enter()
        {
        }

        public virtual void Exit()
        {
        }

        public virtual void Tick()
        {
        }
    }
}