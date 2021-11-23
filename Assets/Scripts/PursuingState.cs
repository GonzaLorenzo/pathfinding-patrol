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
        
    }

    public void OnUpdate()
    {
        
    }
    
    public void OnExit()
    {
        
    }
}
