using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingState : IState
{
    private StateMachine _fsm;
    private Enemy _enemy;

    public PatrollingState(StateMachine fsm, Enemy p)
    {
        _fsm = fsm;
        _enemy = p;
    }

    public void OnStart()
    {
        Debug.Log("Entré a Patrol");
    }
    public void OnUpdate()
    {
        _enemy.Patrol();

        if(!_enemy.foundWaypoint)
        {
            _fsm.ChangeState(EnemyStatesEnum.Pathfinding);
        }

        _enemy.FieldOfView();

        if(_enemy.foundTarget)
        {
            _fsm.ChangeState(EnemyStatesEnum.Pursuit);
        }
    }
    public void OnExit()
    {
        Debug.Log("Sali de Patrol");
    }
}
