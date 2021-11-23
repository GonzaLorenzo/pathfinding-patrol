﻿using System.Collections;
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

    void Start()
    {
        _fsm = new StateMachine();
        _fsm.AddState(EnemyStatesEnum.Patrol, new PatrollingState(_fsm, this));
        _fsm.AddState(EnemyStatesEnum.Pursuit, new PursuingState(_fsm, this));
        _fsm.ChangeState(EnemyStatesEnum.Patrol);
    }

    void Update()
    {
        FieldOfView();
    }

    /// <summary>
    /// Chequea con Raycast hacia el proximo waypoint si hay obstaculos en el medio, si los hay empieza a usar A*, si no los hay patrulla normalmente.
    /// </summary>
    public void Patrol()
    {
        Vector2 dir = waypoints[_currentWaypoint].position - transform.position;
        transform.forward = dir;
        transform.position += transform.forward * speed * Time.deltaTime;

        if (dir.magnitude < 0.1f)
        {
            _currentWaypoint++;
            if (_currentWaypoint > waypoints.Count - 1)
                _currentWaypoint = 0;
        }
    }
    void FieldOfView()
    {
        Collider2D[] allTargets = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetMask);

        foreach (var item in allTargets)
        {
            Debug.Log("Tengo target");
            Vector2 dir = item.transform.position - transform.position;
            
            if (Vector2.Angle(transform.forward, dir.normalized) < viewAngle / 2)
            {
                //if(Physics2D.Raycast(transform.position, dir, dir.magnitude, obstacleMask) == false)
                RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, dir.magnitude, obstacleMask);
                if(hit == false)
                {
                    item.GetComponent<Renderer>().material.color = Color.red;
                    //_targetsFound.Add(item.gameObject);

                    Debug.DrawLine(transform.position, item.transform.position, Color.green);

                    Debug.Log("Te encontré");
                }
                else
                {
                    Debug.DrawLine(transform.position, hit.point, Color.red);
                    Debug.Log("Donde tas?");
                }
            }
        }
    }
}