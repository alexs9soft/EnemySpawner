using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(MoverEnemy), typeof(Animator))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _minLifeTime;
    [SerializeField] private float _maxLifeTime;

    private Rigidbody _rigidbody;
    private MoverEnemy _mover;
    private Animator _animator;

    private Vector3 _direction;
    
    private float _timeDestroy;

    public event Action<Enemy> DestroyTime;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _mover = GetComponent<MoverEnemy>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(CountTimer());
    }

    private void Update()
    {
        _mover.Move(_direction, _speed, _rotationSpeed);
    }

    public void Initialize(Vector3 position, Vector3 direction)
    {
        _timeDestroy = UnityEngine.Random.Range(_minLifeTime, _maxLifeTime);

        transform.position = position;
        _direction = direction;

        _animator.enabled = true;
    }

    public void SetDefaultSettings()
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;

        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.linearVelocity = Vector3.zero;

        _animator.enabled = false;
    }

    private IEnumerator CountTimer()
    {
        var wait = new WaitForSeconds(_timeDestroy);

        yield return wait;

        DestroyTime?.Invoke(this);
    }
}
