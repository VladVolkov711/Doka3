using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenue : MonoBehaviour
{
    [SerializeField] private GameObject UpgrateMenue;

    public void UpgrateMenueVisible() => UpgrateMenue.SetActive(true);
    public void UpgrateMenueInVisible() => UpgrateMenue.SetActive(false);
}
