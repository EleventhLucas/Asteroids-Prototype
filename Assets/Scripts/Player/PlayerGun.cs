using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    // Externals
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float semiCooldown;
    [SerializeField] private float autoCooldown;

    // Internals
    private float semiTimer;
    private float autoTimer;
    private bool isAuto;

    private void Update()
    {
        // Timers
        semiTimer = semiTimer > 0 ? semiTimer - Time.deltaTime : 0f;
        autoTimer = autoTimer > 0 ? autoTimer - Time.deltaTime : 0f;
    }

    public void ProcessInput(InputStruct input)
    {
        if (input.fire)
        {
            if (semiTimer <= 0 && !isAuto)
            {
                Fire();
                semiTimer = semiCooldown;
                autoTimer = autoCooldown;
                isAuto = true;
            }
            else if (autoTimer <= 0 && isAuto)
            {
                Fire();
                autoTimer = autoCooldown;
            }
        }
        else
        {
            isAuto = false;
        }
    }

    private void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint);
        bullet.transform.parent = null;
    }
}
