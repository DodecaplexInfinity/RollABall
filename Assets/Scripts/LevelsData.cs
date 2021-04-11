using System;
using UnityEngine;

namespace Assets.Scripts
{
	[Serializable]
	public class LevelsData
	{
		[SerializeField] private Level[] _levels;

		private int _currentLevelID = 0;
		public int CurrentLevelID => _currentLevelID;

		public Level GetNewLevel(bool shift)
		{
			if(shift) _currentLevelID++;
			
			if (_currentLevelID == _levels.Length)
			{
				_currentLevelID = 0;
			}
			
			return _levels[_currentLevelID];
		}

		public Vector3 GetLevelStartingPoint(int levelId)
		{
			return _levels[levelId].startingPoint.position;
		}
	}
}