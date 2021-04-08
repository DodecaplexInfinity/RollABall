using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIController : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _startButton;
    [SerializeField] private Image _touchScreen;
    [SerializeField] private float _damping = 10f;

    private Vector2 _startDragPoint;
    private Action<Vector2> _onInput;

    public void Initialize(Action onRespawn, Action onExit, Action<Vector2> onInput, Action onStart)
    {
        _restartButton.onClick.AddListener(() => onRespawn());
        _exitButton.onClick.AddListener(() => onExit());
        _startButton.onClick.AddListener(() => onStart());
        _onInput = onInput;
    }

    public void RemoveListeners()
    {
        _restartButton.onClick.RemoveAllListeners();
        _exitButton.onClick.RemoveAllListeners();
        _startButton.onClick.RemoveAllListeners();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _startDragPoint = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(eventData.position == _startDragPoint) return;
        
        var input = eventData.position - _startDragPoint;
        _startDragPoint = eventData.position;
        input.Normalize();
        _onInput(input);
    }
    
}
