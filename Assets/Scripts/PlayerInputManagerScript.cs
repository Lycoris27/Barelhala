using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManagerScript : MonoBehaviour
{

    private InputSystem inputSystem;
    public static event System.Action<Vector2> OnMovePerformed;
    public static event System.Action OnInteractPerformed;
    private Vector2 moveInput;

    private void Awake()
    {
        inputSystem = new InputSystem();
    }

    private void OnEnable()
    {
        inputSystem.Player.Move.performed += OnMove;
        inputSystem.Player.Move.canceled += StopMove;
        inputSystem.Player.Interact.performed += OnInteract;
        inputSystem.Enable();

    }
    private void OnDisable()
    {
        inputSystem.Player.Move.performed -= OnMove;
        inputSystem.Player.Move.canceled -= StopMove;
        inputSystem.Disable();
    }
    private void Update()
    {
        OnMovePerformed?.Invoke(moveInput);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    public void StopMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    public void OnInteract(InputAction.CallbackContext context)
    {
        print("Sent Signal to Interact");
        OnInteractPerformed?.Invoke();
    }
}