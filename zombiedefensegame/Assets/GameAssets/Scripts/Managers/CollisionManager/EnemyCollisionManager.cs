using UnityEngine;

namespace TheyAreComing
{
    public class EnemyCollisionManager : CollisionManagerBase
    {
        private EnemyBase _enemyBase;
        private EnemyBase EnemyBase => _enemyBase ? _enemyBase : _enemyBase = GetComponent<EnemyBase>();

        protected override int MAXHealth => EnemyBase.EnemyCharacterSettings.MaxHealth;

        private void OnTriggerEnter(Collider other)
        {
            if (!StateManager.IsAlive) return;
            if (!other.TryGetComponent(out ITriggerable<EnemyCollisionManager> triggerable)) return;
            triggerable.TriggerEnter(this);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!StateManager.IsAlive) return;
            if (!other.TryGetComponent(out ITriggerable<EnemyCollisionManager> triggerable)) return;
            triggerable.TriggerExit(this);
        }

        protected override void Death()
        {
            StateManager.SwitchState<EnemyStateDeath>(0);
            EnemyBase.Death();
        }

        public override void Damage(int amount)
        {
            base.Damage(amount);
            if (CurrentHealth == 0) Death();
        }
    }
}