using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace TheyAreComing
{
    public abstract class StateManager : MonoBehaviour
    {
        private List<IStateBase> _currentStates = new List<IStateBase>();
        private List<IStateBase> _stateBases = new List<IStateBase>();

        private void FixedUpdate()
        {
            _currentStates.ForEach(x => x?.UpdateState());
        }

        public void OnCollisionEnter(Collision collision)
        {
            _currentStates.ForEach(x => x?.OnCollisionEnter(collision));
        }

        protected void ExtendCurrentStateArray(int targetSize)
        {
        }

        public void SwitchState<T>(int iCurrent) where T : IStateBase
        {
            if (_currentStates.Count < iCurrent + 1)
                _currentStates.AddRange(new IStateBase[iCurrent + 1 - _currentStates.Count]);
            var state = (T) _stateBases.Find(x => x.GetType() == typeof(T));
            _currentStates[iCurrent]?.ExitState();
            _currentStates[iCurrent] = state;
            _currentStates[iCurrent]?.EnterState();
        }

        protected void CreateStates<T>(params object[] constructorArgs) where T : IStateBase
        {
            Assembly.GetAssembly(typeof(T))
                .GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(T)))
                .Select(type => (T) Activator.CreateInstance(type, constructorArgs))
                .ToList().ForEach(x => _stateBases.Add(x));
        }
    }
}