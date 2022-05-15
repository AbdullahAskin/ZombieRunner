using UnityEngine;

namespace TheyAre
{
	public class SharedFunctions : MonoBehaviour
	{

		public static int GetRandomExcept(int range,int except)
		{
			var selectedRandom = Random.Range(0, range - 1);
			if (selectedRandom >= except) selectedRandom += 1;
			return selectedRandom;
		}
	}
}
