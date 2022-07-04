using System.Threading.Tasks;
using UnityEngine;

public class UpGratePlayer : MonoBehaviour
{
    [SerializeField] private GameObject _buttonUpPlayerDamage;
    [SerializeField] private GameObject _buttonUpPlayerSpeedDamage;
    [SerializeField] private GameObject _buttonUpPlayerSpeed;

    [SerializeField] private float _pricePlayerDamage;
    [SerializeField] private float _pricePlayerSpeedDamage;
    [SerializeField] private float _pricePlayerSpeed;

    private GameObject _player;

    private Damageble _playerDamage; // улучшение атаки
    private ButtonReload _buttonReloadSpeed; // улучшение скорости игрока
    private PlayerController _playerController; // улучшение скорости игрока

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");

        _playerDamage = _player.GetComponent<Damageble>();
        _buttonReloadSpeed = _player.GetComponent<ButtonReload>();
        _playerController = _player.GetComponent<PlayerController>();
    }

    private void Start() => UpdateButtonState();

    private async void UpdateButtonState()
    {
        while (true)
        {
            _buttonUpPlayerDamage.SetActive(GetMoney.instance.WorldMoney >= _pricePlayerDamage);
            _buttonUpPlayerSpeedDamage.SetActive(GetMoney.instance.WorldMoney >= _pricePlayerSpeedDamage &&                                                                             _buttonReloadSpeed.MaxTimer >= 0.2f);
            _buttonUpPlayerSpeed.SetActive(GetMoney.instance.WorldMoney >= _pricePlayerSpeed && 
                _playerController.Speed <= 5);

            await Task.Delay(10);
        }
    }

    public void PlayerDamageUp()
    {
        _playerDamage.Damage += _playerDamage.Damage * 0.3f;
        GetMoney.instance.WorldMoney -= _pricePlayerDamage;
        _pricePlayerDamage += _pricePlayerDamage * 0.3f;
    }
    public void PlayerSpeedAtack()
    {
        _buttonReloadSpeed.MaxTimer -= 0.1f;
        GetMoney.instance.WorldMoney -= _pricePlayerSpeedDamage;
        _pricePlayerSpeedDamage += _pricePlayerSpeedDamage * 0.3f;
    }

    public void PlayerSpeedUp()
    {
        _playerController.Speed += 0.5f;
        GetMoney.instance.WorldMoney -= _pricePlayerSpeed;
        _pricePlayerSpeed += _pricePlayerSpeed * 0.3f;
    }
}
