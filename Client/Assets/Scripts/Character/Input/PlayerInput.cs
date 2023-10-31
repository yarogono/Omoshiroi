using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerInput : BaseInput, ThirdPersonController.IPlayerActions
{
    public ThirdPersonController InputActions {  get; private set; }
    public ThirdPersonController.PlayerActions PlayerActions { get; private set; }
    private CharacterMovement _movement;
    private Touchscreen _touchscreen;

    private void Awake()
    {
        _movement = GetComponent<CharacterMovement>();
        InputActions = new ThirdPersonController();
        PlayerActions = InputActions.Player;

        PlayerActions.AddCallbacks(this);


        // TouchScreen
        _touchscreen = InputSystem.GetDevice<Touchscreen>();
    }

    private void OnEnable()
    {
        InputActions.Enable();
    }

    private void OnDisable()
    {
        InputActions.Disable();
    }

    public void OnTouch(InputAction.CallbackContext context)
    {
        var item = context.ReadValue<Touch>();
        Debug.Log($"{item.fingerId} => {item.position}, {item.deltaPosition}, {item.deltaTime}");
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Vector3 forward = Camera.main.transform.forward;
            Vector3 right = Camera.main.transform.right;

            Vector2 dir = context.ReadValue<Vector2>();

            _movement.ControlMove(forward * dir.y + right * dir.x);
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            _movement.ControlMove(Vector3.zero);
        }
    }

    public void OnAimFire(InputAction.CallbackContext context)
    {
        
    }
}
