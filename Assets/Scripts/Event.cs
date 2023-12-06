using DG.Tweening;
using UnityEngine;

public class Event
{
    private static Event instance;
    public static Event Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Event();
            }
            return instance;
        }
    }

    private Transform eventPanel;
    private RectTransform eventPanelRect;
    private Transform event_BackTrigger;

    private CanvasGroup calendarGroup;

    private int width = 250;
    private static Vector2 lastWH;//上一次点击宽高

    private bool isOpen;

    public void Init()
    {
        isOpen = false;

        eventPanel = GameObject.Find("EventPanel").transform;
        eventPanelRect = eventPanel.GetComponent<RectTransform>();
        event_BackTrigger = eventPanel.Find("Event_BackTrigger");

        calendarGroup = GameObject.Find("Calendar").GetComponent<CanvasGroup>();

        lastWH = new Vector2(Screen.width, Screen.height);
        //初始基准1920x1080，如果不是，初始化时就需要进行缩放
        if (lastWH.x != 1920)
        {
            float xScale = (lastWH.x - width) / (1920.0f - width);
            CanvasHandler.ChangeScale(event_BackTrigger, xScale, 1, 1);
        }
    }

    public void Update()
    {
        //记录界面占1/3情况
        if (Screen.width < width * 3)
        {
            //如果调整宽度导致太窄了，就自动缩回去
            if (isOpen)
            {
                CloseRightMenu();
            }
        }
    }

    public void OpenRightMenu()
    {
        //太窄了，不让打开
        if (Screen.width < width * 3)
        {
            return;
        }

        isOpen = true;
        eventPanel.DOLocalMoveX(-eventPanelRect.rect.width, 0.5f).SetEase(Ease.OutQuart).OnComplete(()=>
        {
            //弹出面板的时候检测分辨率是否不满足要求
            if (lastWH.x != Screen.width || lastWH.y != Screen.height)
            {
                float xScale = (Screen.width - width) / (lastWH.x - width);
                CanvasHandler.ChangeScale(event_BackTrigger, xScale, 1, 1);

                lastWH = new Vector2(Screen.width, Screen.height);
            }
            event_BackTrigger.gameObject.SetActive(true);
        });
    }
    public void CloseRightMenu()
    {
        isOpen = false;
        eventPanel.DOLocalMoveX(0, 0.5f).SetEase(Ease.OutQuart).OnComplete(() =>
        {
            event_BackTrigger.gameObject.SetActive(false);
        });
    }

    public void OpenLeftMenu()
    {
        calendarGroup.blocksRaycasts = true;
        calendarGroup.DOFade(1, 0.3f);
    }
    public void CloseLeftMenu()
    {
        calendarGroup.blocksRaycasts = false;
        calendarGroup.DOFade(0, 0.3f);
    }
}
