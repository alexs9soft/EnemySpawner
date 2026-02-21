using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(MoverEnemy), typeof(Animator))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;

    private Rigidbody _rigidbody;
    private MoverEnemy _mover;
    private Animator _animator;

    private Transform _targetPosition;

    public event Action<Enemy> DestroyEnemy;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _mover = GetComponent<MoverEnemy>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _mover.Move(_targetPosition.position, _speed, _rotationSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Adventurer _))
        {
            DestroyEnemy?.Invoke(this);
        }
    }

    public void Initialize(Vector3 position, Transform targetPosition)
    {
        transform.position = position;
        _targetPosition = targetPosition;

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
}