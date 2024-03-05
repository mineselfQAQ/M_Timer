using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class WindowBarHider
{
    /// <summary>
    /// 窗口风格
    /// </summary>
    const int GWL_STYLE = -16;
    /// <summary>
    /// 标题栏
    /// </summary>
    const int WS_CAPTION = 0x00c00000;

    [DllImport("user32.dll")] public static extern IntPtr GetForegroundWindow();
    [DllImport("user32.dll")] public static extern long GetWindowLong(IntPtr hwd, int nIndex);
    [DllImport("user32.dll")] public static extern void SetWindowLong(IntPtr hwd, int nIndex, long dwNewLong);

    //不好用，会在初始化完后才隐藏，而且只是隐藏了，实际还是存在的
    /*
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void HideWindowBar()
    {
        // 获得窗口句柄
        var hwd = GetForegroundWindow();
        // 隐藏标题栏
        var wl = GetWindowLong(hwd, GWL_STYLE);
        wl &= ~WS_CAPTION;
        SetWindowLong(hwd, GWL_STYLE, wl);
    }
    */
}
