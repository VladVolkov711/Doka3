using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float _damage;

    // маска для оптимизации поиска цели
    [SerializeField] private LayerMask _layerMask;

    // точка сферы для поиска и ее радиус
    [SerializeField] private Transform _visiblePosition;
    [SerializeField] private float _radius;

    // массив с найдеными целями и записаная цель
    [SerializeField] private Collider2D[] _targets;
    [SerializeField] private Collider2D _targetforDamage;

    [SerializeField] private ButtonReload _buttonReload;

    private bool _isFinde = true;
    private float _distance;

    private void FixedUpdate()
    {
        _targets = Physics2D.OverlapCircleAll(_visiblePosition.position, _radius, _layerMask);
        if (_isFinde == true) FindeTarget();
        if(_targetforDamage != null) _distance = Vector2.Distance(_targetforDamage.transform.position, transform.position);

        if (_distance > 2 && _isFinde == false)
        {
            if(_targetforDamage != null)
            {
                if (_targetforDamage.GetComponent<DarkKrip>())
                    _targetforDamage.GetComponent<DarkKrip>().VisibleDamage.SetActive(false);

                if (_targetforDamage.GetComponent<DarkLongkrip>())
                    _targetforDamage.GetComponent<DarkLongkrip>().VisibleDamage.SetActive(false);
            }

            _targetforDamage = null;
            _isFinde = true;
        }

        else
        {
            if(_targetforDamage != null)
            {
                if (_targetforDamage.GetComponent<DarkKrip>())
                    _targetforDamage.GetComponent<DarkKrip>().VisibleDamage.SetActive(true);

                if (_targetforDamage.GetComponent<DarkLongkrip>())
                    _targetforDamage.GetComponent<DarkLongkrip>().VisibleDamage.SetActive(true);
            }
        }
    }

    public void FindeTarget()
    {
        foreach (Collider2D target in _targets)
        {
            if (target.name == "TowerDark") _targetforDamage = target;

            if (target.name == "TronDark") _targetforDamage = target;

            if (target.GetComponent<DarkKrip>()) _targetforDamage = target;

            if (target.GetComponent<DarkLongkrip>()) _targetforDamage = target;

            if (_targetforDamage != null) _isFinde = false;
        }
    }

    public void Attack()
    {
        if(_targetforDamage != null)
        {
            if (_targetforDamage.GetComponent<DarkKrip>())
            {
                if (_targetforDamage.GetComponent<DarkKrip>().Health <= 0)
                {
                    _targetforDamage.GetComponent<DarkKrip>().VisibleDamage.SetActive(false);
                    _targetforDamage.GetComponent<DarkKrip>().TakeGold();
                    _targetforDamage.GetComponent<DarkKrip>().IsDie = true;
                    _targetforDamage = null;
                    _isFinde = true;
                    return;
                }
                else _targetforDamage.GetComponent<DarkKrip>().TakeDamage(_damage);
            }

            if (_targetforDamage.GetComponent<DarkLongkrip>())
            {
                if (_targetforDamage.GetComponent<DarkLongkrip>().Health <= 0)
                {
                    _targetforDamage.GetComponent<DarkLongkrip>().VisibleDamage.SetActive(false);
                    _targetforDamage.GetComponent<DarkLongkrip>().TakeGold();
                    _targetforDamage.GetComponent<DarkLongkrip>().IsDie = true;
                    _targetforDamage = null;
                    _isFinde = true;
                    return;
                }
                else _targetforDamage.GetComponent<DarkLongkrip>().TakeDamage(_damage);
            }

            if (_targetforDamage.GetComponent<TowerDark>())
            {
                if (_targetforDamage.GetComponent<TowerDark>().Health <= 0)
                {
                    _targetforDamage.GetComponent<TowerDark>().IsDie = true;
                    _targetforDamage = null;
                    _isFinde = true;
                    return;
                }
                else _targetforDamage.GetComponent<TowerDark>().TakeDamage(_damage);
            }

            if (_targetforDamage.name == "TronDark")
            {
                if (_targetforDamage.GetComponent<Trone>().Health <= 0)
                {
                    _targetforDamage.GetComponent<Trone>().IsDie = true;
                    _targetforDamage = null;
                    _isFinde = true;
                    return;
                }
                else _targetforDamage.GetComponent<Trone>().TakeDamage(_damage);
            }
        }
        StartCoroutine(_buttonReload.ReloadButton());
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_visiblePosition.position, _radius);
    }
}
