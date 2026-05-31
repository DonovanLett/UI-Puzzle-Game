using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollRectActivityReporter : MonoBehaviour
{
    [SerializeField]
    private ScrollRectSyncManager syncManager;

    private ScrollRect scrollRect;

    private void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
    }

    public void OnBeginDrag()
    {
        syncManager.SetActiveScroll(scrollRect);
    }

    /*
    private void OnDisable()
    {
        syncManager.ClearActiveScroll(scrollRect);
    }
    */
}