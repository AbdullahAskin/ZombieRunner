using System.Collections.Generic;
using UnityEngine;

namespace TheyAreComing
{
	public class EnemyStateManager : StateManager
	{
		private EnemyAnimationController _enemyAnimationController;

		public EnemyAnimationController enemyAnimationController => _enemyAnimationController
			? _enemyAnimationController
			: _enemyAnimationController = GetComponent<EnemyAnimationController>();

		private void Start()
		{
			InitStates(new List<IStateBase>
				{new EnemyStateIdle(this)});
			SwitchState<PlayerStateIdle>();
		}
	}
}
