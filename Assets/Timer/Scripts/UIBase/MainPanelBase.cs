
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NRFramework;

public class MainPanelBase : UIPanel
{	protected TextMeshProUGUI m_Number_TMPText;	protected Button m_Btn_Start_Button;	protected Button m_Btn_Stop_Button;	protected Button m_Btn_End_Button;	protected TextMeshProUGUI m_TodayTotalTimeText_TMPText;	protected TextMeshProUGUI m_TotalTimeText_TMPText;	protected TMP_InputField m_InputFieldTMP_TMPInputField;	protected Button m_Btn_Count_Button;
    protected override void OnBindCompsAndEvents() 
    {		m_Number_TMPText = (TextMeshProUGUI)viewBehaviour.GetComponentByIndexs(0, 0);		m_Btn_Start_Button = (Button)viewBehaviour.GetComponentByIndexs(1, 0);		m_Btn_Stop_Button = (Button)viewBehaviour.GetComponentByIndexs(2, 0);		m_Btn_End_Button = (Button)viewBehaviour.GetComponentByIndexs(3, 0);		m_TodayTotalTimeText_TMPText = (TextMeshProUGUI)viewBehaviour.GetComponentByIndexs(4, 0);		m_TotalTimeText_TMPText = (TextMeshProUGUI)viewBehaviour.GetComponentByIndexs(5, 0);		m_InputFieldTMP_TMPInputField = (TMP_InputField)viewBehaviour.GetComponentByIndexs(6, 0);		m_Btn_Count_Button = (Button)viewBehaviour.GetComponentByIndexs(7, 0);		BindEvent(m_Btn_Start_Button);		BindEvent(m_Btn_Stop_Button);		BindEvent(m_Btn_End_Button);		BindEvent(m_InputFieldTMP_TMPInputField);		BindEvent(m_Btn_Count_Button);	}

    protected override void OnUnbindCompsAndEvents() 
    {		UnbindEvent(m_Btn_Start_Button);		UnbindEvent(m_Btn_Stop_Button);		UnbindEvent(m_Btn_End_Button);		UnbindEvent(m_InputFieldTMP_TMPInputField);		UnbindEvent(m_Btn_Count_Button);		m_Number_TMPText = null;		m_Btn_Start_Button = null;		m_Btn_Stop_Button = null;		m_Btn_End_Button = null;		m_TodayTotalTimeText_TMPText = null;		m_TotalTimeText_TMPText = null;		m_InputFieldTMP_TMPInputField = null;		m_Btn_Count_Button = null;	}
}