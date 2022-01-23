using UnityEngine;

public class Bulled : MonoBehaviour
{
    [HideInInspector] public Transform _target;
    public float _speed;
    public float _damage;

    private void Start()
    {
        _damage = 25;
    }

    private void FixedUpdate()
    {
        if (_target == null) Destroy(gameObject);
        else transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DarkKrip") && _target.GetComponent<DarkKrip>())
        {
            _target.GetComponent<DarkKrip>().TakeDamage(_damage);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("LightKrip") && _target.GetComponent<LightKrip>())
        {
            _target.GetComponent<LightKrip>().TakeDamage(_damage);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("DarkKrip") && _target.GetComponent<DarkLongkrip>())
        {
            _target.GetComponent<DarkLongkrip>().TakeDamage(_damage);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("LightKrip") && _target.GetComponent<LightLongKrip>())
        {
            _target.GetComponent<LightLongKrip>().TakeDamage(_damage);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("DarkTower") && _target.GetComponent<TowerDark>())
        {
            _target.GetComponent<TowerDark>().TakeDamage(_damage);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("LightTower") && _target.GetComponent<TowerLight>())
        {
            _target.GetComponent<TowerLight>().TakeDamage(_damage);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            //_target.GetComponent<PlayerController>().TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
}
