using UnityEngine;

namespace TheyAreComing
{
	public abstract class EnemyStateBase : IStateBase
	{
		protected EnemyStateManager _enemyStateManager;

		protected EnemyStateBase(EnemyStateManager enemyStateManager)
		{
			_enemyStateManager = enemyStateManager;
		}
		public abstract void EnterState();
		public abstract void ExitState();
		public abstract void UpdateState();
		public abstract void OnCollisionEnter(Collision collision);
	}
}
