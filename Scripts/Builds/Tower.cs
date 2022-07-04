using System.Collections;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public static int CountTower;

    [SerializeField] private Damageble _damageble;
    [SerializeField] private Health _health;

    [SerializeField] private LayerMask _layerMask;

    // поиск цели
    [SerializeField] private Collider2D[] _target;
    [SerializeField] private Transform _visiblePosition;

    // радиус видимости
    [SerializeField] private float _visibleRange;

    // объект для нападения
    [SerializeField] private Transform _targetForDamage;

    // буфер, максимальное число для найденых объектов
    private Collider2D[] _buffer = new Collider2D[5];
    private int _bufferIndex = 0;

    private float _distance;

    private void Start() => CountTower = 2;

    private void Update()
    {
        if (_health._health <= 0)
        {
            CountTower--;
            Destroy(gameObject);
        }

        _bufferIndex = Physics2D.OverlapCircleNonAlloc(_visiblePosition.position, _visibleRange, _buffer, _layerMask);
        if (_bufferIndex > 0 && _targetForDamage == null) FindeTarget();
        if (_bufferIndex == 0) _targetForDamage = null;

        Distance();
    }

    public void FindeTarget()
    {
        for (int i = 0; i < _bufferIndex; i++)
        {
            if (_buffer[i].GetComponent<Health>())
            {
                _targetForDamage = _buffer[i].transform;
                StartCoroutine(SpawnStone());
            }
        }
    }

    private void Distance()
    {
        if (_targetForDamage != null)
            _distance = Vector2.Distance(gameObject.transform.position, _targetForDamage.position);

        if (_distance > 10)
        {
            StopAllCoroutines();
            _targetForDamage = null;
            FindeTarget();
        }
    }

    public IEnumerator SpawnStone()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.8f);

            if(_targetForDamage == null) yield break;

            else
            {
                if (_targetForDamage.GetComponent<Health>()._health <= 0) _targetForDamage = null;
                else _damageble.TakeDamage(_targetForDamage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_visiblePosition.position, _visibleRange);
    }
}
