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
        Debug.Log("Entré a pathfinding");
        myPath = new List<Node>();
        
        startingPoint = GameManager.instance.GetGoalNode(_enemy.transform);
        Debug.Log("Start at " + startingPoint);

        _currentWaypoint = _enemy.GetCurrentWaypoint();
        
        endingPoint = GameManager.instance.GetGoalNode(_enemy.waypoints[_currentWaypoint].transform);
        Debug.Log("End at " + endingPoint);

        myPath = _pf.ConstructPathAStar(endingPoint, startingPoint); //Antes mandaba startingPoint,endingPoint. Ahora van al revés.
    }
    public void OnUpdate()
    {
        if(_enemy.foundTarget)
        {
            myPath = null;
            _enemy._patrolPath = null;
            _fsm.ChangeState(EnemyStatesEnum.Pursuit);
        }

        if(myPath.Count >= 1)
        {
            Vector2 dir = myPath[_currentPathWaypoint].transform.position - _enemy.transform.position;

            _enemy.transform.up = dir;
            _enemy.transform.position += _enemy.transform.up * _enemy.speed * Time.deltaTime;

            if (dir.magnitude < 0.1f)
            {
                //myPath.RemoveAt(_currentPathWaypoint);        Este es el metodo de borrar
                //_currentPathWaypoint++;
                _currentPathWaypoint++;
                if (_currentPathWaypoint > myPath.Count - 1)
                {
                    _enemy.foundWaypoint = true;
                    _fsm.ChangeState(EnemyStatesEnum.Patrol);
                }
                    
            }
        }

    }
    public void OnExit()
    {
        Debug.Log("Sali de Pathfinding");
    }
}
