using System.Collections;
using UnityEngine;

public class PanelAnim : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private float _timeOpen;
    [SerializeField] private float _scalePanel;
    private void Start()
    {
        StartCoroutine(PanelOpen());
    }

    private IEnumerator PanelOpen()
    {
        while (_panel.transform.localScale.x < 1)
        {
            yield return new WaitForSeconds(_timeOpen);
            _panel.transform.localScale = new Vector3(_panel.transform.localScale.x + _scalePanel, 
                                                        _panel.transform.localScale.y + _scalePanel,
                                                        _panel.transform.localScale.z);
            
        }
    }
}
