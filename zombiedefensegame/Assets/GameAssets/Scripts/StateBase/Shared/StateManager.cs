using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace TheyAreComing
{
    public abstract class StateManager : MonoBehaviour
    {
        protected List<IStateBase> CurrentStates = new List<IStateBase>();
        protected List<IStateBase> StateBases = new List<IStateBase>();

        private void FixedUpdate()
        {
            CurrentStates.ToList().ForEach(x => x?.UpdateState());
        }

        public void OnCollisionEnter(Collision collision)
        {
            CurrentStates.ForEach(x => x?.OnCollisionEnter(collision));
        }

        public void SwitchState<T>(int iState) where T : IStateBase
        {
            if (CurrentStates.Count < iState + 1)
                CurrentStates.AddRange(new IStateBase[iState + 1 - CurrentStates.Count]);
            var state = (T) StateBases.Find(x => x.GetType() == typeof(T));
            CurrentStates[iState]?.ExitState();
            CurrentStates[iState] = state;
            CurrentStates[iState]?.EnterState();
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