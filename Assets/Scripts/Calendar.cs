using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Calendar
{
    public static Dictionary<string, Calendar> day;//通过日期寻找Calendar实例

    public string date;
    public List<string> startTime;
    public List<string> endTime;
    public List<int> duration;
    public int totalTime;

    private static TMP_Text summaryText;

    public static void Init()
    {
        //===创建日历字典===
        //举例：通过day["2023.11.17"]可以获取当天的所有信息
        day = new Dictionary<string, Calendar>();
        foreach (var info in JsonManager.Instance.recordInfoGroup.info)
        {
            if (!day.ContainsKey(info.date))
            {
                Calendar calendar = new Calendar();
                calendar.date = info.date;
                calendar.startTime = new List<string>() { info.startTime };
                calendar.endTime = new List<string>() { info.endTime };
                calendar.duration = new List<int>() { info.duration };
                calendar.totalTime = calendar.duration[0];
                day.Add(info.date, calendar);
            }
            else
            {
                day[info.date].startTime.Add(info.startTime);
                day[info.date].endTime.Add(info.endTime);
                day[info.date].duration.Add(info.duration);
                day[info.date].totalTime += info.duration;
            }
        }

        string today = $"{DateTime.Now.Year}.{DateTime.Now.Month}.{DateTime.Now.Day}";
        if(day.ContainsKey(today))
            JsonManager.Instance.cumulationTime.dayCumulation = day[today].totalTime;

        summaryText = GameObject.Find("Summary").GetComponent<TMP_Text>();
        summaryText.text = $"本月累计：{JsonManager.Instance.cumulationTime.monthCumulation.ToString("F2")} h" +
                        $"        本日累计：{JsonManager.Instance.cumulationTime.dayCumulation} m";
    }

    public static void AddCalendarInfo(RecordInfo info)
    {
        if (!day.ContainsKey(info.date))
        {
            Calendar calendar = new Calendar();
            calendar.date = info.date;
            calendar.startTime = new List<string>() { info.startTime };
            calendar.endTime = new List<string>() { info.endTime };
            calendar.duration = new List<int>() { info.duration };
            calendar.totalTime = calendar.duration[0];
            day.Add(info.date, calendar);
        }
        else
        {
            day[info.date].startTime.Add(info.startTime);
            day[info.date].endTime.Add(info.endTime);
            day[info.date].duration.Add(info.duration);
            day[info.date].totalTime += info.duration;
        }
    }
}
