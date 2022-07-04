using UnityEngine;

public class Damageble : MonoBehaviour
{
    public float Damage;

    [SerializeField] private GameObject _bullet;

    [SerializeField] private bool _isLongDamage;
    public void TakeDamage(Transform obj)
    {
        if (_isLongDamage == true)
        {
            GameObject NewBulled = Instantiate(_bullet, transform.position, Quaternion.identity);
            NewBulled.GetComponent<Bulled>()._target = obj;
        }
        else obj.GetComponent<Health>().CheckHealth(Damage);
    }
}
