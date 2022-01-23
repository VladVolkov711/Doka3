using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class joystick : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler
{
    private Image _back;
    private Image _joystickImg;
    private Vector3 _inputVector;

    private void Start()
    {
        _back = GetComponent<Image>();
        _joystickImg = transform.GetChild(0).GetComponent<Image>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_back.rectTransform, eventData.position, eventData.pressEventCamera, out position))
        {
            position.x = (position.x / _back.rectTransform.sizeDelta.x);
            position.y = (position.y / _back.rectTransform.sizeDelta.y);

            _inputVector = new Vector3(position.x, 0, position.y);
            _inputVector = (_inputVector.magnitude > 1) ? _inputVector.normalized : _inputVector;

            _joystickImg.rectTransform.anchoredPosition = new Vector3(_inputVector.x * (_back.rectTransform.sizeDelta.x / 3),
                                                                        _inputVector.z * (_back.rectTransform.sizeDelta.y / 3));
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _inputVector = Vector3.zero;
        _joystickImg.rectTransform.anchoredPosition = Vector3.zero;
    }

    public float Horozontal()
    {
        if (_inputVector.x != 0) return _inputVector.x;
        else return Input.GetAxis("Horizontal");
    }
    public float Vertical()
    {
        if (_inputVector.z != 0) return _inputVector.z;
        else return Input.GetAxis("Vertical");
    }
}
