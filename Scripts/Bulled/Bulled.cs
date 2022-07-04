using UnityEngine;

public class Bulled : MonoBehaviour
{
    [HideInInspector] public Transform _target;
    public float _speed;
    public float _damage;
    public float _distance;

    private void Start() => _damage = 25;

    private void FixedUpdate()
    {
        if (_target == null)
        {
            Destroy(gameObject);
            return;
        }
        else transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed);

        _distance = Vector2.Distance(gameObject.transform.position, _target.position);
        if(_distance < 0.5f)
        {
            _target.GetComponent<Health>().CheckHealth(_damage);
            Destroy(gameObject);
        }
    }
}
