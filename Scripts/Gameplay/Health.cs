using UnityEngine;
using UnityEngine.UI;

public enum creature { enemy, player, trone }

public class Health : MonoBehaviour
{

    public creature _creature;   
    
    public bool IsDie;

    [SerializeField] private HealthFillAmount _healthFillAmount;
    
    // состояния врага
    [SerializeField] private GameObject _life;
    [SerializeField] private GameObject _dead;

    public float _health;
    public float _maxHealth;

    private void Start() => _health = _maxHealth;

    private void Update()
    {
        if (_health > 0) return;
        
        IsDie = true;
        _life.SetActive(false);
        _dead.SetActive(true);
    }

    public void CheckHealth(float damage)
    {
        if(_creature == creature.enemy)
        {
            if (_health <= 0)
            {
                IsDie = true;
                _life.SetActive(false);
                _dead.SetActive(true);

                gameObject.GetComponent<Collider2D>().enabled = false;
            }

            _health -= damage;
            _healthFillAmount.HealthDown();
        }
        else if(_creature == creature.player)
        {
            if (_health <= 0)
            {
                IsDie = true;
                _life.SetActive(false);
                _dead.SetActive(true);
            }

            _health -= damage;
            _healthFillAmount.HealthDown();
        }

        else if(_creature == creature.trone)
        {
            if (Tower.CountTower > 0) return;

            if (_health <= 0)
            {
                IsDie = true;
                _life.SetActive(false);
                _dead.SetActive(true);
            }

            _health -= damage;
            _healthFillAmount.HealthDown();
        }
        
    }
}
