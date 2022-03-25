using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Uses an Orthographic camera-view to determine boundaries for the game.
/// <br></br>
/// Contains a public ScreenBounds property for <seealso cref="BoundaryClamp"/> to use.
/// </summary>
public class BoundaryManager : MonoBehaviour
{
    // Singleton Pattern
    public static BoundaryManager Instance { get; private set; }

    // Externals
    [HideInInspector] public Camera _mainCamera;

    // Internals/Properties
    public Vector2 ScreenBounds => _screenBounds;
    private Vector2 _screenBounds;

    private void Awake()
    {
        #region Singleton Pattern
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        #endregion

        // TODO: Optional, this code would become deprecated with multiple cameras.
        _mainCamera = Camera.main;

        if (!_mainCamera.orthographic)
            Debug.LogError("BoundaryManager: Will not work properly if the main camera is not orthographic!");

        _screenBounds.x = _mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0, Screen.height)).x;
        _screenBounds.y = -_mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0, Screen.height)).z;
        Debug.Log("BoundaryManager: Screen boundary size is x=" + _screenBounds.x + " y=" + _screenBounds.y);
    }
}
