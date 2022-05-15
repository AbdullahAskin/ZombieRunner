using UnityEngine;

namespace TheyAreComing
{
    [RequireComponent(typeof(EnemyBase))]
    public class EnemyStateManager : StateManager
    {
        private EnemyAnimationController _enemyAnimationController;
        private EnemyBase _enemyBase;

        public EnemyAnimationController EnemyAnimationController => _enemyAnimationController
            ? _enemyAnimationController
            : _enemyAnimationController = GetComponent<EnemyAnimationController>();

        public EnemyBase EnemyBase => _enemyBase
            ? _enemyBase
            : _enemyBase = GetComponent<EnemyBase>();

        public override bool IsAlive => CurrentStates[0].GetType() != typeof(EnemyStateDeath);

        private void Start()
        {
            StateBases.AddRange(GetStateBases<EnemyStateBase>(this));
            SwitchState<EnemyStateIdle>(0);
        }
    }
}