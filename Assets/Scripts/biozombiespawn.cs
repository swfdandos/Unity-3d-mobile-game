using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class biozombiespawn : MonoBehaviour
{

    public GameObject enemyPrefab;
    public int maxEnemies = 10;
    public float spawnDelay = 1f;
    public float spawnRange = 15f;
    public float mapBoundary = 20f;
    public Vector2 spawnAreaSize = new Vector2(10f, 10f);

    private int spawnedEnemies = 0;

    private void Start()
    {
        Camera.main.transform.position = new Vector3(0f, 10f, 0f);
        Camera.main.transform.rotation = Quaternion.Euler(60f, 45f, 0f);

        InvokeRepeating("SpawnEnemy", spawnDelay, spawnDelay);
    }

    private void SpawnEnemy()
    {
        if (spawnedEnemies >= maxEnemies)
        {
            CancelInvoke("SpawnEnemy");
            return;
        }

        Vector3 spawnPosition = GetRandomSpawnPosition();
        spawnPosition = ClampPositionToMap(spawnPosition);

        GameObject enemyObject = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        enemyObject.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        enemyObject.GetComponent<Rigidbody>().useGravity = true;

        spawnedEnemies++;
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float halfWidth = spawnAreaSize.x / 2f;
        float halfHeight = spawnAreaSize.y / 2f;

        float randomX = Random.Range(-halfWidth, halfWidth);
        float randomZ = Random.Range(-halfHeight, halfHeight);

        Vector3 spawnPosition = new Vector3(randomX, 0f, randomZ) + transform.position;
        return spawnPosition;
    }

    private Vector3 ClampPositionToMap(Vector3 position)
    {
        float minX = -mapBoundary + spawnRange;
        float maxX = mapBoundary - spawnRange;
        float minZ = -mapBoundary + spawnRange;
        float maxZ = mapBoundary - spawnRange;

        position.x = Mathf.Clamp(position.x, minX, maxX);
        position.z = Mathf.Clamp(position.z, minZ, maxZ);

        return position;
    }

}
