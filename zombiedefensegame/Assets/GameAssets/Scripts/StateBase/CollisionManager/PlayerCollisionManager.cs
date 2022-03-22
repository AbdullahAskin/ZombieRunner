using UnityEngine;

namespace TheyAreComing
{
    public class PlayerCollisionManager : CollisionManagerBase
    {
        [SerializeField] private ParticleSystem explosionParticle;
        protected override int MAXHealth => 100;

        private void OnTriggerEnter(Collider other)
        {
            if (!StateManager.IsAlive) return;
            if (!other.TryGetComponent(out ITriggerable<PlayerCollisionManager> triggerable)) return;
            triggerable.TriggerEnter(this);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!StateManager.IsAlive) return;
            if (!other.TryGetComponent(out ITriggerable<PlayerCollisionManager> triggerable)) return;
            triggerable.TriggerExit(this);
        }

        protected override void Death()
        {
        }

        public override void Damage(int amount)
        {
            CurrentHealth -= amount;
            explosionParticle.Play();
        }
    }
}