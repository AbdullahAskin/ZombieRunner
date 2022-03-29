using MoreMountains.NiceVibrations;
using Service;
using UnityEngine;

namespace TheyAreComing
{
    [RequireComponent(typeof(ProgressBar))]
    public class PlayerCollisionManager : CollisionManagerBase
    {
        [SerializeField] private ParticleSystem damageParticle;
        private PlayerAnimationController _animationController;
        private CameraService _cameraService;
        private Player _player;
        private ProgressBar _progressBar;

        private Player Player => _player ? _player : _player = GetComponent<Player>();
        private ProgressBar ProgressBar => _progressBar ? _progressBar : _progressBar = GetComponent<ProgressBar>();

        private PlayerAnimationController AnimationController => _animationController
            ? _animationController
            : _animationController = GetComponent<PlayerAnimationController>();

        private CameraService CameraService => _cameraService
            ? _cameraService
            : _cameraService = ServiceManager.GetService<CameraService>();

        protected override int MAXHealth => Player.PlayerCharacterSettings.MaxHealth;

        private void OnTriggerEnter(Collider other)
        {
            if (!StateManager.IsAlive) return;
            if (!other.TryGetComponent(out ITriggerable<PlayerCollisionManager> triggerable)) return;
            triggerable.TriggerEnter(this);
        }

        protected override void Death()
        {
            StateManager.SwitchState<PlayerStateDeath>(0);
            Player.Death();
        }

        public void OnDamage(int amount)
        {
            //Updating health
            OnHealthChange(amount);
            ProgressBar.ONHealthChange(CurrentHealth);

            //Feedbacks
            damageParticle.Play();
            MMVibrationManager.Haptic(HapticTypes.MediumImpact);
            CameraService.ShakeCam();

            //Is death control
            if (CurrentHealth == 0) Death();
            else AnimationController.SetTrigger(PlayerAnimationController.Hit);
        }
    }
}