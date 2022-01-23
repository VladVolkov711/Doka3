using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonReload : MonoBehaviour
{
    public float _maxTimer;

    [SerializeField] private Button _buttonTake;
    [SerializeField] private Image _imageReload;
    [SerializeField] private float _timeCourotine;

    public float _timer;

    public IEnumerator ReloadButton()
    {
        _buttonTake.enabled = false;
        _timer = _maxTimer;
        while (true)
        {
            yield return new WaitForSeconds(_timeCourotine);
            if(_timer > 0)
            {
                _timer -= _timeCourotine;
                _imageReload.fillAmount = _timer / _maxTimer;
            }
            else
            {
                _buttonTake.enabled = true;
                yield break;
            }
        }
    }
}
