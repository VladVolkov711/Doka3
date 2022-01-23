using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpawnerEnemy : MonoBehaviour
{
    [SerializeField] private Text _timeText;
    [SerializeField] private GameObject _krip;
    [SerializeField] private GameObject _longKrip;
    [SerializeField] private Transform[] SpawnPoints;

    private float _timeSpawn;
    private float _maxTimeSpawn;

    private bool _isSpawn = true;

    private void Start()
    {
        _maxTimeSpawn = 1;
        _timeSpawn = _maxTimeSpawn;

        _timeText.text = Mathf.Floor(_timeSpawn).ToString();

        StartCoroutine(Spawner());
    }

    private void Update()
    {
        if (_timeSpawn >= 0)
        {
            _timeSpawn -= Time.deltaTime;
            _timeText.text = Mathf.Floor(_timeSpawn).ToString();
        }
        else _timeSpawn = _maxTimeSpawn;

        if (_isSpawn == true)
            StartCoroutine(Spawner());
    }

    private IEnumerator Spawner()
    {
        _isSpawn = false;
        yield return new WaitForSeconds(_maxTimeSpawn);

        _maxTimeSpawn = 20;
        GameObject newLongKrip = Instantiate(_longKrip, SpawnPoints[Random.Range(0, SpawnPoints.Length)].position, Quaternion.identity);

        for(int i = 0; i < SpawnPoints.Length; i++)
        {
            if (i == 3)
            {
                _isSpawn = true;
                yield break;
            }
                GameObject newKrip = Instantiate(_krip, SpawnPoints[Random.Range(0, SpawnPoints.Length)].position, Quaternion.identity);
        }
    }
}