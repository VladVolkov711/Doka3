using UnityEngine;

public class DarkLongkrip : Enemy
{
    [SerializeField] private GameObject _Bulled;
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

    public override void GiveDamageKrip()
    {
        if (_targetForMove.GetComponent<TowerLight>())
        {
            if (_targetForMove.GetComponent<TowerLight>().Health > 0)
            {
                GameObject NewBulled = Instantiate(_Bulled, transform.position, Quaternion.identity);
                NewBulled.GetComponent<Bulled>()._target = _targetForMove;
            }

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
            {
                GameObject NewBulled = Instantiate(_Bulled, transform.position, Quaternion.identity);
                NewBulled.GetComponent<Bulled>()._target = _targetForMove;
            }

            else
            {
                _targetForMove.GetComponent<Trone>().IsDie = true;
                _targetForMove = null;
                return;
            }
        }

        if (_targetForMove.GetComponent<LightKrip>())
        {
            if (_targetForMove.GetComponent<LightKrip>().Health > 0)
            {
                GameObject NewBulled = Instantiate(_Bulled, transform.position, Quaternion.identity);
                NewBulled.GetComponent<Bulled>()._target = _targetForMove;
            }

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
            {
                GameObject NewBulled = Instantiate(_Bulled, transform.position, Quaternion.identity);
                NewBulled.GetComponent<Bulled>()._target = _targetForMove;
            }

            else
            {
                _targetForMove.GetComponent<LightLongKrip>().IsDie = true;
                _targetForMove = null;
                return;
            }
        }
    }
}
