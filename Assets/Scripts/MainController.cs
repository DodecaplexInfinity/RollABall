using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
	public class MainController:MonoBehaviour
	{
		[SerializeField] private GameSettings _gameSettings;
		[SerializeField] private LevelsController _levelsController;
		[SerializeField] private CameraController _cameraController;
		[SerializeField] private UIController _uiController;
		[SerializeField] private Transform _ballTransform;

		void Start()
		{
			OnStart(); //TODO Temporary. Make game start on button
			_levelsController.Initialize(_gameSettings);
			_cameraController.Initialize(Camera.main, _ballTransform);
			_uiController.Initialize(OnRespawn, OnExit, OnInput, OnStart);
			_levelsController.SpawnLevel(_ballTransform, true);
		}

		private void OnStart()
		{
			var startingPosition = _levelsController.GetStartingPoint();
			var ballView = Instantiate(_gameSettings.BallViewPrefab, startingPosition, Quaternion.identity);
			ballView.Initialize(OnLevelEnd, OnRespawn);
			_ballTransform = ballView.transform;
		}

		void OnLevelEnd()
		{
			//Match results
			//Delete old level
			_levelsController.SpawnLevel(_ballTransform);
			_levelsController.InitializeCurrentLevel();
		}
		
		private void OnExit()
		{
			Application.Quit();
		}

		void OnRespawn()
		{
			Destroy(this);
			SceneManager.LoadScene("Game");
		}

		void OnInput(Vector2 input)
		{
			_levelsController.SetInput(input);
		}
	}
}