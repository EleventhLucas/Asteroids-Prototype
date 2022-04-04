using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages input and data to/from classes like <seealso cref="PlayerMovement"/> and <seealso cref="PlayerGun"/>.
/// <br></br>
/// Holds constants like <seealso cref="ASTEROID_TAG"/>.
/// </summary>
public class PlayerManager : MonoBehaviour
{
    // Consts
    public const string ASTEROID_TAG = "Asteroid";
    public const string PLAYER_TAG = "Player";

    // Externals
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerGun playerGun;

    // Internals
    private GenericInputActions inputActions;
    private PlayerInputStruct input;

    private void Awake()
    {
        inputActions = new GenericInputActions();
    }

    private void Update()
    {
        GrabInputs();
        playerMovement.ProcessInput(input);
        playerGun.ProcessInput(input);
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void GrabInputs()
    {
        Vector2 data = inputActions.Player.Move.ReadValue<Vector2>();
        input.forward = data.y > 0;
        input.backward = data.y < 0;
        input.right = data.x > 0;
        input.left = data.x < 0;
        input.fire = inputActions.Player.Fire.ReadValue<float>() > 0 ? true : false;
    }
}
