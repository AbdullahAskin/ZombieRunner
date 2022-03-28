using System.Collections.Generic;
using MoreMountains.NiceVibrations;
using UnityEngine;

namespace TheyAreComing
{
    public class EnemyCollisionManager : CollisionManagerBase
    {
        public Transform explosionParticlePivot;
        [HideInInspector] public List<ParticleSystem> damageParticles = new List<ParticleSystem>();
        private EnemyBase _enemyBase;
        private EnemyBase EnemyBase => _enemyBase ? _enemyBase : _enemyBase = GetComponent<EnemyBase>();

        protected override int MAXHealth => EnemyBase.EnemyCharacterSettings.MaxHealth;

        private void OnCollisionEnter(Collision other)
        {
            if (!StateManager.IsAlive) return;
            if (!other.transform.TryGetComponent(out ITriggerable<EnemyCollisionManager> triggerable)) return;
            triggerable.CollisionEnter(this, other.contacts[0]);
        }

        public void CreateParticles(IEnumerable<ParticleSystem> explosionParticles)
        {
            foreach (var explosionParticle in explosionParticles)
                damageParticles.Add(Instantiate(explosionParticle, explosionParticlePivot, false));
        }

        protected override void Death()
        {
            MMVibrationManager.Haptic(HapticTypes.SoftImpact);
            StateManager.SwitchState<EnemyStateDeath>(0);
            EnemyBase.Death();
        }

        public void Damage(int amount)
        {
            CalculateHealth(amount);
            if (CurrentHealth == 0) Death();
        }
    }
}