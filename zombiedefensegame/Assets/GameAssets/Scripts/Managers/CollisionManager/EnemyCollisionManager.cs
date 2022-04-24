using MoreMountains.NiceVibrations;
using UnityEngine;

namespace TheyAreComing
{
    public class EnemyCollisionManager : CollisionManagerBase
    {
        private EnemyBase _enemyBase;
        private EnemyBase EnemyBase => _enemyBase ? _enemyBase : _enemyBase = GetComponent<EnemyBase>();

        protected override int MAXHealth => EnemyBase.EnemyCharacterSettings.MaxHealth;
        protected override Vector3 CharacterPos => transform.position;

        private void OnCollisionEnter(Collision other)
        {
            if (!StateManager.IsAlive) return;
            if (!other.transform.TryGetComponent(out ITriggerable<EnemyCollisionManager> triggerable)) return;
            triggerable.CollisionEnter(this, other.contacts[0]);
        }

        protected override void OnDeath()
        {
            MMVibrationManager.Haptic(HapticTypes.SoftImpact);
            StateManager.SwitchState<EnemyStateDeath>(0);
            EnemyBase.Death();
        }

        public void Damage(int amount)
        {
            OnHealthChange(-amount);
            if (CurrentHealth == 0) OnDeath();
        }
    }
}