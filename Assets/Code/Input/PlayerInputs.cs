using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    public static PlayerInputs Instance { get; private set; }

    public event Action OnPrimaryPressed;
    public event Action OnPrimaryHeld;
    public event Action OnPrimaryReleased;
    public event Action OnSecondaryPressed;
    public event Action OnSecondaryHeld;
    public event Action OnSecondaryReleased;
    public event Action OnInteractPressed;
    public event Action OnInteractHeld;
    public event Action OnInteractReleased;    

    public Vector2 MoveInput;
    public float ZoomInput;

    public PlayerControls Controls;

    private Gamepad gamepad;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        if (Gamepad.current != null)
        {
            gamepad = Gamepad.current;
        }

        Controls = new PlayerControls();

        Controls.Actions.Primary.performed += PrimaryPressed;
        Controls.Actions.Primary.canceled += PrimaryReleased;
        Controls.Actions.Secondary.performed += SecondaryPressed;
        Controls.Actions.Secondary.canceled += SecondaryReleased;
        Controls.Actions.Interact.performed += InteractPressed;
        Controls.Actions.Interact.canceled += InteractReleased;
    }

    private void PrimaryPressed(InputAction.CallbackContext context)
    {
        OnPrimaryPressed?.Invoke();
    }

    private void PrimaryReleased(InputAction.CallbackContext context)
    {
        OnPrimaryReleased?.Invoke();
    }

    private void SecondaryPressed(InputAction.CallbackContext context)
    {
        OnSecondaryPressed?.Invoke();
    }

    private void SecondaryReleased(InputAction.CallbackContext context)
    {
        OnSecondaryReleased?.Invoke();
    }

    private void InteractPressed(InputAction.CallbackContext context)
    {
        OnInteractPressed?.Invoke();
    }

    private void InteractReleased(InputAction.CallbackContext context)
    {
        OnInteractReleased?.Invoke();
    }

    #region Enabling/Disabling Action Maps
    private void Start()
    {
        Controls.Enable();
    }

    private void OnEnable()
    {
        Controls.Enable();
    }

    private void OnDisable()
    {
        Controls.Disable();
    }

    private void OnDestroy()
    {
        Controls.Disable();
    }
    #endregion

    void Update()
    {
        HandleInputs();
    }

    private void HandleInputs()
    {
        MoveInput = Controls.Movement.Move.ReadValue<Vector2>();
        ZoomInput = Controls.Movement.Zoom.ReadValue<float>();

        if (Controls.Actions.Primary.phase == InputActionPhase.Performed)
        {
            OnPrimaryHeld?.Invoke();
        }

        if (Controls.Actions.Secondary.phase == InputActionPhase.Performed)
        {
            OnSecondaryHeld?.Invoke();
        }

        if (Controls.Actions.Interact.phase == InputActionPhase.Performed)
        {
            OnInteractHeld?.Invoke();
        }
    }

    #region Haptics

    public void StartHapticFeedback(float intensity, float frequency)
    {
        gamepad?.SetMotorSpeeds(intensity, frequency);
    }

    public void StopHapticFeedback()
    {
        gamepad?.SetMotorSpeeds(0, 0);
    }

    #endregion
}
