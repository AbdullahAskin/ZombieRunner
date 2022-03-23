using DG.Tweening;
using RootMotion.FinalIK;
using UnityEngine;

namespace TheyAreComing
{
    [RequireComponent(typeof(PlayerStateManager))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private CharacterSettingsScriptableObject characterSettingsScriptableObject;
        [SerializeField] private AimIK aimIK;
        [SerializeField] private FullBodyBipedIK fullBodyBipedIK;
        private PlayerCollisionManager _playerCollisionManager;
        private PlayerStateManager _playerStateManager;
        public bool IsAlive { get; set; } = true;

        public PlayerCharacterSettings PlayerCharacterSettings =>
            characterSettingsScriptableObject.PlayerCharacterSettings;

        public PlayerCollisionManager PlayerCollisionManager => _playerCollisionManager
            ? _playerCollisionManager
            : _playerCollisionManager = GetComponent<PlayerCollisionManager>();

        private PlayerStateManager PlayerStateManager => _playerStateManager
            ? _playerStateManager
            : _playerStateManager = GetComponent<PlayerStateManager>();

        public void Init()
        {
            PlayerStateManager.SwitchState<PlayerStateMovement>(0);
        }

        public void Death()
        {
            IsAlive = false;
            DOVirtual.Float(1, 0, .5f, x =>
            {
                aimIK.solver.IKPositionWeight = x;
                fullBodyBipedIK.solver.IKPositionWeight = x;
            });
            EnemyManager.StopEnemies();
        }
    }
}