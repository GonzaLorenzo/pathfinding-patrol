using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursuingState : IState
{
    private StateMachine _fsm;
    private Enemy _enemy;
    private float _timer;
    
    public PursuingState(StateMachine fsm, Enemy p)
    {
        _fsm = fsm;
        _enemy = p;
    }

    public void OnStart()
    {
        Debug.Log("Entré a Pursuit");
        //Avisar a los demás, veremos como.
    }

    public void OnUpdate()
    {
        _enemy.FieldOfView();

        Vector2 dir = _enemy.target.GetComponent<Transform>().position - _enemy.transform.position;
        _enemy.transform.up = dir;
        _enemy.transform.position += _enemy.transform.up * _enemy.speed * Time.deltaTime;

        if(!_enemy.foundTarget)
        {
            _enemy.ResetCurrentWaypointIndex();
            _enemy.ResetWaypoints();
            _fsm.ChangeState(EnemyStatesEnum.Patrol);
        }
    }
    
    public void OnExit()
    {
        Debug.Log("Salí de Pursuit");
    }
}
