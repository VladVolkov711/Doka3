using System.Collections;
using UnityEngine;

public class WInEvent : MonoBehaviour
{
    public static WInEvent instance;


    [SerializeField] private GameObject _znack;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _winButton;

    [SerializeField] private GameObject _camera;
    [SerializeField] private GameObject _tron;

    private GameObject _player;
    private PlayerController _pc;
    private bool _isWin;
    private float _distance;
    private float _speed;

    private void Awake() => instance = this;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _pc = _player.GetComponent<PlayerController>();
    }

    private void FixedUpdate()
    {
        if (_isWin == false) return;
        _distance = Vector2.Distance(_tron.transform.position, _camera.transform.position);

        _camera.transform.position = Vector3.MoveTowards(_camera.transform.position, _tron.transform.position, _speed);
    }

    public IEnumerator StartWinEvent()
    {
        _isWin = true;
        _pc.enabled = false; 

        if (_distance < 5) _speed = 0.05f;
        if (_distance > 5 && _distance < 15) _speed = 0.6f;
        if (_distance > 15 && _distance < 70) _speed = 0.8f;

        yield return new WaitForSeconds(1.5f);
        _znack.SetActive(true);
        yield return new WaitForSeconds(2);
        _winPanel.SetActive(true);
        yield return new WaitForSeconds(1);
        _winButton.SetActive(true);
    }
}
