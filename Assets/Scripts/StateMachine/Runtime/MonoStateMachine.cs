using System;
using System.Collections.Generic;
using UnityEngine;

namespace UGL.StateMachine
{
    public abstract class MonoStateMachine : MonoBehaviour
    {
        private const string EmptyDefaultStateErrorMessage = "Не задано состояние по умолчанию";
        private const string HierarchyDefaultStateErrorMessage = "Состояние по умолчанию не найдено в иерархии";

        [SerializeField] private State defaultState;

        protected IStateMachine StateMachine;

        protected abstract Type GetActualState();

        protected virtual void Awake()
        {
            StateMachine = BuildStateMachine();
        }

        protected virtual void Start() => StateMachine.Start();

        protected virtual void Update() => StateMachine.Tick();

        protected virtual void ApplyActualState() => StateMachine.Apply(GetActualState());

        private IStateMachine BuildStateMachine()
        {
            var leafs = new List<State>();

            for (var i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).TryGetComponent<State>(out var rootState))
                {
                    InitState(rootState, leafs);
                }
            }

            if (!defaultState)
            {
                Debug.LogError(EmptyDefaultStateErrorMessage);
            }

            if (!leafs.Contains(defaultState))
            {
                Debug.LogError(HierarchyDefaultStateErrorMessage);
            }

            return new StateMachineImpl(defaultState, leafs);
        }

        private static void InitState(State rootState, List<State> leafs)
        {
            ushort childCount = 0;
            for (var i = 0; i < rootState.transform.childCount; i++)
            {
                if (!rootState.transform.GetChild(i).TryGetComponent<State>(out var childState))
                {
                    continue;
                }

                childCount++;

                childState.Parent = rootState;
                childState.Depth = rootState.Depth + 1;

                InitState(childState, leafs);
            }

            if (childCount == 0)
            {
                leafs.Add(rootState);
            }
        }
    }
}