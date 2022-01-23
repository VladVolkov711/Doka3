using UnityEngine;

public class LightLongKrip : Enemy
{
    [SerializeField] protected GameObject _bullet;
    private void Start()
    {
        StartComponent();
        StartPath();

        _stopedDistance = 4;

        MaxHealth = 100;
        Health = MaxHealth;
        _imgHealth.fillAmount = 1;

        DamageGive = 15;

    }

    public override void FindeTarget()
    {
        foreach (Collider2D item in _target)
        {
            if (item.name == "TowerDark" ||
            item.name == "TronDark" ||
            item.GetComponent<DarkKrip>() ||
            item.GetComponent<DarkLongkrip>())
            {
                _targetForMove = item.transform;
                _isFind = false;
            }
        }
    }

    public override void GiveDamageKrip()
    {
        if (_targetForMove.GetComponent<TowerDark>())
        {
            if (_targetForMove.GetComponent<TowerDark>().Health > 0)
            {
                GameObject NewBulled = Instantiate(_bullet, transform.position, Quaternion.identity);
                NewBulled.GetComponent<Bulled>()._target = _targetForMove;
            }

            else
            {
                _targetForMove.GetComponent<TowerDark>().IsDie = true;
                _targetForMove = null;
                return;
            }
        }

        if (_targetForMove.GetComponent<Trone>())
        {
            if (_targetForMove.GetComponent<Trone>().Health > 0)
            {
                GameObject NewBulled = Instantiate(_bullet, transform.position, Quaternion.identity);
                NewBulled.GetComponent<Bulled>()._target = _targetForMove;
            }

            else
            {
                _targetForMove.GetComponent<Trone>().IsDie = true;
                _targetForMove = null;
                return;
            }
        }

        if (_targetForMove.GetComponent<DarkKrip>())
        {
            if (_targetForMove.GetComponent<DarkKrip>().Health > 0)
            {
                GameObject NewBulled = Instantiate(_bullet, transform.position, Quaternion.identity);
                NewBulled.GetComponent<Bulled>()._target = _targetForMove;
            }

            else
            {
                _targetForMove.GetComponent<DarkKrip>().IsDie = true;
                _targetForMove = null;
                return;
            }
        }

        if (_targetForMove.GetComponent<DarkLongkrip>())
        {
            if (_targetForMove.GetComponent<DarkLongkrip>().Health > 0)
            {
                GameObject NewBulled = Instantiate(_bullet, transform.position, Quaternion.identity);
                NewBulled.GetComponent<Bulled>()._target = _targetForMove;
            }

            else
            {
                _targetForMove.GetComponent<DarkLongkrip>().IsDie = true;
                _targetForMove = null;
                return;
            }
        }
    }
}
