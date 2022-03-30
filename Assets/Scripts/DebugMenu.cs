using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMenu : MonoBehaviour
{
    // Externals
    [SerializeField] private GameObject debugMenuDisplay;
    [SerializeField] private GameObject UIDisplay;

    private void Awake()
    {
        UIDisplay.SetActive(true);
        debugMenuDisplay.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            debugMenuDisplay.SetActive(!debugMenuDisplay.activeInHierarchy);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            UIDisplay.SetActive(!UIDisplay.activeInHierarchy);
        }
    }
}
