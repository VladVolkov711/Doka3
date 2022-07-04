using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public MoneyAnim _moneyAnim;
    [SerializeField] private SearchPath _searchPath; // поиск пути
    [SerializeField] private Damageble _damageble; // нанесение урона
    [SerializeField] private Health _health; // хп

    // отображение радара для поиска
    [SerializeField] private Transform _visiblePosition;
    [SerializeField] private float _visibleRange;

    [SerializeField] private float _speed;
    [SerializeField] private float _stopedDistance;

    //public bool IsDie;
    [SerializeField] private LayerMask _layerMask;

    // цель для движения
    [SerializeField] private Transform _targetForMove;

    // атака врага
    private bool _isAtack;

    // хешируем трансформ врага
    private Transform _enemyTR;

    // вычисление дистанции
    private float _distance;

    // буфер, максимальное число для найденых объектов
    private Collider2D[] _buffer = new Collider2D[5];
    private int _bufferIndex = 0;

    // компоненты моба
    private Rigidbody2D _rb;
    private Collider2D _collider2D;
    private Animator _anim;

    public int MoneyForKrip;

    private void Start() => StartComponent();

    private void FixedUpdate()
    {
        if (_health.IsDie == true)
        {
            StopAllCoroutines();
            _collider2D.enabled = false;
            _visibleRange = 0;
            return;
        }

        _bufferIndex = Physics2D.OverlapCircleNonAlloc(_visiblePosition.position, _visibleRange, _buffer, _layerMask);

        if (_bufferIndex > 0) _targetForMove = _buffer[0].transform;
        else if(_bufferIndex == 0) _targetForMove = null;

        Move();
    }

    public void Move()
    {
        if (_targetForMove != null)
        {
            _distance = Vector2.Distance(_targetForMove.position, _enemyTR.position);
            _enemyTR.up = _targetForMove.position - _enemyTR.position;

            if (_distance < _stopedDistance && _isAtack == false) StartCoroutine(GiveDamage());

            if (_distance > _stopedDistance)
                _enemyTR.position = Vector3.MoveTowards(_enemyTR.position, _targetForMove.position, _speed);
        }
        else
        {
            //_anim.Play("WalKKripDark");
            var target = _searchPath._childPath[_searchPath._chieldPathCount];
            _enemyTR.position = Vector3.MoveTowards(_enemyTR.position, target.position, _speed);
            _enemyTR.up = target.position - _enemyTR.position;

            if (_enemyTR.position == target.position)
            {
                _searchPath._chieldPathCount++;
                if (_searchPath._chieldPathCount >= _searchPath._path[_searchPath._currentPath].transform.childCount) Destroy(gameObject);
            }
        }
    }

    public IEnumerator GiveDamage()
    {
        _isAtack = true;
        while (true)
        {
            yield return new WaitForSeconds(1);
            if(_targetForMove == null)
            {
                _isAtack = false;
                yield break;
            }
            else
            {
                //_anim.Play("AttackKripDark");
                if (_targetForMove.GetComponent<Health>()._health <= 0) _targetForMove = null;
                else _damageble.TakeDamage(_targetForMove);
            }
        }
    }

    protected void StartComponent()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<Collider2D>();
        //_anim = GetComponent<Animator>();
        _enemyTR = GetComponent<Transform>();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_visiblePosition.position, _visibleRange);
    }
}