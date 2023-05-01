using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance {get; private set;}

    public event EventHandler<OnNumberClickEventArgs> OnNumberClick;
    public class OnNumberClickEventArgs : EventArgs
    {
        public int e_numberPressed;
    }

    public event EventHandler OnCannonClick;
    public event EventHandler OnRocketClick;
    public event EventHandler OnPauseClick;
    public event EventHandler OnQuickZoomed;

    PlayerInputActions playerInputActions;
    Vector2 movementVector;
    Vector2 zoomVector;
    int numberPressed;

    void OnDestroy()
    {
        playerInputActions.Dispose();
    }

    void Awake()
    {Instance = this;

        playerInputActions = new PlayerInputActions();
        
        playerInputActions.Player.Enable();
        playerInputActions.Player.CannonInput.performed += CannonInput_Performed;
        playerInputActions.Player.RocketJump.performed += RocketJump_Performed;
        playerInputActions.Player.Pause.performed += Pause_Performed;
        playerInputActions.Player.QuickZoom.performed += QuickZoom_Performed;
        Keyboard.current.onTextInput += OnTextInput;

    }

    void OnTextInput(char inputChar)
    {
        if(char.IsDigit(inputChar))
        {
            numberPressed = int.Parse(inputChar.ToString());
            
            OnNumberClick?.Invoke(this, new OnNumberClickEventArgs
            {
                e_numberPressed = numberPressed
            });
        }
    }

    void RocketJump_Performed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        OnRocketClick?.Invoke(this,EventArgs.Empty);
    }

    void QuickZoom_Performed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        OnQuickZoomed?.Invoke(this, EventArgs.Empty);
    }

    void Pause_Performed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        OnPauseClick?.Invoke(this, EventArgs.Empty);
    }

    void CannonInput_Performed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        OnCannonClick?.Invoke(this, EventArgs.Empty); //The cannon being clicked and it shooting are different events
    }


    public Vector2 GetMovementVector()
    {
        movementVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
        return movementVector;
    }

    public Vector2 GetZoomVector()
    {
        zoomVector = playerInputActions.Player.Zoom.ReadValue<Vector2>();

        return zoomVector;
    }
}
