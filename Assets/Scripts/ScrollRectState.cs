using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollRectState : MonoBehaviour
{
    [HideInInspector]
    public float lastVerticalPosition = 1f;

    private ScrollRect scrollRect;

    private void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
    }

    private void OnEnable()
    {
        Canvas.ForceUpdateCanvases();

        LayoutRebuilder.ForceRebuildLayoutImmediate(
            scrollRect.content);

        scrollRect.verticalNormalizedPosition =
            lastVerticalPosition;

        Canvas.ForceUpdateCanvases();
    }
}
