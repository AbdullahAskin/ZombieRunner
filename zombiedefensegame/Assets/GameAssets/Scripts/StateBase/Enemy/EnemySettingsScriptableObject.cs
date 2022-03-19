using System;
using UnityEngine;

namespace TheyAreComing
{
    [CreateAssetMenu(fileName = "EnemyCharacterSettings", menuName = "ScriptableObjects/EnemyCharacterSettings",
        order = 1)]
    public class EnemySettingsScriptableObject : ScriptableObject
    {
        [SerializeField] private EnemyCharacterSettings characterSettings;
        
        public EnemyCharacterSettings GetCharacterSettings()
        {
            return characterSettings;
        }
    }

    [Serializable]
    public class EnemyCharacterSettings
    {
        public float speed;
        public float range;
        public int health;
        public float attackRange;
    }
}