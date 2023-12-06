using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class RecordUI
{
    private static int showNum = 12;
    private static List<GameObject> recordList;

    private static Transform context;
    private static GameObject prefab;
    private static RecordInfoGroup group;
    private static ScrollRect rect;

    public static void Init()
    {
        recordList = new List<GameObject>();
        GameObject parent = GameObject.Find("ScrollView_Record");
        context = parent.transform.Find("Viewport").Find("content").transform;
        prefab = (GameObject)Resources.Load("RecordText");
        rect = parent.GetComponent<ScrollRect>();
        group = JsonManager.Instance.recordInfoGroup;

        if (group.info.Count < showNum)
        {
            foreach(var info in group.info)
            {
                CreateInstance(info);
            }
        }
        else
        {
            for (int i = group.info.Count - showNum; i < group.info.Count; i++)
            {
                RecordInfo info = group.info[i];
                CreateInstance(info);
            }
        }
        

        rect.normalizedPosition = new Vector2(0, 0);
    }

    public static void AddLastRecord()
    {
        RecordInfo info = group.info[group.info.Count - 1];
        CreateInstance(info);
        CoroutineHandler.Instance.AddCorotine(WaitOneFrame());
    }
    private static IEnumerator WaitOneFrame()
    {
        yield return null;
        rect.normalizedPosition = new Vector2(0, 0);
    }

    private static void CreateInstance(RecordInfo info)
    {
        if (recordList.Count >= showNum)
        {
            GameObject.Destroy(recordList[0]);
            recordList.RemoveAt(0);
        }
        
        string text = $"日期: {info.date}\n" +
                      $"时间: {info.startTime}-{info.endTime}\n" +
                      $"持续时间: {info.duration}min";

        GameObject instance = Object.Instantiate(prefab, context);
        instance.GetComponent<TMP_Text>().text = text;

        recordList.Add(instance);
    }
}
