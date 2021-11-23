using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingState : IState
{
    private StateMachine _fsm;
    private Enemy _enemy;

    public PathfindingState(StateMachine fsm, Enemy p)
    {
        _fsm = fsm;
        _enemy = p;
    }

    public void OnStart()
    {
        
    }
    public void OnUpdate()
    {

    }
    public void OnExit()
    {
        Debug.Log("Sali de Pathfinding");
    }
}
