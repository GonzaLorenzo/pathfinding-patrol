using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float _hMov;
    private float _vMov;
    private Vector3 _velocity;

    public float speed = 1.5f;

    void Update()
    {
        Inputs();
        Move();
    }

    void Inputs()
    {
        _hMov = Input.GetAxisRaw("Horizontal");
        _vMov = Input.GetAxisRaw("Vertical");
    }

    void Move()
    {
        _velocity.Set(_hMov, _vMov, 0);
        _velocity.Normalize();
        transform.position += _velocity * speed * Time.deltaTime;
    }
}
