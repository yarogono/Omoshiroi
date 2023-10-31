using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class PlayerInput : BaseInput, ThirdPersonController.IPlayerActions
{
    public ThirdPersonController InputActions {  get; private set; }
    public ThirdPersonController.PlayerActions PlayerActions { get; private set; }
    private CharacterMovement _movement;

    [SerializeField] private TMP_Text PrimaryTouchTestTxt;
    [SerializeField] private TMP_Text SecondTouchTestTxt;
    [SerializeField] private TMP_Text ThirdTouchTestTxt;

    private void Awake()
    {
        _movement = GetComponent<CharacterMovement>();
        InputActions = new ThirdPersonController();
        PlayerActions = InputActions.Player;

        PlayerActions.AddCallbacks(this);
    }

    private void OnEnable()
    {
        InputActions.Enable();
    }

    private void OnDisable()
    {
        InputActions.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Vector3 forward = Camera.main.transform.forward;
            forward.y = 0;
            forward.Normalize();
            Vector3 right = Camera.main.transform.right;
            right.y = 0;
            right.Normalize();

            Vector2 dir = context.ReadValue<Vector2>();

            _movement.ControlMove(forward * dir.y + right * dir.x);
            //Debug.Log($"Move {forward * dir.y + right * dir.x}");
        }
        else
        {
            _movement.ControlMove(Vector3.zero);
            //Debug.Log($"Stop {Vector3.zero}");
        }
    }

    public void OnAimFire(InputAction.CallbackContext context)
    {
        
    }

    public void OnPrimaryTouch(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            var touch = context.ReadValue<TouchState>();
            PrimaryTouchTestTxt.text = $"{touch.touchId} {touch.startPosition} {touch.delta} {touch.position}";
        }
    }

    public void OnSecondTouch(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            var touch = context.ReadValue<TouchState>();
            SecondTouchTestTxt.text = $"{touch.touchId} {touch.startPosition} {touch.delta} {touch.position}";
        }
    }

    public void OnThirdTouch(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            var touch = context.ReadValue<TouchState>();
            ThirdTouchTestTxt.text = $"{touch.touchId} {touch.startPosition} {touch.delta} {touch.position}";
        }
    }

    public void OnCancelTouch(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            Application.Quit();
    }
}
