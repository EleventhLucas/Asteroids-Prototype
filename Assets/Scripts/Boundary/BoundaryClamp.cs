using System;
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
    [Header("Trails can have visual defects when mirrored.")]
    [SerializeField] private TrailRenderer trail;

    // Internals
    private BoundaryManager boundaryManager;
    private bool hasTrail;

    private void Awake()
    {
        hasTrail = false;
        try
        {
            trail.Clear();
            hasTrail = true;
        }
        catch (Exception e)
        {
        }
    }

    void Start()
    {
        boundaryManager = BoundaryManager.Instance;
    }

    void Update()
    {
        DoClamp();
    }

    private void DoClamp()
    {
        // Gather Data
        Vector3 pos = this.transform.position;
        Vector2 boundary = boundaryManager.ScreenBounds;

        // Crunch numbers
        if (mirrorToOtherSide)
        {
            if (pos.x > boundary.x || pos.x < -boundary.x)
            {
                pos.x = -pos.x;
                if (hasTrail)
                {
                    trail.Clear();
                }

            }
            if (pos.z > boundary.y || pos.z < -boundary.y)
            {
                pos.z = -pos.z;
                if (hasTrail)
                {
                    trail.Clear();
                }

            }
        }
        pos.x = Mathf.Clamp(pos.x, -boundary.x, boundary.x);
        pos.z = Mathf.Clamp(pos.z, -boundary.y, boundary.y);
        
        // Apply
        this.transform.position = pos;
    }
}
