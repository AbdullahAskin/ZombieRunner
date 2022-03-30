using UnityEngine;

namespace TheyAreComing
{
    public class HealCollectable : CollectableBase
    {
        [SerializeField] private int amount;

        public override void TriggerEnter(PlayerCollisionManager collisionManager)
        {
            if (IsCollected) return;
            IsCollected = true;

            base.TriggerEnter(collisionManager);
            collisionManager.OnHeal(amount);
        }
    }
}