using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingState : IState
{
    private StateMachine _fsm;
    private Pathfinding _pf;
    private Enemy _enemy;
    public List<Node> myPath;
    private int _currentWaypoint;
    private int _currentPathWaypoint = 0;
    private Node startingPoint;
    private Node endingPoint;

    public PathfindingState(StateMachine fsm, Enemy p, Pathfinding pf)
    {
        _fsm = fsm;
        _enemy = p;
        _pf = pf;
    }

    public void OnStart()
    {
        GetAStar();
        Debug.Log("Entré a pathfinding");
    }
    public void OnUpdate()
    {
        _enemy._patrolPath = myPath;

        _enemy.FieldOfView();
        if(_enemy.foundTarget)
        {
            _fsm.ChangeState(EnemyStatesEnum.Pursuit);
            _enemy.AlertEnemies(_enemy.transform);
        }

        if(myPath != null)
        {
                if(myPath.Count >= 1)
            {
                Vector2 dir = myPath[_currentPathWaypoint].transform.position - _enemy.transform.position;

                _enemy.transform.up = dir;
                _enemy.transform.position += _enemy.transform.up * _enemy.speed * Time.deltaTime;

                if (dir.magnitude < 0.1f)
                {
                    _currentPathWaypoint++;
                    if (_currentPathWaypoint > myPath.Count - 1)
                    {
                        _enemy.foundWaypoint = true;
                        _enemy.beenAlerted = false;
                        _enemy.alarmPosition = null;
                        _fsm.ChangeState(EnemyStatesEnum.Patrol);
                    }
                }
            }
        }   

    }
    public void OnExit()
    {
        Debug.Log("Sali de Pathfinding");
        myPath = null;
        _currentPathWaypoint = 0;
    }

    public void GetAStar()
    {
        myPath = new List<Node>();

        startingPoint = GameManager.instance.GetStartNode(_enemy.transform);
        Debug.Log("Start at " + startingPoint);
        
        if(_enemy.alarmPosition != null)
        {
            endingPoint = GameManager.instance.GetEndNode(_enemy.alarmPosition);
        }
        else
        {
            _currentWaypoint = _enemy.GetCurrentWaypoint();
        
            endingPoint = GameManager.instance.GetEndNode(_enemy.waypoints[_currentWaypoint].transform);
            Debug.Log("End at " + endingPoint);
        }

        myPath = _pf.ConstructPathAStar(endingPoint, startingPoint);
    }
}
