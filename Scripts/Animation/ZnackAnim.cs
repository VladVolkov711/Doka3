using System.Collections;
using UnityEngine;

public class ZnackAnim : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _forseSpeed;
    [SerializeField] private float _speed;
    [SerializeField] private float _scalePanel;
    private Rigidbody2D _rigidbody;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.AddForce(Vector2.left * _forseSpeed, ForceMode2D.Impulse);
        StartCoroutine(Size());
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed);
    }

    private IEnumerator Size()
    {
        while (gameObject.transform.localScale.x < 2)
        {
            yield return new WaitForSeconds(0.05f);
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x + _scalePanel,
                                                        gameObject.transform.localScale.y + _scalePanel,
                                                        gameObject.transform.localScale.z);

        }
    }
}
