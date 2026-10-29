
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyScalingFactor = 0.75f;
    [SerializeField] private float enemiesPerSecondCap = 15f;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    private int currentWave = 1;

    // Allows WaveUI to read the current wave.
    public int CurrentWave => currentWave;

    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private float eps;
    private bool isSpawning = false;

    private void OnEnable()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    private void OnDisable()
    {
        onEnemyDestroy.RemoveListener(EnemyDestroyed);
    }

    private void Start()
    {
        StartCoroutine(StartWave());
    }

    private void Update()
    {
        if (!isSpawning)
            return;

        timeSinceLastSpawn += Time.deltaTime;

        if (eps > 0f &&
            timeSinceLastSpawn >= 1f / eps &&
            enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }

        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            EndWave();
        }
    }

    private void EnemyDestroyed()
    {
        enemiesAlive = Mathf.Max(0, enemiesAlive - 1);
    }

    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);

        enemiesLeftToSpawn = EnemiesPerWave();
        eps = EnemiesPerSecond();
        isSpawning = true;
    }

    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;

        StartCoroutine(StartWave());
    }

    private void SpawnEnemy()
    {
        if (enemyPrefabs == null || enemyPrefabs.Length == 0)
        {
            Debug.LogError("No enemy prefabs assigned to EnemySpawner!");
            enemiesLeftToSpawn = 0;
            return;
        }

        int index = Random.Range(0, enemyPrefabs.Length);
        GameObject prefabToSpawn = enemyPrefabs[index];

        Instantiate(
            prefabToSpawn,
            LevelManager.main.startPoint.position,
            Quaternion.identity
        );
    }

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(
            baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor)
        );
    }

    private float EnemiesPerSecond()
    {
        return Mathf.Clamp(
            enemiesPerSecond * Mathf.Pow(currentWave, difficultyScalingFactor),
            0f,
            enemiesPerSecondCap
        );
    }
}
