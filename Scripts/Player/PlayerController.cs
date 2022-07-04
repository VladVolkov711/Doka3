using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Health;

    public float Speed;
    [SerializeField] private joystick _joystick;

    [HideInInspector] public Animator Anim;

    private Rigidbody2D _rb;
    private Transform _tr;

    private void Start()
    {
        Speed = 3;
        _rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
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

        _rb.velocity = new Vector2(x * Speed, y * Speed);

        if (y > 0.5f) transform.localScale = new Vector3(1f, 1f, 1f);
        if (y < -0.5f) transform.localScale = new Vector3(1f, -1f, 1f);

        if (x != 0 || y != 0) Anim.Play("Walk");

    }
}
