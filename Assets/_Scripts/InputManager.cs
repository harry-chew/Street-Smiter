using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    [HideInInspector] public InputAction touchPressAction;
    [HideInInspector] public InputAction primaryPositionAction;
    public bool touchHeld;
    public Vector2 mousePosition;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        touchPressAction = playerInput.actions["TouchPress"];
        primaryPositionAction = playerInput.actions["PrimaryPosition"];
    }

    private void OnEnable()
    {
        touchPressAction.started += ctx => StartedTouchPress(ctx);
        touchPressAction.canceled += ctx => EndedTouchPress(ctx);
    }

    private void OnDisable()
    {
        touchPressAction.started -= ctx => StartedTouchPress(ctx);
        touchPressAction.canceled -= ctx => EndedTouchPress(ctx);
    }
    private void Update()
    {
        mousePosition = primaryPositionAction.ReadValue<Vector2>();
    }

    private void StartedTouchPress(InputAction.CallbackContext ctx)
    {
        float value = ctx.ReadValue<float>();
        if (value == 1) touchHeld = true;
        mousePosition = primaryPositionAction.ReadValue<Vector2>();
        Debug.Log(value);
    } 

    private void EndedTouchPress(InputAction.CallbackContext ctx)
    {
        float value = ctx.ReadValue<float>();
        //Debug.Log(value);
        if (value == 0) touchHeld = false;
    }

    
}
