using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheyAreComing
{
    [CreateAssetMenu(fileName = "EnemyCharacterSettings", menuName = "ScriptableObjects/EnemyCharacterSettings",
        order = 1)]
    public class CharacterSettingsScriptableObject : ScriptableObject
    {
        public bool isPlayer;
        [HideIf("isPlayer")] [SerializeField] private EnemyCharacterSettings _enemyCharacterSettings;
        [ShowIf("isPlayer")] [SerializeField] private PlayerCharacterSettings _playerCharacterSettings;

        public PlayerCharacterSettings PlayerCharacterSettings => _playerCharacterSettings;
        public EnemyCharacterSettings EnemyCharacterSettings => _enemyCharacterSettings;
    }

    [Serializable]
    public class EnemyCharacterSettings : CharacterSettings
    {
        public float range;
        public float attackRange;
        public int damage;
    }

    [Serializable]
    public class PlayerCharacterSettings : CharacterSettings
    {
    }

    public class CharacterSettings
    {
        public int MaxHealth;
        public float Speed;
    }
}