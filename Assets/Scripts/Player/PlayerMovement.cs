using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spaceship movement.
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    // Externals
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float accelerationSpeed;
    [SerializeField] private float boostSpeed;
    [SerializeField] private float topSpeed;

    // Internals
    private bool boosted;

    public void ProcessInput(PlayerInputStruct input)
    {
        // Forward
        if (input.forward)
        {
            if (!boosted)
            {
                playerRigidbody.AddForce(playerRigidbody.transform.forward * boostSpeed, ForceMode.Impulse);
                boosted = true;
            }
            playerRigidbody.AddForce(playerRigidbody.transform.forward * accelerationSpeed, ForceMode.Acceleration);
        }
        else
        {
            boosted = false;
        }

        // Rotation
        if (input.left ^ input.right)
        {
            float yRot = 0;
            yRot -= input.left ? 1f : 0f;
            yRot += input.right ? 1f : 0f;
            playerRigidbody.transform.Rotate(0f, yRot * rotationSpeed * Time.deltaTime, 0f, Space.Self);
        }

        // Clamping
        playerRigidbody.velocity = Vector3.ClampMagnitude(playerRigidbody.velocity, topSpeed);
    }
}