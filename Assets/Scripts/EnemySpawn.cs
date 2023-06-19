    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    /* public GameObject enemyPrefab; // D��man prefab'�
     public int maxEnemies; // Spawn edilecek maksimum d��man say�s�
     public float spawnDelay; // Spawn aral��� (saniye)
     public float spawnRange = 15f; // Spawn aral��� (birim)

     private int spawnedEnemies = 0; // �u ana kadar spawn edilmi� d��man say�s�

     public float mapBoundary = 20f; // Harita s�n�rlar�

     private void Start()
     {
         // Kamera y�ksekli�i ve rotasyonu ayarla
         Camera.main.transform.position = new Vector3(0f, 10f, 0f);
         Camera.main.transform.rotation = Quaternion.Euler(60f, 45f, 0f);

         StartCoroutine(SpawnEnemies());
     }

     private IEnumerator SpawnEnemies()
     {
         while (spawnedEnemies < maxEnemies)
         {
             yield return new WaitForSeconds(spawnDelay);

             // Karakterin y�ksekli�ini al
             float playerY = transform.position.y;

             // Rastgele spawn pozisyonu olu�tur
             Vector3 spawnPosition = new Vector3(Random.Range(-spawnRange, spawnRange), playerY, Random.Range(-spawnRange, spawnRange));
             spawnPosition = ClampPositionToMap(spawnPosition);

             // D��man� spawn et
             GameObject enemyObject = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
             enemyObject.transform.localScale = new Vector3(1f, 1f, 1f);

             enemyObject.GetComponent<Rigidbody>().useGravity = true; // Yer�ekimini etkinle�tir

             spawnedEnemies++;
         }

         // Maksimum d��man say�s�na ula��ld���nda yap�lacak i�lemler
         // Coroutine'i durdur
         StopCoroutine(SpawnEnemies());
     }

     private Vector3 ClampPositionToMap(Vector3 position)
     {
         // Harita s�n�rlar�n� kontrol et ve pozisyonu s�n�rla
         float minX = -mapBoundary;
         float maxX = mapBoundary;
         float minZ = -mapBoundary;
         float maxZ = mapBoundary;

         position.x = Mathf.Clamp(position.x, minX, maxX);
         position.z = Mathf.Clamp(position.z, minZ, maxZ);

         return position;
     }*/

    /* public GameObject enemyPrefab;
     public int maxEnemies;
     public float spawnDelay;
     public float spawnRange = 15f;
     private int spawnedEnemies = 0;
     public float mapBoundary = 20f;

     private void Start()
     {
         Camera.main.transform.position = new Vector3(0f, 10f, 0f);
         Camera.main.transform.rotation = Quaternion.Euler(60f, 45f, 0f);
         StartCoroutine(SpawnEnemies());
     }

     private IEnumerator SpawnEnemies()
     {
         while (spawnedEnemies < maxEnemies)
         {
             yield return new WaitForSeconds(spawnDelay);
             float playerY = transform.position.y;

             // Seed de�erini g�ncelle
             int seed = (int)(Time.time * 1000);
             Random.InitState(seed);

             Vector3 spawnPosition = new Vector3(Random.Range(-spawnRange, spawnRange), playerY, Random.Range(-spawnRange, spawnRange));
             spawnPosition = ClampPositionToMap(spawnPosition);
             GameObject enemyObject = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
             enemyObject.transform.localScale = new Vector3(1f, 1f, 1f);
             enemyObject.GetComponent<Rigidbody>().useGravity = true;
             spawnedEnemies++;
         }
         StopCoroutine(SpawnEnemies());
     }

     private Vector3 ClampPositionToMap(Vector3 position)
     {
         float minX = -mapBoundary;
         float maxX = mapBoundary;
         float minZ = -mapBoundary;
         float maxZ = mapBoundary;
         position.x = Mathf.Clamp(position.x, minX, maxX);
         position.z = Mathf.Clamp(position.z, minZ, maxZ);
         return position;
     }*/

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
        enemyObject.transform.localScale = new Vector3(1f, 1f, 1f);
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

