using System;
using UnityEngine;

namespace TheyAreComing
{
    [CreateAssetMenu(fileName = "EnemyCharacterSettings", menuName = "ScriptableObjects/EnemyCharacterSettings",
        order = 1)]
    public class EnemySettingsScriptableObject : ScriptableObject
    {
        public EnemyCharacterSettings characterSettings;
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