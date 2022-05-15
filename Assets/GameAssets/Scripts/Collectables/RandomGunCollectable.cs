namespace TheyAreComing
{
    public class RandomGunCollectable : CollectableBase
    {
        public override void TriggerEnter(PlayerCollisionManager collisionManager)
        {
            if (IsCollected) return;
            IsCollected = true;

            base.TriggerEnter(collisionManager);
            collisionManager.GunManager.SwitchGunToRandomOne();
        }
    }
}