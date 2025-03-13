using UnityEngine;
using UnityEngine.InputSystem;

public class touch : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction touchPositionAction;
    private InputAction touchPressAction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        touchPositionAction = playerInput.actions["TouchPosition"];
    }
    private void OnEnable()
    {
        touchPressAction.performed += Pressed;
    }
    private void OnDisable()
    {
        touchPressAction.performed -= Pressed;
    }
    private void Pressed(InputAction.CallbackContext context)
    {
        Vector2 position = touchPositionAction.ReadValue<Vector2>();
        Debug.Log(position);
    }
}
