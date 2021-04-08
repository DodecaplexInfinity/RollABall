using UnityEngine;
namespace Assets.Scripts
{
	public class LevelSpawner:MonoBehaviour
	{
		public Level SpawnLevel(Level prefab, Vector3 spawnPoint)
		{
			var newLevel = Instantiate(prefab, spawnPoint, prefab.transform.rotation);
			return newLevel;
		}
	}
}