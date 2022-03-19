using System.Collections.Generic;
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

        public bool IsAlive
        {
            get
            {
                if (_currentStates.Capacity == 0 || _currentStates[0] == null) return false;
                return _currentStates[0].GetType() == typeof(EnemyStateDeath);
            }
        }

        private void Start()
        {
            GetStateBases<EnemyStateBase>(this);
            SwitchState<EnemyStateIdle>(0);
        }
    }
}