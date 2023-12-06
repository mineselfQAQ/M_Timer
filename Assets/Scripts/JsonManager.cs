using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;


[Serializable]
public class CumulationTime
{
    public float monthCumulation;
    public int dayCumulation;//其实不需要
}

[Serializable]
public class RecordInfo
{
    public string date;
    public string startTime;
    public string endTime;
    public int duration;
}
public class RecordInfoGroup
{
    public List<RecordInfo> info;
}

public class JsonManager : Singleton<JsonManager>
{
    public RecordInfoGroup recordInfoGroup;
    public CumulationTime cumulationTime;


    //每次启动都需要进行的操作
    public void Init()
    {
        //---RecordInfo---
        recordInfoGroup = FromJson<RecordInfoGroup>(MPath.RecordInfoPath);
        if (recordInfoGroup == default(RecordInfoGroup))
        {
            recordInfoGroup = new RecordInfoGroup();
            recordInfoGroup.info = new List<RecordInfo>();
        }

        //---CumulationTime---
        cumulationTime = FromJson<CumulationTime>(MPath.CumulationTimePath);
        if (cumulationTime == default(CumulationTime))
        {
            cumulationTime = new CumulationTime();
            cumulationTime.dayCumulation = 0;
            cumulationTime.monthCumulation = 0.0f;
        }
        
    }

    public void ToJson<T>(T info, string path, bool prettyPrint)
    {
        string serializationStr = JsonUtility.ToJson(info, prettyPrint);
        File.WriteAllText(path, serializationStr);
    }

    public T FromJson<T>(string path)
    {
        if (!File.Exists(path))
        {
            FileStream fs = File.Create(path);
            fs.Close();
        }

        string deserializationStr = File.ReadAllText(path);
        if (deserializationStr != null)
        {
            T result = JsonUtility.FromJson<T>(deserializationStr);
            return result;
        }
        return default(T);
    }
}
