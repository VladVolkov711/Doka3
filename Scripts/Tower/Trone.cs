using UnityEngine;
using UnityEngine.UI;

public class Trone : MonoBehaviour
{
    [HideInInspector] public bool IsDie;
    public Image ImgHealth;
    public float Health;
    public float MaxHealth;

    [SerializeField] private bool _islightTrone;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("Health")) MaxHealth = 500;
        Health = MaxHealth;
    }

    private void Update()
    {
        if(IsDie == true)
        {
            if (_islightTrone == true)
            {
                WinAndFail.Instance.PannelFail.SetActive(true);
                Destroy(gameObject);
                return;
            }
            else
            {
                WinAndFail.Instance.PannelWin.SetActive(true);
                Destroy(gameObject);
                return;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        if (Health > 0)
        {
            Health -= damage;
            ImgHealth.fillAmount = Health / MaxHealth;
        }
        else IsDie = true;
    }
}
