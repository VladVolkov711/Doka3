using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Damageble _damageble;

    // маска для оптимизации поиска цели
    [SerializeField] private LayerMask _layerMask;

    // точка сферы для поиска и ее радиус
    [SerializeField] private Transform _visiblePosition;
    [SerializeField] private float _radius;

    [SerializeField] private ButtonReload _buttonReload;

    [SerializeField] private float _stamine;

    // массив с найдеными целями и записаная цель
    private Collider2D[] _buffer = new Collider2D[10];
    private Transform _targetforDamage;
    private int _bufferIndex = 0;

    // компоненты объекта для оптимизации
    private VisibleDamage _visibleDamage;
    private Health _health;

    private float _distance;

    private void Start()
    {
        //_stamine = _buttonReload.MaxTimer;
        _playerController.Anim.SetFloat("Attack", _stamine);
    }

    private void FixedUpdate()
    {
        _bufferIndex = Physics2D.OverlapCircleNonAlloc(_visiblePosition.position, _radius, _buffer, _layerMask);

        if(_bufferIndex > 0 && _targetforDamage == null) FindeTarget();
        if (_bufferIndex == 0) _targetforDamage = null;

        if(_targetforDamage != null)
        {
            _distance = Vector2.Distance(_targetforDamage.transform.position, transform.position);

            if (_distance > 3)
            {
                _visibleDamage.VisDamage.SetActive(false);
                _targetforDamage = null;
            }

            else _visibleDamage.VisDamage.SetActive(true);
        }

    }

    private void FindeTarget()
    {
        for (int i = 0; i < _bufferIndex; i++)
        {
            if (_buffer[i].GetComponent<Health>())
            {
                _targetforDamage = _buffer[i].transform;
                TackeComponent();
            }
        }
    }

    public void Attack()
    {
        if (_targetforDamage == null) return;

        _playerController.Anim.Play("Attack");
        StartCoroutine(_buttonReload.ReloadButton());
        _damageble.TakeDamage(_targetforDamage);

        if (_targetforDamage.GetComponent<Enemy>()) 
            _targetforDamage.GetComponent<Enemy>()._moneyAnim._isAttackPlayer = true;

        if (_health.IsDie == true) // проверить бъет ли игрок башню !!! ВАЖНО !!!
        {
            _targetforDamage = null;
            return;
        }
    }

    private void TackeComponent()
    {
        _visibleDamage = _targetforDamage.GetComponent<VisibleDamage>();
        _health = _targetforDamage.GetComponent<Health>();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_visiblePosition.position, _radius);
    }
}
