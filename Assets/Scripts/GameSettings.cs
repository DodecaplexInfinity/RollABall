using UnityEngine;

namespace Assets.Scripts
{
	[CreateAssetMenu(menuName = "GameSettings", fileName = "GameSettings")]
	public class GameSettings:ScriptableObject
	{
		public BallView BallViewPrefab;
		public Level[] Levels;
		
	}
}