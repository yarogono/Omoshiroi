using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.LowLevel;

[Serializable]
public struct TouchAreaData
{
    public eTouchAreaType AreaType;
    public Vector2 CenterPoint;
    public float Radius;
}

public class PlayerInput : BaseInput, ThirdPersonController.ITestActions, ThirdPersonController.IPlayerActions
{
    public ThirdPersonController InputActions { get; private set; }

#if UNITY_EDITOR
    public ThirdPersonController.TestActions TestActions { get; private set; }
#else
    public ThirdPersonController.PlayerActions PlayerActions { get; private set; }
#endif
    private CharacterMovement _movement;

    [Header("컨트롤 범위 설정")]
    [SerializeField] private List<TouchAreaData> _TouchArea;

    [SerializeField] private TMP_Text MoveTestTxt;
    [SerializeField] private TMP_Text AimTestTxt;
    [SerializeField] private TMP_Text DodgeTestTxt;

    Dictionary<int, (eTouchAreaType, Action<TouchState>)> _touchAction;
    bool[] _touchAreaOccupied;

    private void Awake()
    {
        _movement = GetComponent<CharacterMovement>();

        _touchAction = new Dictionary<int, (eTouchAreaType, Action<TouchState>)>();

        _touchAreaOccupied = new bool[Enum.GetValues(typeof(eTouchAreaType)).Length];
        for (int i = 0; i < _touchAreaOccupied.Length; i++)
            _touchAreaOccupied[i] = false;

        InputActions = new ThirdPersonController();

#if UNITY_EDITOR
        TestActions = InputActions.Test;
        TestActions.AddCallbacks(this);
#else
        PlayerActions = InputActions.Player;
        PlayerActions.AddCallbacks(this);
#endif
#if DEVELOPMENT_BUILD
        OnMoveEvent += (t) => { MoveTestTxt.text = $"{t}"; };
        OnAimEvent += (t) => { AimTestTxt.text = $"{t}"; };
        OnDodgeEvent += (t) => { DodgeTestTxt.text = $"{t}"; };
#endif
        OnMoveEvent += (t) => { MoveCharacter(t); };
    }

    private void Reset()
    {
        _touchAction.Clear();
        for (int i = 0; i < _touchAreaOccupied.Length; i++)
            _touchAreaOccupied[i] = false;
    }

    private void OnEnable()
    {
        InputActions.Enable();
    }

    private void OnDisable()
    {
        InputActions.Disable();
    }

    // 터치 조작
    public void OnCancelTouch(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Application.Quit();
        }
    }

    public void OnHoldTouch(InputAction.CallbackContext context)
    {
        var touch = context.ReadValue<TouchState>();

        if (touch.phase == UnityEngine.InputSystem.TouchPhase.Began)
        {
            // 터치 영역을 확인
            eTouchAreaType area = CheckTouchArea(touch.startPosition);
            // 터치 아이디와 행동 함수를 연결
            if (!CheckAreaOccupied(area))
            {
                ConnectToAreaAction(area, touch);
            }
        }
        else if (touch.phase == UnityEngine.InputSystem.TouchPhase.Moved)
        {
            // 터치 아이디에 연결된 행동 함수를 호출
            CallConnectAction(touch.touchId, touch);
        }
        else if (touch.phase == UnityEngine.InputSystem.TouchPhase.Ended)
        {
            // 터치 아이디에 연결된 행동 함수를 호출
            CallConnectAction(touch.touchId, touch);
            // 연결 삭제
            RemoveAreaAction(touch.touchId);
        }
    }

    private void CallConnectAction(int touchId, TouchState touch)
    {
        if (_touchAction.TryGetValue(touchId, out (eTouchAreaType, Action<TouchState>) connectedTouch))
            connectedTouch.Item2?.Invoke(touch);
    }

    private void RemoveAreaAction(int touchId)
    {
        if (_touchAction.ContainsKey(touchId))
        {
            _touchAreaOccupied[(int)_touchAction[touchId].Item1] = false;
            _touchAction.Remove(touchId);
        }
    }

    private void ConnectToAreaAction(eTouchAreaType area, TouchState touch)
    {
        _touchAreaOccupied[(int)area] = true;
        Action<TouchState> action;
        switch(area)
        {
            case eTouchAreaType.Move:
                action = MoveAction;
                break;
            case eTouchAreaType.AimFire:
                action = AimEvent;
                break;
            case eTouchAreaType.DodgeRun:
                action = DodgeRunEvent;
                break;
            default:
                action = null;
                break;
        }
        _touchAction.Add(touch.touchId, (area, action));
    }

    private void MoveAction(TouchState touch)
    {
        if (touch.phase == UnityEngine.InputSystem.TouchPhase.Ended)
            CallMoveEvent(Vector3.zero);
        else
            CallMoveEvent(touch.position - touch.startPosition);
    }

    private void AimEvent(TouchState touch)
    {
        CallAimEvent(touch.position - touch.startPosition);
    }

    private void DodgeRunEvent(TouchState touch)
    {
        CallDodgeEvent(touch.position - touch.startPosition);
    }

    private bool CheckAreaOccupied(eTouchAreaType area)
    {
        return _touchAreaOccupied[(int)area];
    }

    public void OnTapTouch(InputAction.CallbackContext context)
    {

    }

    private eTouchAreaType CheckTouchArea(Vector2 position)
    {
        eTouchAreaType touchArea = eTouchAreaType.None;
        foreach (var area in _TouchArea)
        {
            if ((position - area.CenterPoint).magnitude < area.Radius)
            {
                touchArea = area.AreaType;
                break;
            }
        }
        return touchArea;
    }

    private void CheckTouchID(int id)
    {

    }

    // 키보드 마우스 조작
    public void OnAim(InputAction.CallbackContext context)
    {

    }

    public void OnFire(InputAction.CallbackContext context)
    {

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Vector2 dir = context.ReadValue<Vector2>();
            CallMoveEvent(dir);
        }
        else
        {
            CallMoveEvent(Vector3.zero);
        }
    }

    private void MoveCharacter(Vector2 direction)
    {
        Vector3 forward = Camera.main.transform.forward;
        forward.y = 0;
        forward.Normalize();
        Vector3 right = Camera.main.transform.right;
        right.y = 0;
        right.Normalize();

        direction.Normalize();

        _movement.ControlMove(forward * direction.y + right * direction.x);
    }
}