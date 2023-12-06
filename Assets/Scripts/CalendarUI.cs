using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DayBtn
{
    public Button btn;
    public Image ui;
    public TMP_Text text;
    public string date;
}

public class CalendarUI
{
    private static TMP_Text yearAndMonthText;
    private static List<DayBtn> btns = new List<DayBtn>();
    private static TMP_Text infoText;
    private static Button closeBtn;

    private static int currentShowYear;
    private static int currentShowMonth;

    public static void Init()
    {
        //===获取物体===
        Transform parent = GameObject.Find("Calendar").transform;
        //---左侧日历表---
        Transform calendarTable = parent.Find("CalendarTable");
        Transform mainCalendar = calendarTable.Find("Table");
        //日期：如2023年11月
        Transform yearAndMonth = mainCalendar.Find("YearAndMonth");
        yearAndMonthText = yearAndMonth.GetComponent<TMP_Text>();
        //按钮：上一个月/下一个月
        yearAndMonth.Find("Left").GetComponent<Button>().onClick.AddListener(() => 
        { 
            ChangeCurrentDate(true);
            RefreshUI();
        });
        yearAndMonth.Find("Right").GetComponent<Button>().onClick.AddListener(() =>
        {
            ChangeCurrentDate(false);
            RefreshUI();
        });
        //每个日期
        Transform day = mainCalendar.Find("Day");
        for (int i = 0; i <= 41; i++)
        {
            DayBtn btn = new DayBtn();
            Transform t = day.Find($"Button_{i}");
            btn.btn = t.GetComponent<Button>();
            btn.ui = t.GetComponent<Image>();
            btn.text = t.Find("Text (TMP)").GetComponent<TMP_Text>();
            btns.Add(btn);
        }
        //---右侧信息栏---
        Transform info = parent.Find("Info");
        infoText = info.Find("Text (TMP)").GetComponent<TMP_Text>();
        //---关闭按钮---
        closeBtn = parent.Find("CloseBtn").GetComponent<Button>();

        //===初始化===
        //添加关闭监听
        closeBtn.onClick.AddListener(() =>
        {
            Event.Instance.CloseLeftMenu();
        });
        //更改日期
        currentShowYear = DateTime.Now.Year;
        currentShowMonth = DateTime.Now.Month;
        //刷新UI
        RefreshUI();
    }

    public static void RefreshUI()
    {
        yearAndMonthText.text = $"{currentShowYear}年{currentShowMonth}月";

        DateTime date = new DateTime(currentShowYear, currentShowMonth, 1);

        int firstDay = (int)date.DayOfWeek;
        int firstIndex = firstDay == 0 ? 6 : firstDay - 1;

        int lastMonthLastDay = date.AddDays(-1).Day;
        int lastDay = date.AddMonths(1).AddDays(-1).Day;

        //更新
        foreach (var btn in btns)
        {
            btn.btn.interactable = true;
        }
        int num = lastMonthLastDay;
        for (int i = firstIndex - 1; i >= 0; i--)
        {
            btns[i].text.text = num.ToString();
            btns[i].ui.color = new Color(1, 1, 1, 0.2f);
            btns[i].date = $"{currentShowYear}.{currentShowMonth}.{num}";
            btns[i].btn.interactable = false;
            num--;
        }
        num = 1;
        for (int i = firstIndex; i < firstIndex + lastDay; i++)
        {
            btns[i].text.text = num.ToString();
            btns[i].ui.color = new Color(1, 1, 1, 0.5f);
            btns[i].date = $"{currentShowYear}.{currentShowMonth}.{num}";
            int temp = i;//i会发生闭包，使用其它变量存储
            btns[i].btn.onClick.RemoveAllListeners();
            btns[i].btn.onClick.AddListener(() =>
            {
                GetInfo(infoText, btns[temp].date);
            });
            num++;
        }
        num = 1;
        for (int i = firstIndex + lastDay; i <= 41; i++)
        {
            btns[i].text.text = num.ToString();
            btns[i].ui.color = new Color(1, 1, 1, 0.2f);
            btns[i].date = $"{currentShowYear}.{currentShowMonth}.{num}";
            btns[i].btn.interactable = false;
            num++;
        }
    }

    public static void ChangeCurrentDate(bool isLeft)
    {
        if (isLeft)
        {
            currentShowMonth -= 1;
            if (currentShowMonth == 0)
            {
                currentShowYear -= 1;
                currentShowMonth = 12;
            }
        }
        else
        {
            currentShowMonth += 1;
            if (currentShowMonth == 13)
            {
                currentShowYear += 1;
                currentShowMonth = 1;
            }
        }
    }

    private static void GetInfo(TMP_Text tmp, string date)
    {
        if (Calendar.day.ContainsKey(date))
        {
            tmp.fontSize = 18;
            tmp.alignment = TextAlignmentOptions.Top;
            tmp.text = $"<size=24>\n{Calendar.day[date].date}\n" +
                       $"当日总计: {Calendar.day[date].totalTime}min\n\n</size>";
            for (int i = 0; i < Calendar.day[date].duration.Count; i++)
            {
                tmp.text += $"{Calendar.day[date].startTime[i]}-{Calendar.day[date].endTime[i]}\n" +
                            $"持续时间: {Calendar.day[date].duration[i]}\n\n";
            }
        }
        else
        {
            tmp.fontSize = 28;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.text = "该天无记录";
        }
    }
}
