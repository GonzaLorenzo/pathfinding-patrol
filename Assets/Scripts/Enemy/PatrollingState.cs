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
        
    }
    public void OnUpdate()
    {
        _enemy.Patrol();
        _enemy.FieldOfView();

        if(_enemy.foundTarget)
        {
            Debug.Log("Gracias dios");
        }
        //Cuando encuentra al Player pasa a PursuitState.
    }
    public void OnExit()
    {
        Debug.Log("Sali de Patrol");
    }
}
