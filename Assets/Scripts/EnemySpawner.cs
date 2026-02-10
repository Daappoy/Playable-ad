using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //multiple spawnpoints
    public GameObject[] spawnPoints;
    public GameObject enemyPrefab;
    public float spawnInterval = 3f;
    public int enemyCount = 0;
    public int maxEnemies = 5;
    private float timer = 0f;
    
    //spawn every 3 seconds on a random spawn point
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }
    
    private void SpawnEnemy()
    {
        // Fix logic: if prefab is null OR we've reached max, do nothing
        if (spawnPoints.Length == 0 || enemyPrefab == null || enemyCount >= maxEnemies) return;
        int randomIndex = Random.Range(0, spawnPoints.Length);
        GameObject spawnPoint = spawnPoints[randomIndex];
        var enemyGO = Instantiate(enemyPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
        
        // Subscribe to enemy death event so we can decrement count
        var enemy = enemyGO.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.onDeath.AddListener(OnEnemyDeath);
            // Register enemy with GameManager to add score exactly once
            if (GameManager.Instance != null)
            {
                GameManager.Instance.RegisterEnemy(enemy);
            }
        }
        
        enemyCount++;
    }
    
    private void OnEnemyDeath(int score)
    {
        Debug.Log("Enemy died, reducing enemy count. Score: " + score);
        enemyCount = Mathf.Max(0, enemyCount - 1);
    }
}
