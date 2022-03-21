using UnityEngine;

namespace TheyAreComing
{
	public class EnemyStateDeath : EnemyStateBase
	{
		public EnemyStateDeath(EnemyStateManager stateManager) : base(stateManager)
		{
		}

		public override void EnterState()
		{
			EnemyAnimationController.OnDeath();
		}

		public override void ExitState()
		{
		}

		public override void UpdateState()
		{
		}

		public override void OnCollisionEnter(Collision collision)
		{
		}
	}
}
