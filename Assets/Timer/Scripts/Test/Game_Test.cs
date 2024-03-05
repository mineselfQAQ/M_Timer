using NRFramework;
using UnityEngine;

public class Game_Test : Singleton<Game_Test>
{
    //数字越小，优先级越低(被压在底下)
    public UIRoot root1 { get; private set; }
    public UIRoot root2 { get; private set; }
    public UIRoot root3 { get; private set; }
    public UIRoot root4 { get; private set; }

    private Game_Test()
    {
        root1 = UIManager.Instance.CreateRoot("root1", 0, 99);
        root2 = UIManager.Instance.CreateRoot("root2", 100, 199);
        root3 = UIManager.Instance.CreateRoot("root3", 200, 299);
        root4 = UIManager.Instance.CreateRoot("root4", 300, 399);
    }

    public void StartGame()
    {
        TestPanel testPanel = root1.CreatePanel<TestPanel>("Assets/Timer/Prefabs/UI/TestPanel.prefab");
        testPanel.Init();
    }
}
