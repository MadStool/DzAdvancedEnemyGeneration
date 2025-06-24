using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _waypointReachThreshold = 0.1f;

    private int _currentWaypointIndex = 0;

    private void Update()
    {
        if (_waypoints == null || _waypoints.Length == 0) 
            return;

        Transform currentWaypoint = _waypoints[_currentWaypointIndex];
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, _moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, currentWaypoint.position) < _waypointReachThreshold)
        {
            _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, 0.25f);

        if (_waypoints != null && _waypoints.Length > 0)
        {
            Gizmos.color = Color.cyan;

            for (int i = 0; i < _waypoints.Length; i++)
            {
                if (_waypoints[i] != null)
                {
                    Gizmos.DrawLine(
                        i == 0 ? transform.position : _waypoints[i - 1].position,
                        _waypoints[i].position);

                    if (i == _waypoints.Length - 1)
                    {
                        Gizmos.DrawLine(_waypoints[i].position, transform.position);
                    }
                }
            }
        }
    }
}