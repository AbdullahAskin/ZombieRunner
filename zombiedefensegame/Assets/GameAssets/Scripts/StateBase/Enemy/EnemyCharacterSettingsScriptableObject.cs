using UnityEngine;

namespace TheyAreComing
{
	[CreateAssetMenu(fileName = "EnemyCharacterSettings", menuName = "ScriptableObjects/EnemyCharacterSettings", order = 1)]
	public class EnemyCharacterSettingsScriptableObject : ScriptableObject
	{
		[SerializeField] private EnemyCharacterSettings characterSettings;

		public EnemyCharacterSettings GetCharacterSettings() => characterSettings;
	}

	[System.Serializable]
	public struct EnemyCharacterSettings
	{
		public float speed;
		public float range;
		public float attackRange;
	}
}
