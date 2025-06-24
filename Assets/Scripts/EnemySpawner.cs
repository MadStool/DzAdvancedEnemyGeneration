using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private float _spawnInterval = 2f;
    [SerializeField] private float _enemySpeed = 3f;

    [Header("Spawn Points")]
    [SerializeField] private SpawnPoint[] _spawnPoints;

    private Coroutine _spawningCoroutine;
    private bool _isSpawning;

    private void OnEnable() => Initialize();
    private void OnDisable() => StopSpawning();

    private void Initialize()
    {
        if (ValidateSpawnSettings())
            StartSpawning();
    }

    public void StartSpawning()
    {
        if (_isSpawning)
            return;

        _isSpawning = true;
        _spawningCoroutine = StartCoroutine(SpawnProcess());
    }

    public void StopSpawning()
    {
        if (_isSpawning == false)
            return;

        _isSpawning = false;

        if (_spawningCoroutine != null)
            StopCoroutine(_spawningCoroutine);
    }

    private bool ValidateSpawnSettings()
    {
        if (_enemyPrefab == null)
        {
            Debug.LogError("Enemy prefab missing!", this);
            return false;
        }

        if (_spawnPoints == null || _spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points assigned!", this);
            return false;
        }

        foreach (var point in _spawnPoints)
        {
            if (point.Target == null)
            {
                Debug.LogError($"Spawn point {point.name} has no target assigned!", this);
                return false;
            }
        }

        return true;
    }

    private IEnumerator SpawnProcess()
    {
        var wait = new WaitForSeconds(_spawnInterval);

        while (_isSpawning)
        {
            yield return wait;
            CreateEnemyAtRandomPoint();
        }
    }

    private void CreateEnemyAtRandomPoint()
    {
        SpawnPoint point = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
        Enemy enemy = Instantiate(_enemyPrefab, point.transform.position, Quaternion.identity);
        enemy.Setup(point.Target.transform, _enemySpeed);
    }
}