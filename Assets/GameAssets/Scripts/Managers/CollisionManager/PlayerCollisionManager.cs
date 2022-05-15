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
        private GunManager _gunManager;
        private Player _player;
        private ProgressBar _progressBar;

        private Player Player => _player ? _player : _player = GetComponent<Player>();
        public GunManager GunManager => _gunManager ? _gunManager : _gunManager = GetComponent<GunManager>();
        private ProgressBar ProgressBar => _progressBar ? _progressBar : _progressBar = GetComponent<ProgressBar>();

        private PlayerAnimationController AnimationController => _animationController
            ? _animationController
            : _animationController = GetComponent<PlayerAnimationController>();

        private CameraService CameraService => _cameraService
            ? _cameraService
            : _cameraService = ServiceManager.GetService<CameraService>();

        protected override int MAXHealth => Player.PlayerCharacterSettings.MaxHealth;
        protected override Vector3 CharacterPos => Player.Position;

        private void FixedUpdate()
        {
            if (Player.IsAlive && Input.GetKey(KeyCode.Space)) OnDeath();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!StateManager.IsAlive) return;
            if (!other.TryGetComponent(out ITriggerable<PlayerCollisionManager> triggerable)) return;
            triggerable.TriggerEnter(this);
        }

        protected override void OnDeath()
        {
            StateManager.SwitchState<PlayerStateDeath>(0);
        }

        public void OnDamage(int amount)
        {
            //Updating health
            OnHealthChange(-amount);
            ProgressBar.ONHealthChange(CurrentHealth);

            //Feedbacks
            damageParticle.Play();
            MMVibrationManager.Haptic(HapticTypes.MediumImpact);
            CameraService.ShakeCam();

            //Is death control
            if (CurrentHealth == 0) OnDeath();
            else AnimationController.SetTrigger(PlayerAnimationController.Hit);
        }

        public void OnHeal(int amount)
        {
            OnHealthChange(amount);
            ProgressBar.ONHealthChange(CurrentHealth);
        }
    }
}