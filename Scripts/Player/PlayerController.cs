using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Health;
    [SerializeField] private joystick _joystick;
    [SerializeField] private float _speed;

    private Rigidbody2D _rb;
    //private Animator _anim;
    private Transform _tr;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        //_anim = GetComponent<Animator>();
        _tr = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float x = _joystick.Horozontal();
        float y = _joystick.Vertical();

        _rb.velocity = new Vector2(x * _speed, y * _speed);

        if(y < -0.5f) transform.localScale = new Vector3(1f, -1f, 1f);
        else transform.localScale = new Vector3(1f, 1f, 1f);

    }
}
