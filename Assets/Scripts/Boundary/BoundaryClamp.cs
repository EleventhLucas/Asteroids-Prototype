using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Uses <seealso cref="BoundaryManager"/> instance's data to clamp an object's transform or mirror it to another side.
/// </summary>
public class BoundaryClamp : MonoBehaviour
{
    // Externals
    [SerializeField] private bool mirrorToOtherSide;

    // Internals
    private Vector2 boundary;
    private bool initialized = false;

    void Start()
    {
        initialized = false;
        GetBoundaryData();
    }

    void Update()
    {
        if (initialized)
            DoClamp();
    }

    private void GetBoundaryData()
    {
        boundary = BoundaryManager.Instance.ScreenBounds;
        Debug.Log("BoundaryClamp: Screen boundary size is x=" + boundary.x + " y=" + boundary.y);
        initialized = true;
    }

    private void DoClamp()
    {
        // Gather Data
        Vector3 pos = this.transform.position;

        // Crunch numbers
        if (mirrorToOtherSide)
        {
            if (pos.x > boundary.x || pos.x < -boundary.x)
            {
                pos.x = -pos.x;
            }
            if (pos.z > boundary.y || pos.z < -boundary.y)
            {
                pos.z = -pos.z;
            }
        }
        pos.x = Mathf.Clamp(pos.x, -boundary.x, boundary.x);
        pos.z = Mathf.Clamp(pos.z, -boundary.y, boundary.y);
        
        // Apply
        this.transform.position = pos;
    }
}
