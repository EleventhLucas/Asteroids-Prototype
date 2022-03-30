using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    // Externals
    [SerializeField] public float speed;
    [SerializeField] private GameObject asteroidDestructionEffect;
    // Hidden
    [HideInInspector] public int splitStage = 0;

    private void Start()
    {
        transform.localScale /= splitStage > 0 ? splitStage*1.337f : 1;
        speed *= splitStage > 0 ? splitStage*1.337f : 1;
    }

    private void Update()
    {
        this.transform.position += this.transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == PlayerManager.PLAYER_TAG)
            Debug.Log(this.gameObject.name + "Hit player.");
        else if (other.gameObject.tag == PlayerManager.ASTEROID_TAG)
            if (other.gameObject.GetComponent<Asteroid>().splitStage > splitStage)
                if (Random.value > 0.5f) // Flip a coin
                    Explode();
    }

    public void Explode()
    {
        splitStage++;
        if (splitStage <= 2)
        {
            ObjectSpawner os = FindObjectOfType<ObjectSpawner>();
            Vector3 randomOffset;
            randomOffset = new Vector3(Random.Range(-1, 1f), Random.Range(-1, 1f), Random.Range(-1, 1f));
            os.SpawnObject(this.transform.position + randomOffset, this.splitStage);
            randomOffset = new Vector3(Random.Range(-1, 1f), Random.Range(-1, 1f), Random.Range(-1, 1f));
            os.SpawnObject(this.transform.position + randomOffset, this.splitStage);
        }
        GameObject explosionEffect = Instantiate(asteroidDestructionEffect, this.transform.position, Quaternion.identity);
        explosionEffect.transform.localScale = transform.localScale;
        Destroy(this.gameObject);
    }
}
