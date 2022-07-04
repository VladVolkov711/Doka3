using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthFillAmount : MonoBehaviour
{
    [SerializeField] private Image _red;
    [SerializeField] private Image _orange;
    [SerializeField] private Health _health;

    public void HealthDown()
    {
        _red.fillAmount = _health._health / _health._maxHealth;
        StartCoroutine(HealthDownOrange());
    }

    private IEnumerator HealthDownOrange()
    {
        while (_orange.fillAmount >= _red.fillAmount)
        {
            yield return new WaitForSeconds(0.03f);
            _orange.fillAmount -= 0.01f;
        }
    }
}
