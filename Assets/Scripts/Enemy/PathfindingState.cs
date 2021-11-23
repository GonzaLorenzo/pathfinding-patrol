using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingState : IState
{
    private StateMachine _fsm;
    private Enemy _enemy;
    private int _currentWaypoint;
    private Node startingPoint;
    private Node endingPoint;

    public PathfindingState(StateMachine fsm, Enemy p)
    {
        _fsm = fsm;
        _enemy = p;
    }

    public void OnStart()
    {
        Debug.Log("Entré a pathfinding");
        
        startingPoint = GameManager.instance.GetGoalNode(_enemy.transform);
        Debug.Log("Start at " + startingPoint);

        _currentWaypoint = _enemy.GetCurrentWaypoint();
        
        endingPoint = GameManager.instance.GetGoalNode(_enemy.waypoints[_currentWaypoint].transform);
        Debug.Log("End at " + endingPoint);

        GameManager.instance.SearchPath(startingPoint, endingPoint);
    }
    public void OnUpdate()
    {
        if(_enemy.foundTarget)
        {
            _fsm.ChangeState(EnemyStatesEnum.Pursuit);
        }
    }
    public void OnExit()
    {
        Debug.Log("Sali de Pathfinding");
    }
}
