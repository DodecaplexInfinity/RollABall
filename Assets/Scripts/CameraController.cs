using UnityEngine;

namespace Assets.Scripts
{
	public class CameraController:MonoBehaviour
	{		
		private Camera _gameCamera;
		private Transform _ballTransform;
		
		[SerializeField] private Vector3 _cameraOffset;

		public void Initialize(Camera gameCamera, Transform ballTransform)
		{
			_gameCamera = gameCamera;
			_ballTransform = ballTransform;
		}
		
		void FixedUpdate()
		{
			_gameCamera.transform.position = _ballTransform.position + _cameraOffset;
		}
	}
}