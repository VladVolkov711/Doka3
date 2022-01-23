using UnityEngine;

public class WinAndFail : MonoBehaviour
{
    public static WinAndFail Instance;
    public GameObject PannelWin;
    public GameObject PannelFail;

    private void Awake() => Instance = this;
}