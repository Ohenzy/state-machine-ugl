using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UGL.StateMachine
{
    internal class StateMachineImpl : IStateMachine
    {
        private readonly Dictionary<Type, State> _states;

        private State _state;

        internal StateMachineImpl(State defaultState, List<State> states)
        {
            _state = defaultState;
            _states = states.ToDictionary(state => state.GetType());
        }

        public void Start() => EnterStateRecursive(_state);

        public void Tick() => _state.Tick();

        public void Apply<T>() where T : State => Apply(typeof(T));

        public void Apply(Type type)
        {
            if (!TryGet(type, out var state))
            {
                return;
            }

            ApplyExitsAndEnters(_state, state);
            _state = state;
        }

        internal void SetDefaultState(Type type)
        {
            if (TryGet(type, out var state))
            {
                _state = state;
            }
        }

        private bool TryGet(Type type, out State state)
        {
            var isPresent = _states.TryGetValue(type, out state);

            if (!isPresent)
            {
                Debug.LogWarning($"Состояние не найдено: {type.Name}");
            }

            return isPresent;
        }

        private static void ApplyExitsAndEnters(State from, State to)
        {
            if (from == to)
            {
                return;
            }

            var enters = new Stack<State>();

            while (from.Depth > to.Depth && from.Parent != null)
            {
                from.Exit();
                from = from.Parent;
            }

            while (to.Depth > from.Depth && to.Parent != null)
            {
                enters.Push(to);
                to = to.Parent;
            }

            while (from != to)
            {
                if (from != null)
                {
                    from.Exit();
                    from = from.Parent;
                }

                if (to != null)
                {
                    enters.Push(to);
                    to = to.Parent;
                }
            }

            while (enters.Count > 0)
            {
                enters.Pop().Enter();
            }
        }

        private static void EnterStateRecursive(State state)
        {
            if (!state)
            {
                return;
            }

            if (state.Parent)
            {
                EnterStateRecursive(state.Parent);
            }

            state.Enter();
        }
    }
}