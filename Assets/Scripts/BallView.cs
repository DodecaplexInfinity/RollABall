using System;
using UnityEngine;

public class BallView : MonoBehaviour
{
    private Action _onLevelEnd;
    private Action _onRespawn;
    
    public void Initialize(Action onLevelEnd, Action onRespawn)
    {
        _onLevelEnd = onLevelEnd;
        _onRespawn = onRespawn;
    }

    private void OnTriggerEnter(Collider other)
    {
        var colliderTag = other.gameObject.tag;
        switch (colliderTag)
        {
            case "Finish":
                _onLevelEnd();
                break;
            case "Respawn":
                _onRespawn();
                break;
        }
    }
}
