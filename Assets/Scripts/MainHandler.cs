using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainHandler : MonoBehaviour
{
    //Main
    private Transform main;
    private TMP_Text timer;
    private Button btn_Start;
    private Button btn_Stop;
    private Button btn_End;

    //Bottom
    private TMP_Text totalTime;
    private Button btn_Left;
    private Button btn_Right;//用于记录每次计时的信息

    private void Start()
    {
        //=====模块初始化=====
        Timer.Instance.Init();
        Event.Instance.Init();
        JsonManager.Instance.Init();
        RecordUI.Init();
        Calendar.Init();
        CalendarUI.Init();

        //=====获取各控件分区需要更改的控件=====
        //Main
        main = GameObject.Find("Main").transform;
        foreach (Transform t in main)
        {
            if (t.name == "Timer")
                timer = t.GetComponent<TMP_Text>();
            else if (t.name == "Btn_Start")
                btn_Start = t.GetComponent<Button>();
            else if (t.name == "Btn_Stop")
                btn_Stop = t.GetComponent<Button>();
            else if (t.name == "Btn_End")
                btn_End = t.GetComponent<Button>();
        }
        btn_Start.onClick.AddListener(StartRecord);
        btn_Stop.onClick.AddListener(PauseRecord);
        btn_End.onClick.AddListener(StopRecord);

        //Bottom
        Transform bottom = GameObject.Find("Bottom").transform;
        foreach (Transform t in bottom)
        {
            if (t.name == "Summary")
                totalTime = t.GetComponent<TMP_Text>();
            else if (t.name == "Btn_LeftMenu")
                btn_Left = t.GetComponent<Button>();
            else if (t.name == "Btn_RightMenu")
                btn_Right = t.GetComponent<Button>();
        }
        btn_Left.onClick.AddListener(Event.Instance.OpenLeftMenu);
        btn_Right.onClick.AddListener(Event.Instance.OpenRightMenu);
    }

    private void Update()
    {
        //Timer类更新
        Timer.Instance.Update();
        Event.Instance.Update();
        timer.text = Timer.Instance.Time;
    }

    private void OnApplicationQuit()
    {
        JsonManager.Instance.ToJson(JsonManager.Instance.cumulationTime, MPath.CumulationTimePath, true);
    }

    private void StartRecord()
    {
        Timer.Instance.ChangeState(TimerState.Start);
    }
    private void PauseRecord()
    {
        Timer.Instance.ChangeState(TimerState.Pause);
    }
    private void StopRecord()
    {
        Timer.Instance.ChangeState(TimerState.Stop);
    }
}
