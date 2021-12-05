﻿using System.IO;
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

        _currentWaypoint = _enemy.GetCurrentWaypoint();
        
        endingPoint = GameManager.instance.GetEndNode(_enemy.waypoints[_currentWaypoint].transform);
        Debug.Log("End at " + endingPoint);

        myPath = _pf.ConstructPathAStar(endingPoint, startingPoint); //Antes mandaba startingPoint,endingPoint. Ahora van al revés.
    }
}
