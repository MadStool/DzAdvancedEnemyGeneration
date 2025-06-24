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

    private void FixedUpdate()
    {
        if (_rigidbody != null && _target != null)
        {
            Vector3 direction = (_target.position - transform.position).normalized;
            _rigidbody.linearVelocity = direction * _speed;

            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(direction);
            }
        }
    }

    public void Setup(Transform target, float speed)
    {
        _target = target;
        _speed = speed;
    }
}