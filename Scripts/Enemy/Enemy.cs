using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float Health;

    [SerializeField] protected Image _imgHealth;
    [SerializeField] protected MoneyAnim _moneyAnim;

    // радительский объект пути
    [SerializeField] protected GameObject[] _path;

    // состояния врага
    [SerializeField] private GameObject _life;
    [SerializeField] private GameObject _dead;
    public GameObject VisibleDamage;

    // отображение радара для поиска
    [SerializeField] protected Transform _visiblePosition;
    [SerializeField] protected float _visibleRange;

    [SerializeField] protected float _speed;
    [SerializeField] protected float _stopedDistance;

    public bool IsDie;
    [SerializeField] protected LayerMask _layerMask;

    // цель для движения
    [SerializeField] protected Collider2D[] _target;
    [SerializeField] protected Transform _targetForMove;

    protected Collider2D _enemyCollider;

    // хешируем трансформ игрока
    protected Transform _playerTR;

    // массив точек для пути и текущая точка
    protected Transform[] _childPath;
    protected int _chieldPathCount;
    protected int _currentPath;

    protected float DamageGive;
    protected float MaxHealth;

    // вычисление дистанции
    protected float _distance;

    // поиск врага, атака врага, является ли враг дальнобойным
    [SerializeField] protected bool _isAtack;
    [SerializeField] protected bool _isFind = true;
    [SerializeField] protected bool _isLongAttack;

    protected Rigidbody2D _rb;
    public int MoneyForKrip;

    // подумай нужен ли isDie т.к можно проверять на наличие хп, так работает без атказно
    private void FixedUpdate()
    {
        _life.SetActive(IsDie == false);
        _dead.SetActive(IsDie == true);

        if(Health < 0) _enemyCollider.enabled = false;

        if (IsDie == true)
        {
            _isAtack = false;
            _isFind = false;
            Destroy(gameObject, 1);
            return;
        }

        _target = Physics2D.OverlapCircleAll(_visiblePosition.position, _visibleRange, _layerMask);
        if (_isFind == true) FindeTarget();

        Move();
    }

    public virtual void Move()
    {
        if (_targetForMove != null)
        {
            _distance = Vector2.Distance(_targetForMove.position, _playerTR.position);
            _playerTR.up = _targetForMove.position - _playerTR.position;

            if (_distance < _stopedDistance && _isAtack == false) StartCoroutine(GiveDamage());

            if (_distance > _stopedDistance)
                _playerTR.position = Vector3.MoveTowards(_playerTR.position, _targetForMove.position, _speed);
        }
        else
        {
            var target = _childPath[_chieldPathCount];
            _playerTR.position = Vector3.MoveTowards(_playerTR.position, target.position, _speed);
            _playerTR.up = target.position - _playerTR.position;

            if (_playerTR.position == target.position)
            {
                _chieldPathCount++;
                if (_chieldPathCount >= _path[_currentPath].transform.childCount) Destroy(gameObject);
            }
        }
    }

    public virtual void FindeTarget()
    {
        foreach (Collider2D item in _target)
        {
            if (item.name == "TowerLight" ||
            item.name == "TronLight" ||
            item.GetComponent<LightKrip>() ||
            item.GetComponent<LightLongKrip>() ||
            item.name == "Player")
            {
                _targetForMove = item.transform;
                _isFind = false;
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
                _isFind = true;
                _isAtack = false;
                yield break;
            }
            else GiveDamageKrip();
        }
    }

    public virtual void GiveDamageKrip()
    {
        if (_targetForMove.GetComponent<LightKrip>())
        {
            if (_targetForMove.GetComponent<LightKrip>().Health > 0)
                _targetForMove.GetComponent<LightKrip>().TakeDamage(DamageGive);

            else
            {
                _targetForMove.GetComponent<LightKrip>().IsDie = true;
                _targetForMove = null;
                return;
            }
        }

        if (_targetForMove.GetComponent<LightLongKrip>())
        {
            if (_targetForMove.GetComponent<LightLongKrip>().Health > 0)
                _targetForMove.GetComponent<LightLongKrip>().TakeDamage(DamageGive);

            else
            {
                _targetForMove.GetComponent<LightLongKrip>().IsDie = true;
                _targetForMove = null;
                return;
            }
        }

        if (_targetForMove.GetComponent<TowerLight>())
        {
            if (_targetForMove.GetComponent<TowerLight>().Health > 0)
                _targetForMove.GetComponent<TowerLight>().TakeDamage(DamageGive);

            else
            {
                _targetForMove.GetComponent<TowerLight>().IsDie = true;
                _targetForMove = null;
                return;
            }
        }

        if (_targetForMove.GetComponent<Trone>())
        {
            if (_targetForMove.GetComponent<Trone>().Health > 0)
                _targetForMove.GetComponent<Trone>().TakeDamage(DamageGive);

            else
            {
                _targetForMove.GetComponent<Trone>().IsDie = true;
                _targetForMove = null;
                return;
            }
        }

    }

    public virtual void TakeDamage(float damage)
    {
        if(Health > 0)
        {
            Health -= damage;
            _imgHealth.fillAmount = Health / MaxHealth;
        }
    }

    public void TakeGold()
    {
        _moneyAnim.MoneyText.text = MoneyForKrip.ToString();
        GetMoney.instance.WorldMoney += MoneyForKrip;
        GetMoney.instance.GetTextUI();
        _moneyAnim.IsStartAnim = true;
    }

    protected void StartComponent()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerTR = GetComponent<Transform>();
        _enemyCollider = GetComponent<Collider2D>();
    }

    protected void StartPath()
    {
        _path = GameObject.FindGameObjectsWithTag("PathBottom");
        _currentPath = Random.Range(0, _path.Length);

        _childPath = new Transform[_path[_currentPath].transform.childCount];

        for (int i = 0; i < _path[_currentPath].transform.childCount; i++) _childPath[i] = _path[_currentPath].transform.GetChild(i);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_visiblePosition.position, _visibleRange);
    }
}