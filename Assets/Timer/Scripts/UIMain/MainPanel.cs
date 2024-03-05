using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class MainPanel : MainPanelBase
{
    //持续时间(单位---秒)
    public BindableProperty<float> Duration { get; } = new BindableProperty<float>()
    {
        Value = 0f
    };

    private bool isRunning;

    public void Init()
    {
        Duration.OnValueChanged += OnDurationChanged;
        isRunning = false;

        PersistentModel.TodayTotalTime.OnValueChanged += OnTodayTotalTimeChanged;
        PersistentModel.TotalTime.OnValueChanged += OnTotalTimeChanged;

        string lastStartDay = PlayerPrefs.GetString("LastStartDay", "0001/01/01");
        if (lastStartDay == "0001/01/01")
        {
            lastStartDay = DateTime.Now.ToString("d");
            PlayerPrefs.SetString("LastStartDay", lastStartDay);
        }
        else if (lastStartDay != DateTime.Now.ToString("d"))
        {
            PlayerPrefs.SetString("LastStartDay", lastStartDay);
            PersistentModel.TodayTotalTime.Value = 0;
        }

        int todayTotalTime = PlayerPrefs.GetInt("TodayTotalTime", 0);
        float totalTime = PlayerPrefs.GetFloat("TotalTime", 0.0f);
        PersistentModel.TodayTotalTime.Value = todayTotalTime;
        PersistentModel.TotalTime.Value = totalTime;
    }

    public void Update()
    {
        if (isRunning)
        {
            Duration.Value += Time.deltaTime;
        }
    }

    public void OnApplicationQuit()
    {
        PersistentModel.TodayTotalTime.Value += SecondTimeToMinTime(Duration.Value);
        PersistentModel.TotalTime.Value += SecondTimeToHourTime(Duration.Value);
    }

    private void OnDurationChanged(float newDuration)
    {
        //当m_duration发生变动时，更改UI状态
        m_Number_TMPText.text = FloatTimeToStringTime(newDuration);
    }
    private void OnTodayTotalTimeChanged(int newTime)
    {
        m_TodayTotalTimeText_TMPText.text = newTime.ToString();
        PlayerPrefs.SetInt("TodayTotalTime", newTime);
    }
    private void OnTotalTimeChanged(float newTime)
    {
        m_TotalTimeText_TMPText.text = newTime.ToString("F1");
        PlayerPrefs.SetFloat("TotalTime", newTime);
    }

    protected override void OnCreating() 
    {

    }

    protected override void OnCreated() 
    {

    }

    protected override void OnClicked(Button button)
    {
        if (button == m_Btn_Start_Button)
        {
            isRunning = true;
        }
        else if (button == m_Btn_Stop_Button)
        {
            isRunning = false;
        }
        else if (button == m_Btn_End_Button)
        {
            PersistentModel.TodayTotalTime.Value += SecondTimeToMinTime(Duration.Value);
            PersistentModel.TotalTime.Value += SecondTimeToHourTime(Duration.Value);

            isRunning = false;
            Duration.Value = 0.0f;
        }
        else if (button == m_Btn_Count_Button)
        {
            if (m_InputFieldTMP_TMPInputField.text != "")
            {
                int secTime = int.Parse(m_InputFieldTMP_TMPInputField.text);
                int minTime = secTime * 60;
                PersistentModel.TodayTotalTime.Value += SecondTimeToMinTime(minTime);
                PersistentModel.TotalTime.Value += SecondTimeToHourTime(minTime);

                m_InputFieldTMP_TMPInputField.text = "";
            }
        }
    }

    protected override void OnValueChanged(Toggle toggle, bool value) { }

    protected override void OnValueChanged(Dropdown dropdown, int value) { }

    protected override void OnValueChanged(TMP_Dropdown tmpDropdown, int value) { }

    protected override void OnValueChanged(InputField inputField, string value) { }

    protected override void OnValueChanged(TMP_InputField tmpInputField, string value) { }

    protected override void OnValueChanged(Slider slider, float value) { }

    protected override void OnValueChanged(Scrollbar scrollbar, float value) { }

    protected override void OnValueChanged(ScrollRect scrollRect, Vector2 value) { }
    
    protected override void OnVisibleChanged(bool visible) { }
    
    protected override void OnFocusChanged(bool got) { }

    //protected override void OnBackgroundClicked() { }

    //protected override void OnEscButtonPressed() { }

    protected override void OnDestroying() { }

    protected override void OnDestroyed() { }

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
    private int SecondTimeToMinTime(float time)
    {
        return (int)time / 60;
    }
    private float SecondTimeToHourTime(float time)
    { 
        return time / 3600;
    }
}