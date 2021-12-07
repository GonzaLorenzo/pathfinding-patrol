using System.Dynamic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    StateMachine _fsm;
    public List<Transform> waypoints;
    public List<Transform> startingWaypoints;
    public float speed;
    private int _currentWaypoint = 0;
    public Transform alarmPosition = null;
    [Header("FIELD OF VIEW")]
    public float viewRadius;
    public float viewAngle;
    public LayerMask targetMask;
    public bool beenAlerted = false;
    public LayerMask obstacleMask;
    public Pathfinding _pf;
    public Collider2D target;
    public List<Node> _patrolPath = new List<Node>(); 
    public bool foundTarget = false;

    public bool foundWaypoint = true;
    void Awake()
    {
        
        _fsm = new StateMachine();
        _pf = new Pathfinding();
        _fsm.AddState(EnemyStatesEnum.Patrol, new PatrollingState(_fsm, this));
        _fsm.AddState(EnemyStatesEnum.Pursuit, new PursuingState(_fsm, this));
        _fsm.AddState(EnemyStatesEnum.Pathfinding, new PathfindingState(_fsm, this, _pf));
        _fsm.ChangeState(EnemyStatesEnum.Patrol);
    }

    private void Start()
    {
        GameManager.instance.AddEnemies(this);
    }
    void Update()
    {
        _fsm.OnUpdate();
    }

    /// <summary>
    /// Chequea con Raycast hacia el proximo waypoint si hay obstaculos en el medio, si los hay empieza a usar A*, si no los hay patrulla normalmente.
    /// </summary>
    public void Patrol()
    {
        Vector2 dir = waypoints[_currentWaypoint].position - transform.position;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, dir.magnitude, obstacleMask);
        if (hit == false)
        {
            foundWaypoint = true;
            transform.up = dir;
            transform.position += transform.up * speed * Time.deltaTime;

            if (dir.magnitude < 0.1f)
            {
                _currentWaypoint++;
                if (_currentWaypoint > waypoints.Count - 1)
                    _currentWaypoint = 0;
            }
        }
        else
        {
            foundWaypoint = false;
        }
    }
    public void FieldOfView()
    {
        Collider2D[] allTargets = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetMask);

        foreach (var item in allTargets)
        {
            Vector2 dir = item.transform.position - transform.position;
            
            if (Vector2.Angle(transform.up, dir.normalized) < viewAngle / 2)
            {

                RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, dir.magnitude, obstacleMask);
                if(hit == false)
                {
                    item.GetComponent<Renderer>().material.color = Color.red;

                    Debug.DrawLine(transform.position, item.transform.position, Color.green);
                    foundTarget = true;
                    target = item;
                }
                else
                {
                    foundTarget = false;
                    Debug.DrawLine(transform.position, hit.point, Color.red);
                }
            }
        }
    }

    public void AlertEnemies(Transform enemyPosition)
    {
        foreach (Enemy enemy in GameManager.instance.enemies)
        {
            if(enemy != this)
            {
                enemy.target = target;
                enemy.alarmPosition = enemyPosition;
                enemy.beenAlerted = true;
            }
        }
    }

    public int GetCurrentWaypoint()
    {
        return _currentWaypoint;
    }
}
