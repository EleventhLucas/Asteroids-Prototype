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

        // Movement
        this.transform.position += this.transform.forward * bulletSpeed * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, this.transform.forward, out hit, 0.5f) || Physics.Raycast(transform.position, this.transform.forward, out hit, -0.5f))
        {
            ProcessTarget(hit.collider);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        ProcessTarget(other);
    }

    private void ProcessTarget(Collider other)
    {
        if (other.CompareTag(PlayerManager.ASTEROID_TAG))
        {
            // TODO: points for destruction, etc.
            other.gameObject.GetComponent<Asteroid>().Explode();
            Destroy(this.gameObject);
        }
    }
}
