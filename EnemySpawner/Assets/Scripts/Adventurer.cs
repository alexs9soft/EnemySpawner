using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Animator))]
public class Adventurer : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Animator _animator;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    public void Initialize(Vector3 position)
    {
        transform.position = position;

        _animator.enabled = true;
    }
}
