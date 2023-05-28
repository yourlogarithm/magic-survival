using System.Collections.Generic;
using Entities;
using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnDelay;
    [SerializeField] private float spawnRadius;
    [SerializeField] private Transform player;
    [SerializeField] private List<Enemy> enemyPrefabs;
    [SerializeField] private Canvas worldSpaceCanvas;
    [SerializeField] private TextMeshProUGUI killCountText;
    
    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator<WaitForSeconds>  SpawnEnemies()
    {
        while (true)
        {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized;
            Vector2 spawnPosition = player.position + spawnDirection * spawnRadius;
            Enemy enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
            Enemy enemyInstance = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            enemyInstance.canvas = worldSpaceCanvas;
            enemyInstance.killCountText = killCountText;
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
