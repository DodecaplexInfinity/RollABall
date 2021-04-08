using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsController : MonoBehaviour
{
	[SerializeField] private LevelStack _levelStack;
    [SerializeField] private LevelSpawner _levelSpawner;

    [SerializeField] private float _rotationSpeed = 1f;
    [SerializeField] private float _axisClamp = 8f;

    // private Level _currentLevel;
    // private Level _previousLevel;
    //TODO add queue for levels

    private Queue<Level> _levelsQueue = new Queue<Level>();

    private Level _currentLevel;

    
    private float _levelDistance = 20f;

    public void Initialize(GameSettings settings)
    {
	    _levelStack.Initialize(settings.Levels);
    }

    [ContextMenu("SpawnLevel")]
    public void SpawnLevel(Transform ballTransform, bool isStartingLevel = false)
    {
	    var levelPrefab = _levelStack.GetNewLevel(!isStartingLevel);
	    var spawnPoint = Vector3.zero;
	    if (!isStartingLevel)
	    {
		    spawnPoint = _currentLevel.endPoint.position
		                 + new Vector3(0, -_levelDistance, 0)
		                 + (-levelPrefab.transform.TransformPoint(levelPrefab.startingPoint.localPosition));
	    }
	    var newLevel = _levelSpawner.SpawnLevel(levelPrefab, spawnPoint);
        
        newLevel.Initialize(ballTransform, _rotationSpeed, _axisClamp);

        // _previousLevel = _currentLevel;
        _currentLevel = newLevel;
        _levelsQueue.Enqueue(newLevel); 
        
        if(_levelsQueue.Count >= 3)
        {
	        DestroyPreviousLevel();
        }
        
	    // _currentLevel = newLevel;
    }
    
    public Vector3 GetStartingPoint()
    {
	   return _levelStack.GetLevelStartingPoint(_levelStack.CurrentLevelID);
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

    public void InitializeCurrentLevel()
    {
	   
    }
}
