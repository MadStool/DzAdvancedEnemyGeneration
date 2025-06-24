using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;

    private Rigidbody _rigidbody;
    private Transform _target;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Initialize(Transform target, float speed)
    {
        _target = target;
        _speed = speed;
    }

    private void FixedUpdate()
    {
        if (_rigidbody == null || _target == null) 
            return;

        Vector3 direction = (_target.position - transform.position).normalized;
        _rigidbody.linearVelocity = direction * _speed;

        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}