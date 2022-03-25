using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    // Externals
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float lifeTime;

    // Internals
    private float lifeTimer;

    private void Awake()
    {
        lifeTimer = lifeTime;
    }

    void Update()
    {
        // Timers
        lifeTimer = lifeTimer > 0 ? lifeTimer - Time.deltaTime : 0f;

        if (lifeTimer <= 0)
        {
            Destroy(this.gameObject);
        }

        this.transform.position += this.transform.forward * bulletSpeed * Time.deltaTime;
    }
}
