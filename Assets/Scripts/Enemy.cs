using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    StateMachine _fsm;
    public List<Transform> waypoints;
    public float speed;
    private int _currentWaypoint = 0;
    void Start()
    {
        
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
}
