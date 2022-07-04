using System.Collections;
using UnityEngine;
using TMPro;

public class MoneyAnim : MonoBehaviour
{
    public TextMeshProUGUI MoneyText;

    [SerializeField] private Health _health; // проверка на наличие хп
    [SerializeField] private MoneyForEnemy _moneyForEnemy;
    [SerializeField] private GameObject _money;

    [SerializeField] private float _speed;

    [SerializeField] private float _time;
    private bool _isStartAnim = true;
    internal bool _isAttackPlayer;

    private void Start() => _speed = 0.01f;


    private void FixedUpdate()
    {
        if (_health._health <= 0 && _isStartAnim == true && _isAttackPlayer == true)
        {
            _isStartAnim = false;
            _isAttackPlayer = false;
            _money.SetActive(true);
            StartCoroutine(InvisibleText());
        }
    }

    private IEnumerator InvisibleText()
    {
        _time = 5;

        MoneyText.text = _moneyForEnemy.GiveMoneyForPlayer.ToString();
        GetMoney.instance.WorldMoney += _moneyForEnemy.GiveMoneyForPlayer;
        GetMoney.instance.GetTextUI();

        while (_time > 0)
        {
            yield return new WaitForSeconds(0.01f);
            _time -= 0.1f;
            _money.transform.Translate(Vector2.up * _speed);
        }

        _money.SetActive(false);
    }
}
