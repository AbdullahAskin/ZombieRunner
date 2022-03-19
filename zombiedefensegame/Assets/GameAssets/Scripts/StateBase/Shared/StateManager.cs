using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace TheyAreComing
{
    public abstract class StateManager : MonoBehaviour
    {
        private readonly List<IStateBase> _currentStates = new List<IStateBase>();
        protected List<IStateBase> StateBases = new List<IStateBase>();

        private void FixedUpdate()
        {
            _currentStates.ForEach(x => x?.UpdateState());
        }

        public void OnCollisionEnter(Collision collision)
        {
            _currentStates.ForEach(x => x?.OnCollisionEnter(collision));
        }

        public void SwitchState<T>(int iState) where T : IStateBase
        {
            if (_currentStates.Count < iState + 1)
                _currentStates.AddRange(new IStateBase[iState + 1 - _currentStates.Count]);
            var state = (T) StateBases.Find(x => x.GetType() == typeof(T));
            _currentStates[iState]?.ExitState();
            _currentStates[iState] = state;
            _currentStates[iState]?.EnterState();
        }

        protected List<IStateBase> GetStateBases<T>(params object[] constructorArgs) where T : IStateBase
        {
            var stateBases = new List<IStateBase>();
            Assembly.GetAssembly(typeof(T))
                .GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(T)))
                .Select(type => (T) Activator.CreateInstance(type, constructorArgs))
                .ToList().ForEach(x => stateBases.Add(x));
            return stateBases;
        }
    }
}