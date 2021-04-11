using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsController : MonoBehaviour
{
	[SerializeField] private LevelsData levelsData;
	[SerializeField] private float _rotationSpeed = 1f;
    [SerializeField] private float _axisClamp = 8f;

    private Queue<Level> _levelsQueue = new Queue<Level>();

    private Level _currentLevel;

    private float _levelDistance = 20f;

    [ContextMenu("SpawnLevel")]
    public void SpawnLevel(Transform ballTransform, bool isStartingLevel = false)
    {
	    var levelPrefab = levelsData.GetNewLevel(!isStartingLevel);
	    var spawnPoint = Vector3.zero;
	    if (!isStartingLevel)
	    {
		    spawnPoint = _currentLevel.endPoint.position
		                 + new Vector3(0, -_levelDistance, 0)
		                 + (-levelPrefab.transform.TransformPoint(levelPrefab.startingPoint.localPosition));
	    }
	    
	    var newLevel = Instantiate(levelPrefab, spawnPoint, levelPrefab.transform.rotation);
	    
	    _currentLevel = newLevel;
        _levelsQueue.Enqueue(newLevel);
        
        if (!isStartingLevel)
        {
			InitializeCurrentLevel(ballTransform);    
        }
        
        if(_levelsQueue.Count >= 3)
        {
	        DestroyPreviousLevel();
        }
    }
    
    public Vector3 GetStartingPoint()
    {
	   return levelsData.GetLevelStartingPoint(levelsData.CurrentLevelID);
    }

    public void SetInput(Vector2 input)
    {
	    _currentLevel.SetInput(input);
    }

    private void DestroyPreviousLevel()
    {
	    var queuedLevel = _levelsQueue.Dequeue();
	    Destroy(queuedLevel.gameObject);
    }

    public void InitializeCurrentLevel(Transform ballTransform)
    {
	    _currentLevel.Initialize(ballTransform, _rotationSpeed, _axisClamp); 
    }
}
