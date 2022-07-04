using System.Collections;
using UnityEngine;

public class FailEvent : MonoBehaviour
{
    public static FailEvent instance;

    [SerializeField] private GameObject _failPanel;
    [SerializeField] private GameObject _failButton;

    private void Awake() => instance = this;


    public IEnumerator StartFailEvent()
    {
        yield return new WaitForSeconds(2);
        _failPanel.SetActive(true);
        yield return new WaitForSeconds(1);
        _failButton.SetActive(true);
    }
}
