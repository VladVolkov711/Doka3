using System.Collections;
using UnityEngine;

public class TowerLight : Tower
{
    public override void FindeTarget()
    {
        foreach (Collider2D item in _target)
        {
            if (item.GetComponent<DarkKrip>() ||
                item.GetComponent<DarkLongkrip>())
            {
                _targetForDamage = item.transform;
                StartCoroutine(SpawnStone());
                _isFinde = false;
            }
        }
    }

    public override void GiveDamage()
    {
        // наносим урон простому крипу
        if (_targetForDamage.GetComponent<DarkKrip>())
        {
            if (_targetForDamage.GetComponent<DarkKrip>().Health > 0)
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
                _targetForDamage.GetComponent<DarkKrip>().IsDie = true;
                _targetForDamage = null;
                return;
            }
        }

        // наносим урон стреляющему крипу
        if (_targetForDamage.GetComponent<DarkLongkrip>())
        {
            if (_targetForDamage.GetComponent<DarkLongkrip>().Health > 0)
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
                _targetForDamage.GetComponent<DarkLongkrip>().IsDie = true;
                _targetForDamage = null;
                return;
            }
        }
    }
}
