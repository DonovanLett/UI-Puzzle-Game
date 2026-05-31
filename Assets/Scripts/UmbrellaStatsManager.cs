using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UmbrellaStatsManager : MonoBehaviour
{
    [SerializeField]
    private StatsManager[] _statsManagers;

    [SerializeField]
    private ToggleGroup[] _toggleGroups;

    [SerializeField]
    private ScrollRect[] _scrollRects;

    // Start is called before the first frame update
    void Start()
    {
        foreach(var manager in _statsManagers)
        {
           manager.Load();
        }

        foreach(ToggleGroup group in _toggleGroups)
        {
            Toggle currentToggle =
            group.ActiveToggles().FirstOrDefault();

            if (currentToggle != null)
            {
                currentToggle.onValueChanged.Invoke(currentToggle.isOn);
            }

            // Set their scroll to be at the top

        }
        StartCoroutine(OneFrameAfterStart());
        // PlayerPrefs.DeleteAll();
    }

    IEnumerator OneFrameAfterStart()
    {
        yield return null;

        foreach(ScrollRect scrollRect in _scrollRects)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(scrollRect.content);
            scrollRect.verticalNormalizedPosition = 1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
