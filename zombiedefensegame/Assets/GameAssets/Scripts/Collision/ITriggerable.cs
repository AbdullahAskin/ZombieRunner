using UnityEngine;

namespace TheyAreComing
{
    public interface ITriggerable<Base> where Base : CollisionManagerBase
    {
        public void TriggerEnter(Base t);
        public void CollisionEnter(Base collisionManager, ContactPoint contactPoint);
    }
}