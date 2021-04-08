using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private Rigidbody[] levelObjects;

    public Transform startingPoint;
    public Transform endPoint;

    private Transform _playerTransform;
    private Quaternion _targetRotation;

    private float _rotationSpeed;
    private float _axisClamp;

    public void Initialize(Transform targetObject, float rotationSpeed, float axisClamp)
    {
        _rotationSpeed = rotationSpeed;
        _axisClamp = axisClamp;
        _playerTransform = targetObject;
        _targetRotation = transform.localRotation;
    }

    void FixedUpdate()
    {
        UpdatePosition();
    }

    public void SetInput(Vector2 input)
    {
        _targetRotation *= Quaternion.Euler(-input.x * _rotationSpeed, 0, -input.y * _rotationSpeed);
        _targetRotation = ClampRotation(_targetRotation);
        _targetRotation.Normalize();
        UpdateRotation(_targetRotation);
    }
            
    private void UpdateRotation(Quaternion targetRotation)
    {
        transform.localRotation = targetRotation;
    }

    private void UpdatePosition()
    {
        if (_playerTransform == null) return;

        var levelTransform = transform;
        var position = _playerTransform.position;
        var delta = position - levelTransform.position;
        
        levelTransform.position = position;

        foreach (var go in levelObjects)
        {
            go.transform.position += -delta;
        }
    }
    
    private Quaternion ClampRotation(Quaternion q)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        var angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);
        var angleY = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.z);
        angleX = Mathf.Clamp(angleX, -_axisClamp, _axisClamp);
        angleY = Mathf.Clamp(angleY, -_axisClamp, _axisClamp);
        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);
        q.z = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleY);

        return q;
    }
}
