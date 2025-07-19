using System;
using System.Collections.Generic;
using UnityEngine;

namespace UGL.StateMachine
{
    internal class StateMachineBuilderImpl : IStateMachineBuilder
    {
        private readonly Queue<State> _roots = new();
        private readonly HashSet<Type> _types = new();
        private readonly Dictionary<Type, State> _leafs = new();

        private Type _startStateType;

        public IStateMachineBuilder Add(State state)
        {
            _roots.Enqueue(state);
            return this;
        }

        public IStateMachineBuilder StartWith<T>() where T : State
        {
            if (_startStateType == null)
            {
                _startStateType = typeof(T);
            }
            else
            {
                Debug.LogWarning($"Начальное состояние уже установлено на: {_startStateType.Name}");
            }

            return this;
        }

        public IStateMachine Build()
        {
            FillLeafsRecursive(_roots);
            var sm = new StateMachineImpl(_leafs);

            if (_leafs.Count == 0)
            {
                Debug.LogWarning("Не добавленно ни одного состояния");
            }
            else if (_startStateType != null)
            {
                sm.SetDefaultState(_startStateType);
            }

            return sm;
        }

        private void FillLeafsRecursive(Queue<State> states, int depth = 0)
        {
            while (states.Count > 0)
            {
                var state = states.Dequeue();
                var type = state.GetType();

                if (!_types.Add(type))
                {
                    Debug.LogWarning($"Дубликат состояния: {type.Name}");
                    continue;
                }

                state.Depth = depth;
                if (state.Children == null)
                {
                    _leafs.Add(type, state);
                }
                else
                {
                    FillLeafsRecursive(state.Children, depth + 1);
                }
            }
        }
    }
}