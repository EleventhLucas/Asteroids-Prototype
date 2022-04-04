using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public struct PlayerInputStruct
{
    public bool fire;

    public bool forward;
    public bool backward;
    public bool left;
    public bool right;
}

/// <summary>
/// Manages input and data for classes to hook from.
/// </summary>
/// <remarks>
/// Note: This is not a performance-oriented class - that would be unnecessary.
/// <br></br>
/// Libraries like Photon Fusion will delta-compress the data.
/// </remarks>
public class InputManager : MonoBehaviour
{
    /// Externals
    // Singleton Pattern
    public static InputManager instance;
    // Data
    public PlayerInputStruct playerInput;

    /// Internals
    private GenericInputActions inputActions;
    // Events
    public delegate void PlayerInputHandler();
    public event PlayerInputHandler PlayerInput;

    private void Awake()
    {
        #region Singleton Pattern
        if (instance != this || instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning(this.name + " failed the Singleton Pattern. There was already one in the scene?");
            Destroy(this);
        }
        #endregion

        inputActions = new GenericInputActions();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }
}
