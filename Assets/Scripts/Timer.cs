using System;
using TMPro;
using UnityEngine;

public enum TimerState
{
    Start,
    Pause,
    Stop
}

public class Timer
{
    private static Timer instance;
    public static Timer Instance
    {
        get 
        {
            if (instance == null)
            {
                instance = new Timer();
            }
            return instance;
        }
    }

    public string Time { get { return FloatTimeToStringTime(elapsedTime); } }
    private float elapsedTime = 0.0f;

    private bool isRunning;
    private bool isFirstStart;

    private string date;
    private string startTime;
    private string endTime;

    private TMP_Text summaryText;

    //注意要在Monobehaviour中调用该Start()
    public void Init()
    {
        isFirstStart = true;

        summaryText = GameObject.Find("Summary").GetComponent<TMP_Text>();
    }

    //注意要在Monobehaviour中调用该Update()
    public void Update()
    {
        if (isRunning)
        {
            elapsedTime += UnityEngine.Time.deltaTime;
        }
    }

    private string FloatTimeToStringTime(float time)
    {
        //首先time是以秒为单位的，有转换公式：
        //1小时=60分钟=3600秒
        int hour = (int)time / 3600;
        int minute = ((int)time - hour * 3600) / 60;
        int second = (int)time - hour * 3600 - minute * 60;

        string stringTime = $"{hour.ToString("D2")}:{minute.ToString("D2")}:{second.ToString("D2")}";

        return stringTime;
    }

    public void ChangeState(TimerState state)
    {
        switch (state)
        {
            case TimerState.Start:
                {
                    if (isFirstStart)
                    {
                        date = $"{DateTime.Now.Year}.{DateTime.Now.Month}.{DateTime.Now.Day}";//日期(xxxx.xx.xx)
                        startTime = $"{AddZero(DateTime.Now.Hour)}:{AddZero(DateTime.Now.Minute)}";//开始时间(xx:xx)
                        isFirstStart = false;
                    }
                    isRunning = true;
                }
                break;
            case TimerState.Pause:
                {
                    isRunning = false;
                }
                break;
            case TimerState.Stop:
                {
                    if (elapsedTime == 0.0f) { return; }

                    //终止本次计时，将结果放入日历中
                    //需要信息：
                    //本次时长|开始与结束时间|当天日期
                    int duration = (int)elapsedTime / 60;//本次时长(分钟min)
                    endTime = $"{AddZero(DateTime.Now.Hour)}:{AddZero(DateTime.Now.Minute)}";//结束时间(xx:xx)

                    RecordInfo info = new RecordInfo();
                    info.date = date;
                    info.startTime = startTime;
                    info.endTime = endTime;
                    info.duration = duration;
                    //---记录内容更新---
                    JsonManager.Instance.recordInfoGroup.info.Add(info);
                    JsonManager.Instance.ToJson(JsonManager.Instance.recordInfoGroup,
                        MPath.RecordInfoPath, true);
                    //---记录面板更新---
                    RecordUI.AddLastRecord();
                    //---下侧累计时间更新---
                    JsonManager.Instance.cumulationTime.dayCumulation += duration;
                    JsonManager.Instance.cumulationTime.monthCumulation += duration / 60.0f;
                    summaryText.text = $"本月累计：{JsonManager.Instance.cumulationTime.monthCumulation.ToString("F2")} h" +
                        $"        本日累计：{JsonManager.Instance.cumulationTime.dayCumulation} m";
                    //---日历更新---
                    Calendar.AddCalendarInfo(info);
                    //重置
                    isRunning = false;
                    isFirstStart = true;
                    elapsedTime = 0.0f;
                }
                break;
            default:
                {
                    Debug.Log("gg");
                }
                break;
        }
    }

    public string AddZero(int num)
    {
        if (num < 10)
        {
            return $"0{num}";
        }
        return num.ToString();
    }
}
