using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnEnemyPoint : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoints;
    [SerializeField] private Enemy _prefab;
    [SerializeField] private Transform _targetPositon;

    [SerializeField] private float _spawnInterval;

    [SerializeField] private int _poolMaxSize;
    [SerializeField] private int _poolCapacity;

    private ObjectPool<Enemy> _pool;

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
    }

    private void Start()
    {
        StartCoroutine(SpawnTimer());
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

        enemy.Initialize(transform.position, _targetPositon);

        enemy.gameObject.SetActive(true);

        enemy.DestroyEnemy += OnRelease;
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

        enemy.DestroyEnemy -= OnRelease;
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
