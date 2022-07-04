using System.Threading.Tasks;
using UnityEngine;
using TMPro;

public class GetMoney : MonoBehaviour
{
    public static GetMoney instance;

    public TextMeshProUGUI MoneyText;
    public float WorldMoney;

    private void Awake() => instance = this;

    private void Start() => GetTextUI();

    public async void GetTextUI()
    {
        while (true)
        {
            MoneyText.text = WorldMoney.ToString();
            await Task.Delay(10);
        }
    }
}
