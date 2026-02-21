using UnityEngine;
using UnityEngine.Pool;

public class SpawnerAdventurer : MonoBehaviour
{
    [SerializeField] private Adventurer _prefab;
    
    [SerializeField] private int _poolMaxSize;
    [SerializeField] private int _poolCapacity;

    private ObjectPool<Adventurer> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Adventurer>(
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
        Spawn();
    }

    private Adventurer Create()
    {
        Adventurer adventurer = Instantiate(_prefab);

        return adventurer;
    }

    private void Spawn()
    {
        Adventurer adventurer = _pool.Get();
    }

    private void OnGet(Adventurer adventurer)
    {
        adventurer.Initialize(transform.position);

        adventurer.gameObject.SetActive(true);
    }

    private void OnRelease(Adventurer adventurer)
    {
        adventurer.gameObject.SetActive(false);

        ReturnToPool(adventurer);
    }

    private void ReturnToPool(Adventurer adventurer)
    {
        _pool.Release(adventurer);
    }
}
