using System.Collections.Generic;
using UnityEngine;

namespace TheyAreComing
{
	public class StateManager : MonoBehaviour
	{
		private IStateBase _current;
		private List<IStateBase> _stateBases;

		protected void InitStates(List<IStateBase> stateBases) => _stateBases = stateBases;
		
		public void OnCollisionEnter(Collision collision)
		{
			_current?.OnCollisionEnter(collision);
		}
		
		private void FixedUpdate()
		{
			_current?.UpdateState();
		}
		
		public void SwitchState<T>() where T : IStateBase
		{
			var state = (T) _stateBases.Find(x => x.GetType() == typeof(T));
			_current?.ExitState();
			_current = state;
			_current?.EnterState();
		}
	}
}
