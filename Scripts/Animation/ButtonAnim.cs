using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAnim : MonoBehaviour
{
    [SerializeField] private Image _image;
    private Color _color;

    private void Start()
    {
        _image = GetComponent<Image>();
        _color = _image.material.color;
        _color.a = 0;
        _image.color = _color;
        StartCoroutine(Visible());
    }

    private IEnumerator Visible()
    {
        for (float i = 0.05f; i <= 1; i += 0.05f)
        {
            _color = _image.material.color;
            _color.a = i;
            _image.color = _color;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
