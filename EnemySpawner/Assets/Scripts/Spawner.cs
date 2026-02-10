using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private Enemy _prefab;
    
    [SerializeField] private int _poolMaxSize;
    [SerializeField] private int _poolCapacity;

    [SerializeField] private float _spawnInterval;

    private ObjectPool<Enemy> _pool;
    private List<Vector3> _direction;

    private void Awake()
    {
        _pool = new ObjectPool<Enemy>(
            createFunc: Create,
            actionOnGet: (obj) => OnGet(obj),
            actionOnRelease: (obj) => obj.gameObject.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
        );

        _direction = new List<Vector3>() { Vector3.left, Vector3.right, Vector3.back, Vector3.forward };
    }

    private void Start()
    {
        _spawnPoints = new List<Transform>(_spawnPoints);

        StartCoroutine(SpawnTimer());
    }

    private Vector3 GetRandomSpawnPosition()
    {
        int randomIndex = UserUtils.GenerateRandomNumber(0, _spawnPoints.Count);

        return _spawnPoints[randomIndex].transform.position;
    }

    private Vector3 GetRandomDirection()
    {
        return _direction[Random.Range(0, _direction.Count)];
    }

    private Enemy Create()
    {
        Enemy enemy = Instantiate(_prefab);

        return enemy;
    }

    private void SpawnEnemy()
    {
        Enemy enemy = _pool.Get();
    }

    private void OnGet(Enemy enemy)
    {
        Vector3 position = GetRandomSpawnPosition();
        Vector3 direction = GetRandomDirection();

        enemy.Initialize(position, direction);

        enemy.gameObject.SetActive(true);

        enemy.DestroyTime += OnRelease;
    }

    private void OnRelease(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
        enemy.SetDefaultSettings();

        ReturnToPool(enemy);
    }

    private void ReturnToPool(Enemy enemy)
    {
        _pool.Release(enemy);

        enemy.DestroyTime -= OnRelease;
    }

    private IEnumerator SpawnTimer()
    {
        var wait = new WaitForSeconds(_spawnInterval);

        while (enabled)
        {
            SpawnEnemy();

            yield return wait;
        }
    }
}
