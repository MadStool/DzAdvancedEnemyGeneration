using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private Target _target;

    public Target Target => _target;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.25f);

        if (_target != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, _target.transform.position);
        }
    }
}