using UnityEngine;

public class SearchPath : MonoBehaviour
{
    [HideInInspector] public GameObject[] _path;
    [HideInInspector] public Transform[] _childPath;
    [HideInInspector] public int _chieldPathCount;
    [HideInInspector] public int _currentPath;

    [SerializeField] private string _pathName;

    private void Start() => StartPath();

    protected void StartPath()
    {
        _path = GameObject.FindGameObjectsWithTag(_pathName);
        _currentPath = Random.Range(0, _path.Length);

        _childPath = new Transform[_path[_currentPath].transform.childCount];

        for (int i = 0; i < _path[_currentPath].transform.childCount; i++) _childPath[i] = _path[_currentPath].transform.GetChild(i);
    }
}
