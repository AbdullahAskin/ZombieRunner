using UnityEngine;

namespace TheyAreComing
{
    public class CollectableBase : MonoBehaviour, ITriggerable<PlayerCollisionManager>
    {
        [SerializeField] private ParticleSystem onCollectParticle;
        [SerializeField] private GameObject skin;
        protected bool IsCollected;

        public virtual void TriggerEnter(PlayerCollisionManager collisionManager)
        {
            skin.SetActive(false);
            onCollectParticle.transform.position =
                GameManager.CurvedWorldController.TransformPosition(transform.position);
            onCollectParticle.Play();
        }

        public void CollisionEnter(PlayerCollisionManager collisionManager, ContactPoint contactPoint)
        {
        }
    }
}