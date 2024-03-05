using NRFramework;

public class Game_Timer : Singleton<Game_Timer>
{
    //数字越小，优先级越低(被压在底下)
    public UIRoot root1 { get; private set; }
    public UIRoot root2 { get; private set; }
    public UIRoot root3 { get; private set; }
    public UIRoot root4 { get; private set; }

    private MainPanel m_MainPanel;

    private Game_Timer()
    {
        root1 = UIManager.Instance.CreateRoot("root1", 0, 99);
        root2 = UIManager.Instance.CreateRoot("root2", 100, 199);
        root3 = UIManager.Instance.CreateRoot("root3", 200, 299);
        root4 = UIManager.Instance.CreateRoot("root4", 300, 399);
    }
    public void Start()
    {
        m_MainPanel = root1.CreatePanel<MainPanel>("Assets/Timer/Prefabs/UI/MainPanel.prefab");
        m_MainPanel.Init();
    }

    public void Update()
    {
        m_MainPanel.Update();
    }

    public void OnApplicationQuit()
    {
        m_MainPanel.OnApplicationQuit();
    }
}
