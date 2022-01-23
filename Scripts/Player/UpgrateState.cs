using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgrateState : MonoBehaviour
{
    // данные дл€ обновлени€
    [SerializeField] private Upgrate _damageUpgrate;
    [SerializeField] private Upgrate _speedAttackUpgrate;

    // кнопки дл€ обновлени€ 
    [SerializeField] private Button _damageUpgrateButton;
    [SerializeField] private Button _speedAttackUpgrateButton;

    [SerializeField] private Text _damageTextPrice;
    [SerializeField] private Text _speedAttackTextPrice;

    [SerializeField] private TextMeshProUGUI _damageTMPInfo;
    [SerializeField] private TextMeshProUGUI _speedAttackTMPInfo;

    // ссылки на компоненты дл€ апгрейта
    [SerializeField] private PlayerAttack _playerAttack;
    [SerializeField] private ButtonReload _buttonReload;

    // значени€ змен€ющие характеристики игрока
    [SerializeField] private float _damageUp;
    [SerializeField] private float _speedAttackUp;

    private float _damagePrice;
    private float _speedAttackPrice;

    private void Start()
    {
        _damagePrice = 30;
        _speedAttackPrice = 40;

        _damageTextPrice.text = _damagePrice.ToString() + " $";
        _speedAttackTextPrice.text = _speedAttackPrice.ToString() + " $";

        _damageTMPInfo.text = "Damage Value: " + _playerAttack._damage.ToString();
        _speedAttackTMPInfo.text = "Damage Value: " + _buttonReload._maxTimer.ToString();
    }

    public void DamageUpdate()
    {
        if (GetMoney.instance.WorldMoney > 0)
        {
            if (_playerAttack._damage < 50)
                if (GetMoney.instance.WorldMoney > 0) _damageUpgrate.UpgratePlayer(ref _playerAttack._damage, ref _damageUp, "+");
            else return;

            _damageTMPInfo.text = "Damage Value: " + _playerAttack._damage.ToString();

            GetMoney.instance.WorldMoney -= _damagePrice;
            _damagePrice += 10;
            _damageTextPrice.text = _damagePrice.ToString() + " $";
        }
    }

    public void SpeedAttackUpdate()
    {
        if (GetMoney.instance.WorldMoney > 0)
        {
            if(_buttonReload._maxTimer > 0.4f)
                _speedAttackUpgrate.UpgratePlayer(ref _buttonReload._maxTimer, ref _speedAttackUp, "-");
            else return;

            _speedAttackTMPInfo.text = "Damage Value: " + _buttonReload._maxTimer.ToString();

            GetMoney.instance.WorldMoney -= _speedAttackPrice;
            _speedAttackPrice += 10;
            _speedAttackTextPrice.text = _speedAttackPrice.ToString() + " $";
        }
    }
}
