using UnityEngine;

public class DarkKrip : Enemy
{
    private void Start()
    {
        StartComponent();
        StartPath();

        MaxHealth = 100;
        Health = MaxHealth;
        _imgHealth.fillAmount = 1;

        DamageGive = 10;
    }
}