using UnityEngine;

[CreateAssetMenu]
public class Upgrate : ScriptableObject
{
    public void UpgratePlayer(ref float firstUp, ref float lastUp, string znack)
    {
        if(znack == "+") firstUp += lastUp;
        if(znack == "-") firstUp -= lastUp;
    }
}