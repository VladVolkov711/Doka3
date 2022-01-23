using UnityEngine;
using TMPro;

public class GetMoney : MonoBehaviour
{
    public static GetMoney instance;
    public TextMeshProUGUI MoneyText;
    public float WorldMoney;

    private void Awake() => instance = this;

    public void GetTextUI()
    {
        MoneyText.text = WorldMoney.ToString();
    }
}
