using MoreMountains.NiceVibrations;
using Service;
using UnityEngine;

namespace TheyAreComing
{
    public class PlayerCollisionManager : CollisionManagerBase
    {
        [SerializeField] private ParticleSystem damageParticle;
        private PlayerAnimationController _animationController;
        private CameraService _cameraService;
        private Player _player;

        private Player Player => _player ? _player : _player = GetComponent<Player>();

        private PlayerAnimationController AnimationController => _animationController
            ? _animationController
            : _animationController = GetComponent<PlayerAnimationController>();

        private CameraService CameraService => _cameraService
            ? _cameraService
            : _cameraService = ServiceManager.GetService<CameraService>();

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
            StateManager.SwitchState<PlayerStateDeath>(0);
            Player.Death();
        }

        public void Damage(int amount)
        {
            CalculateHealth(amount);
            MMVibrationManager.Haptic(HapticTypes.MediumImpact);
            CameraService.ShakeCam();
            if (CurrentHealth == 0) Death();
            else AnimationController.SetTrigger(PlayerAnimationController.Hit);
        }
    }
}