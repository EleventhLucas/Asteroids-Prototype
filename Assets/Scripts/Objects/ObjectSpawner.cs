using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectType
{
    PLAYER,
    ASTEROID,
    UFO,
    TRASH,
    HEALTH,
    INVINCIBILITY,
    DOUBLESHOT
}

// Using this because Unity doesn't support serializing dictionaries.
[System.Serializable]
public class ObjectToSpawn
{
    public ObjectType key;
    public GameObject value;
}

/// <summary>
/// Tunable object spawner for automated/random or manual object spawning.
/// <br></br>
/// Uses <seealso cref="BoundaryManager.ScreenBounds"/> as a clamp for random.
/// </summary>
public class ObjectSpawner : MonoBehaviour
{
    // Externals
    [SerializeField] private ObjectToSpawn[] objectsToSpawn;
    [SerializeField] private float spawnTime;
    [SerializeField] private uint initialSpawns;

    // Internals
    private BoundaryManager boundaryManager;
    private float spawnTimer;

    private void Awake()
    {
        spawnTimer = spawnTime;
    }

    void Start()
    {
        boundaryManager = BoundaryManager.Instance;
        for (int i = 0; i < initialSpawns; i++)
        {
            SpawnObject();
        }
    }

    private void Update()
    {
        // Timers
        spawnTimer = spawnTimer > 0 ? spawnTimer - Time.deltaTime : 0f;

        if (spawnTimer <= 0)
        {
            SpawnObject();
            spawnTimer = spawnTime;
        }
    }

    #region SpawnObject Methods

    private void SpawnObject(Vector3 position, float rotation, ObjectType objectType, int splitStage)
    {
        GameObject temp = Instantiate(SearchObjectDictionary(objectType), position, Quaternion.Euler(new Vector3(0f, rotation, 0f)));
        temp.GetComponent<Asteroid>().splitStage = splitStage;
    }
    /// <summary>
    /// Random object spawning (no parameters).
    /// </summary>
    public void SpawnObject()
    {
        float rotation = Random.Range(-90, 90);
        bool randomAxis = Random.value > 0.5f;
        bool randomAxisSide = Random.value > 0.5f;
        Vector3 position = new Vector3(
            randomAxis ? Random.Range(-boundaryManager.ScreenBounds.x, boundaryManager.ScreenBounds.x) : randomAxisSide ? boundaryManager.ScreenBounds.x : -boundaryManager.ScreenBounds.x,
            0,
            !randomAxis ? Random.Range(-boundaryManager.ScreenBounds.y, boundaryManager.ScreenBounds.y) : randomAxisSide ? boundaryManager.ScreenBounds.y : -boundaryManager.ScreenBounds.y);
        SpawnObject(position, rotation, ObjectType.ASTEROID, 0); // TODO: change it to select a random object
    }
    /// <summary>
    /// Random object spawning with amount.
    /// </summary>
    public void SpawnObject(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            SpawnObject();
        }
    }
    /// <summary>
    /// Asteroid spawning.
    /// </summary>
    public void SpawnObject(Vector3 position, int splitStage)
    {
        float rotation = Random.Range(-90, 90);
        SpawnObject(position, rotation, ObjectType.ASTEROID, splitStage);
    }

    #endregion

    private GameObject SearchObjectDictionary(ObjectType key)
    {
        foreach(ObjectToSpawn o in objectsToSpawn)
        {
            if (o.key == key)
            {
                return o.value;
            }
        }
        return null;
    }
}
