using UnityEngine;

public class Target : MonoBehaviour
{
    private const float WaypointGizmoRadius = 0.25f;
    private const int NextWaypointIndexIncrement = 1;

    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _waypointReachThreshold = 0.1f;

    private int _currentWaypointIndex;
    private float _sqrWaypointThreshold;

    private void Awake()
    {
        _sqrWaypointThreshold = _waypointReachThreshold * _waypointReachThreshold;
    }

    private void Update()
    {
        if (_waypoints == null || _waypoints.Length == 0)
            return;

        Transform currentWaypoint = _waypoints[_currentWaypointIndex];
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, _moveSpeed * Time.deltaTime);

        if ((transform.position - currentWaypoint.position).sqrMagnitude < _sqrWaypointThreshold)
        {
            _currentWaypointIndex = GetNextWaypointIndex();
        }
    }

    private int GetNextWaypointIndex()
    {
        return _currentWaypointIndex + NextWaypointIndexIncrement % _waypoints.Length;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, WaypointGizmoRadius);

        if (_waypoints == null || _waypoints.Length == 0)
            return;

        Gizmos.color = Color.cyan;

        for (int i = 0; i < _waypoints.Length; i++)
        {
            if (_waypoints[i] == null)
                continue;

            Vector3 startPoint = i == 0 ? transform.position : _waypoints[i - 1].position;
            Gizmos.DrawLine(startPoint, _waypoints[i].position);

            bool isLastWaypoint = i == _waypoints.Length - 1;

            if (isLastWaypoint)
            {
                Gizmos.DrawLine(_waypoints[i].position, transform.position);
            }
        }
    }
}