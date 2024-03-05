using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NRFramework;

public class TestPanelBase : UIPanel
{
	protected TextMeshProUGUI m_TextTMP_TMPText;
    protected override void OnBindCompsAndEvents() 
    {
		m_TextTMP_TMPText = (TextMeshProUGUI)viewBehaviour.GetComponentByIndexs(0, 0);
	}

    protected override void OnUnbindCompsAndEvents() 
    {
		m_TextTMP_TMPText = null;
	}
}