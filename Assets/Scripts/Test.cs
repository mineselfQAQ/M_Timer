using System;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    private void Start()
    {
        DateTime time = DateTime.Now.AddDays(3);
        //Debug.Log((int)time.DayOfWeek);
    }

    private void Update()
    {

    }
}
