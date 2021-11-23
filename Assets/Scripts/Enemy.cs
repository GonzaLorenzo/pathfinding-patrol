using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    StateMachine _fsm;
    public List<Transform> waypoints;
    public float speed;
    private int _currentWaypoint = 0;

    [Header("Field of view")]
    public float viewRadius;
    public float viewAngle;
    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public Color targetDefaultColor;
    private List<GameObject> _targetsFound = new List<GameObject>();
    void Start()
    {
        _fsm = new StateMachine();
        _fsm.AddState(EnemyStatesEnum.Patrol, new PatrollingState(_fsm, this));
        _fsm.AddState(EnemyStatesEnum.Pursuit, new PursuingState(_fsm, this));
        _fsm.ChangeState(EnemyStatesEnum.Patrol);
    }

    void Update()
    {
        
    }

    /// <summary>
    /// Chequea con Raycast hacia el proximo waypoint si hay obstaculos en el medio, si los hay empieza a usar A*, si no los hay patrulla normalmente.
    /// </summary>
    public void Patrol()
    {
        Vector3 dir = waypoints[_currentWaypoint].position - transform.position;
        transform.forward = dir;
        transform.position += transform.forward * speed * Time.deltaTime;

        if (dir.magnitude < 0.1f)
        {
            _currentWaypoint++;
            if (_currentWaypoint > waypoints.Count - 1)
                _currentWaypoint = 0;
        }
    }

    private void ClearFoundTargetList()
    {
        foreach (var item in _targetsFound)
        {
            item.GetComponent<Renderer>().material.color = targetDefaultColor;
        }
        _targetsFound.Clear();
    }
    void FieldOfView()
    {
        ClearFoundTargetList();

        Collider[] allTargets = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        foreach (var item in allTargets)
        {
            Vector3 dir = item.transform.position - transform.position;

            if (Vector3.Angle(transform.forward, dir.normalized) < viewAngle / 2)
            {
                if(Physics.Raycast(transform.position, dir, out RaycastHit hit, dir.magnitude, obstacleMask) == false)
                {
                    item.GetComponent<Renderer>().material.color = Color.red;
                    _targetsFound.Add(item.gameObject);

                    Debug.DrawLine(transform.position, item.transform.position, Color.red);
                }
                else
                {
                    Debug.DrawLine(transform.position, hit.point, Color.red);
                }
            }
        }
    }
}
