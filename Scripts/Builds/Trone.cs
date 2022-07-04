using UnityEngine;

public class Trone : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private bool _islightTrone;

    private void Update()
    {
        if (_health._health > 0) return;

        if(_islightTrone == false)
            StartCoroutine(WInEvent.instance.StartWinEvent());
        else
            StartCoroutine(FailEvent.instance.StartFailEvent());
    }
}
