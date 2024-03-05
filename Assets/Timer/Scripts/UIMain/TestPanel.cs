using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NRFramework;

public class TestPanel : TestPanelBase
{
    public void Init()
    {
        m_TextTMP_TMPText.text = "OK";
    }

    protected override void OnCreating() 
    {
        Debug.Log("开始创建");
    }

    protected override void OnCreated()
    {
        Debug.Log("创建完毕");
    }

    protected override void OnClicked(Button button) { }

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
}