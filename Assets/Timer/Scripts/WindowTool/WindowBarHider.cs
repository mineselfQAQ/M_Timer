using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class WindowBarHider
{
    /// <summary>
    /// ���ڷ��
    /// </summary>
    const int GWL_STYLE = -16;
    /// <summary>
    /// ������
    /// </summary>
    const int WS_CAPTION = 0x00c00000;

    [DllImport("user32.dll")] public static extern IntPtr GetForegroundWindow();
    [DllImport("user32.dll")] public static extern long GetWindowLong(IntPtr hwd, int nIndex);
    [DllImport("user32.dll")] public static extern void SetWindowLong(IntPtr hwd, int nIndex, long dwNewLong);

    //�����ã����ڳ�ʼ���������أ�����ֻ�������ˣ�ʵ�ʻ��Ǵ��ڵ�
    /*
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void HideWindowBar()
    {
        // ��ô��ھ��
        var hwd = GetForegroundWindow();
        // ���ر�����
        var wl = GetWindowLong(hwd, GWL_STYLE);
        wl &= ~WS_CAPTION;
        SetWindowLong(hwd, GWL_STYLE, wl);
    }
    */
}
