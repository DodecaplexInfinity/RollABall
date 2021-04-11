using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
	public class MainController:MonoBehaviour
	{
		[SerializeField] private BallView _ballPrefab;
		[SerializeField] private LevelsController _levelsController;
		[SerializeField] private CameraController _cameraController;
		[SerializeField] private UIController _uiController;
		private Transform _ballTransform;

		void Start()
		{
			_uiController.Initialize(OnRespawn, OnExit, OnInput, OnStart);
			_levelsController.SpawnLevel(_ballTransform, true);
		}

		private void OnStart()
		{
			var startingPosition = _levelsController.GetStartingPoint();
			var ballView = Instantiate(_ballPrefab, startingPosition, Quaternion.identity);
			ballView.Initialize(OnLevelEnd, OnRespawn);
			_ballTransform = ballView.transform;
			_levelsController.InitializeCurrentLevel(_ballTransform);
			_cameraController.Initialize(Camera.main, _ballTransform);
		}

		void OnLevelEnd()
		{
			_levelsController.SpawnLevel(_ballTransform);
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