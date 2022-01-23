using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    public bool IsDie;

    [SerializeField] protected LayerMask _layerMask;

    // здоровье
    public float Health;
    [SerializeField] protected Image ImgHealth;

    // снар€д
    [SerializeField] protected GameObject _stone;
    protected GameObject _stoneCheck;

    // поиск цели
    [SerializeField] protected Collider2D[] _target;
    [SerializeField] protected Transform _visiblePosition;

    // радиус видимости
    [SerializeField] protected float _visibleRange;

    // объект дл€ нападени€
    [SerializeField] protected Transform _targetForDamage;

    // поиск цели
    [SerializeField] protected bool _isFinde = true;

    // максемальное здоровье
    private float MaxHealth;

    // коллайдер объекта
    private Collider2D _collider;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();

        if (!PlayerPrefs.HasKey("Health")) MaxHealth = 300;
        Health = MaxHealth;

        _isFinde = true;
    }

    private void FixedUpdate()
    {
        if(IsDie == true)
        {
            _collider.enabled = false;
            _isFinde = false;
            Destroy(gameObject, 2);
            return;
        }
        _target = Physics2D.OverlapCircleAll(_visiblePosition.position, _visibleRange, _layerMask);
        if(_isFinde == true) FindeTarget();
    }

    public virtual void FindeTarget()
    {
        foreach (Collider2D item in _target)
        {
            if (item.GetComponent<LightKrip>() ||
                item.GetComponent<LightLongKrip>() ||
                item.name == "Player")
            {
                _targetForDamage = item.transform;
                StartCoroutine(SpawnStone());
                _isFinde = false;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        if (Health >= 10)
        {
            Health -= damage;
            ImgHealth.fillAmount = Health / MaxHealth;
        }
        else
        {
            IsDie = true;
            _isFinde = false;
        }
    }

    public IEnumerator SpawnStone()
    {
        while(true)
        {

            yield return new WaitForSeconds(0.5f);

            if(_targetForDamage == null || IsDie == true)
            {
                _isFinde = true;
                yield break;
            }
            else GiveDamage();

        }
    }

    public virtual void GiveDamage()
    {
        // наносим урон простому крипу
        if (_targetForDamage.GetComponent<LightKrip>())
        {
            if (_targetForDamage.GetComponent<LightKrip>().Health > 0)
            {
                if (_stoneCheck == null)
                {
                    GameObject newStone = Instantiate(_stone, transform.position, Quaternion.identity);
                    _stoneCheck = newStone;
                    newStone.GetComponent<Bulled>()._target = _targetForDamage;
                }
            }

            else
            {
                _targetForDamage.GetComponent<LightKrip>().IsDie = true;
                _targetForDamage = null;
                return;
            }
        }

        // наносим урон стрел€ющему крипу
        if (_targetForDamage.GetComponent<LightLongKrip>())
        {
            if (_targetForDamage.GetComponent<LightLongKrip>().Health > 0)
            {
                if (_stoneCheck == null)
                {
                    GameObject newStone = Instantiate(_stone, transform.position, Quaternion.identity);
                    _stoneCheck = newStone;
                    newStone.GetComponent<Bulled>()._target = _targetForDamage;
                }
            }

            else
            {
                _targetForDamage.GetComponent<LightLongKrip>().IsDie = true;
                _targetForDamage = null;
                return;
            }
        }
        // наносим урон игрогу
        if (_targetForDamage.GetComponent<PlayerController>())
        {
            if (_targetForDamage.GetComponent<PlayerController>().Health > 0)
            {
                if (_stoneCheck == null)
                {
                    GameObject newStone = Instantiate(_stone, transform.position, Quaternion.identity);
                    _stoneCheck = newStone;
                    newStone.GetComponent<Bulled>()._target = _targetForDamage;
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_visiblePosition.position, _visibleRange);
    }
}
