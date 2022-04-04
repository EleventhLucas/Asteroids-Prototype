using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMenu : MonoBehaviour
{
    // Externals
    [SerializeField] private GameObject debugMenuDisplay;
    [SerializeField] private GameObject UIDisplay;

    // Internals
    private GenericInputActions inputActions;

    private void Awake()
    {
        inputActions = new GenericInputActions();

        UIDisplay.SetActive(true);
        debugMenuDisplay.SetActive(false);
    }

    private void Update()
    {
        if (inputActions.General.Pause.triggered)
        {
            debugMenuDisplay.SetActive(!debugMenuDisplay.activeInHierarchy);
        }
        if (inputActions.General.Hide.triggered)
        {
            UIDisplay.SetActive(!UIDisplay.activeInHierarchy);
        }
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
