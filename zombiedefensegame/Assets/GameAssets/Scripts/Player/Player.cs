using UnityEngine;

namespace TheyAreComing
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private CharacterSettingsScriptableObject characterSettingsScriptableObject;
        private PlayerStateManager _playerStateManager;

        public PlayerCharacterSettings PlayerCharacterSettings =>
            characterSettingsScriptableObject.PlayerCharacterSettings;

        private PlayerStateManager playerStateManager => _playerStateManager
            ? _playerStateManager
            : _playerStateManager = GetComponent<PlayerStateManager>();

        public void Init()
        {
            playerStateManager.SwitchState<PlayerStateMovement>(0);
        }
    }
}