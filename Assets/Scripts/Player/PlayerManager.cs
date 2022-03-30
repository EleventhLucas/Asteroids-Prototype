using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct InputStruct
{
    public bool fire;

    public bool forward;
    public bool backward;
    public bool left;
    public bool right;
}

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
    private InputStruct input;

    private void Update()
    {
        GrabInputs();
        playerMovement.ProcessInput(input);
        playerGun.ProcessInput(input);
    }

    private void GrabInputs()
    {
        // TODO: use new input system rather than old input system
        input.forward = Input.GetKey(KeyCode.W);
        input.backward = Input.GetKey(KeyCode.S);
        input.left = Input.GetKey(KeyCode.A);
        input.right = Input.GetKey(KeyCode.D);
        input.fire = Input.GetKey(KeyCode.Space);
    }
}
