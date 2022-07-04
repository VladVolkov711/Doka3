using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonReload : MonoBehaviour
{
    public float MaxTimer;
    public float Timer;

    [SerializeField] private Button _buttonTake;
    [SerializeField] private Image _imageReload;
    [SerializeField] private float _timeCourotine;

    public IEnumerator ReloadButton()
    {
        _buttonTake.enabled = false;
        Timer = MaxTimer;
        while (true)
        {
            yield return new WaitForSeconds(_timeCourotine);
            if(Timer > 0)
            {
                Timer -= _timeCourotine;
                _imageReload.fillAmount = Timer / MaxTimer;
            }
            else
            {
                _buttonTake.enabled = true;
                yield break;
            }
        }
    }
}
