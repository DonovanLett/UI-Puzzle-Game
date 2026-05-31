using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollRectSyncManager : MonoBehaviour
{
    [SerializeField]
    private ScrollRect[] scrollRects;

    private ScrollRect activeScroll;

    private bool isSyncing;

    private void Start()
    {
        foreach (ScrollRect scrollRect in scrollRects)
        {
            scrollRect.onValueChanged.AddListener(
                (pos) => OnScrollChanged(scrollRect, pos));
        }
        Debug.Log("Thing Added");
    }

    public void SetActiveScroll(
        ScrollRect scrollRect)
    {
        activeScroll = scrollRect;
    }

    public void ClearActiveScroll(
        ScrollRect scrollRect)
    {
        if (activeScroll == scrollRect)
        {
            activeScroll = null;
        }
    }

    public void ClearActiveScroll()
    {
        activeScroll = null;
    }

    private void OnScrollChanged(
        ScrollRect source,
        Vector2 position)
    {
        if (isSyncing)
        {
            return;
        }  

        if (source != activeScroll)
        {
            return;
        }

        isSyncing = true;

        foreach (ScrollRect target in scrollRects)
        {
            // Skip self
            if (target == source)
                continue;

            // Vertical only
            target.verticalNormalizedPosition =
                source.verticalNormalizedPosition;

            ScrollRectState state = target.GetComponent<ScrollRectState>();

            if (state != null)
            {
                state.lastVerticalPosition = source.verticalNormalizedPosition;
            }
        }

        isSyncing = false;
    }
}
