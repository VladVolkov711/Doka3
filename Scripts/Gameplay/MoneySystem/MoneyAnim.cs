using System.Collections;
using UnityEngine;
using TMPro;

public class MoneyAnim : MonoBehaviour
{
    public TextMeshProUGUI MoneyText;
    [SerializeField] private GameObject _money;
    [SerializeField] private float _speed;
    [HideInInspector] public bool IsStartAnim;

    private void Start()
    {
        _speed = 0.01f;
    }


    private void FixedUpdate()
    {
        if (IsStartAnim == true)
        {
            _money.SetActive(true);
            StartCoroutine(InvisibleText());
            _money.transform.Translate(Vector2.up * _speed);
        }
    }

    private IEnumerator InvisibleText()
    {
        yield return new WaitForSeconds(0.5f);
        IsStartAnim = false;
        _money.SetActive(false);
    }
}
