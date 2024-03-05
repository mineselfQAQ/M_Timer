using UnityEngine;

public class Core_Timer : MonoBehaviour
{
    private void Start()
    {
        Game_Timer.Instance.Start();
    }

    private void Update()
    {
        Game_Timer.Instance.Update();
    }

    private void OnApplicationQuit()
    {
        Game_Timer.Instance.OnApplicationQuit();
    }
}
