using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CanvasHandler : MonoBehaviour
{
    private CanvasScaler scaler;
    private float width;

    private float mainScale;
    private float moduleScale;
    private RectTransform main;
    private RectTransform day;

    public int minWidth = 800;
    public int minHeight = 600;

    private void Start()
    {
        //根据显示器分辨率进行整体缩放
        width = Screen.currentResolution.width;
        mainScale = width / 1920.0f;//不考虑16:9以外的情况
        scaler = GetComponent<CanvasScaler>();
        scaler.scaleFactor = mainScale;

        main = GameObject.Find("Main").GetComponent<RectTransform>();
        day = GameObject.Find("Day").GetComponent<RectTransform>();
        //if (main.rect.width / Screen.width > 0.5f)
        //{
        //    float scale = Screen.width / 1920.0f;
        //    main.localScale = new Vector3(scale, scale, scale);
        //}
    }

    private void Update()
    {
        //临时---设置最小分辨率
        if (Screen.width <= minWidth && Screen.height <= minHeight)
            Screen.SetResolution(minWidth, minHeight, false);
        else if(Screen.width <= minWidth)
            Screen.SetResolution(minWidth, Screen.height, false);
        else if (Screen.height <= minHeight)
            Screen.SetResolution(Screen.width, minHeight, false);


        //main控件分区的缩放
        float scale = Screen.width / 800.0f;
        if (Screen.width <= 800)
        {
            main.localScale = new Vector3(scale, scale, scale);
            day.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            main.localScale = new Vector3(1, 1, 1);
            day.localScale = new Vector3(scale * 0.8f, scale * 0.8f, scale * 0.8f);
        }
        
        //鼠标点击到控件时可能进行的操作
        if (Input.GetMouseButtonUp(0))
        {
            GameObject go = GetFirstPickGameObject(Input.mousePosition);
            if (go && go.name == "Event_BackTrigger")
            {
                Event.Instance.CloseRightMenu();
            }
        }
    }

    private GameObject GetFirstPickGameObject(Vector2 position)
    {
        EventSystem eventSystem = EventSystem.current;
        PointerEventData pointerEventData = new PointerEventData(eventSystem);
        pointerEventData.position = position;
        //射线检测ui
        List<RaycastResult> uiRaycastResultCache = new List<RaycastResult>();
        eventSystem.RaycastAll(pointerEventData, uiRaycastResultCache);
        if (uiRaycastResultCache.Count > 0)
            return uiRaycastResultCache[0].gameObject;
        return null;
    }

    public static void ChangeScale(Transform t, float xScale, float yScale, float zScale)
    {
        t.localScale = new Vector3(t.localScale.x * xScale, t.localScale.y * yScale, t.localScale.z * zScale);
    }
}
