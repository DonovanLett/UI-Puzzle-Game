using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISaveScript : MonoBehaviour
{
    // -----------------------------
    // Saved Data Classes
    // -----------------------------

    [System.Serializable]
    public class GraphicState
    {
        public Graphic graphic;
        public Color color;
        public bool enabled;
    }

    [System.Serializable]
    public class RectTransformState
    {
        public RectTransform rect;
        public Vector2 anchoredPosition;
        public Vector2 sizeDelta;
        public Vector3 localScale;
        public Quaternion localRotation;
    }

    [System.Serializable]
    public class TMPTextState
    {
        public TMP_Text textObject;
        public string text;
    }

    [System.Serializable]
    public class SliderState
    {
        public Slider slider;
        public float value;
    }

    [System.Serializable]
    public class ToggleState
    {
        public Toggle toggle;
        public bool value;
    }

    // -----------------------------
    // Saved Collections
    // -----------------------------

    private List<GraphicState> graphicStates = new();
    private List<RectTransformState> rectStates = new();
    private List<TMPTextState> textStates = new();
    private List<SliderState> sliderStates = new();
    private List<ToggleState> toggleStates = new();

    // -----------------------------
    // Save UI State
    // -----------------------------

    private void Start()
    {
        SaveState();
    }

    [ContextMenu("Save UI State")]
    public void SaveState()
    {
        // Clear old saves
        graphicStates.Clear();
        rectStates.Clear();
        textStates.Clear();
        sliderStates.Clear();
        toggleStates.Clear();

        // -----------------------------
        // Save Graphic Components
        // -----------------------------
        Graphic[] graphics = GetComponentsInChildren<Graphic>(true);

        foreach (Graphic g in graphics)
        {
            graphicStates.Add(new GraphicState
            {
                graphic = g,
                color = g.color,
                enabled = g.enabled
            });
        }

        // -----------------------------
        // Save RectTransforms
        // -----------------------------
        RectTransform[] rects = GetComponentsInChildren<RectTransform>(true);

        foreach (RectTransform r in rects)
        {
            rectStates.Add(new RectTransformState
            {
                rect = r,
                anchoredPosition = r.anchoredPosition,
                sizeDelta = r.sizeDelta,
                localScale = r.localScale,
                localRotation = r.localRotation
            });
        }

        // -----------------------------
        // Save TMP Text
        // -----------------------------
        TMP_Text[] texts = GetComponentsInChildren<TMP_Text>(true);

        foreach (TMP_Text t in texts)
        {
            textStates.Add(new TMPTextState
            {
                textObject = t,
                text = t.text
            });
        }

        // -----------------------------
        // Save Sliders
        // -----------------------------
        Slider[] sliders = GetComponentsInChildren<Slider>(true);

        foreach (Slider s in sliders)
        {
            sliderStates.Add(new SliderState
            {
                slider = s,
                value = s.value
            });
        }

        // -----------------------------
        // Save Toggles
        // -----------------------------
        Toggle[] toggles = GetComponentsInChildren<Toggle>(true);

        foreach (Toggle t in toggles)
        {
            toggleStates.Add(new ToggleState
            {
                toggle = t,
                value = t.isOn
            });
        }

        Debug.Log("UI State Saved.");
    }

    // -----------------------------
    // Load UI State
    // -----------------------------

    [ContextMenu("Load UI State")]
    public void LoadState()
    {
        // Restore Graphics
        foreach (GraphicState g in graphicStates)
        {
            if (g.graphic == null) continue;

            g.graphic.color = g.color;
            g.graphic.enabled = g.enabled;
        }

        // Restore RectTransforms
        foreach (RectTransformState r in rectStates)
        {
            if (r.rect == null) continue;

            r.rect.anchoredPosition = r.anchoredPosition;
            r.rect.sizeDelta = r.sizeDelta;
            r.rect.localScale = r.localScale;
            r.rect.localRotation = r.localRotation;
        }

        // Restore TMP Text
        foreach (TMPTextState t in textStates)
        {
            if (t.textObject == null) continue;

            t.textObject.text = t.text;
        }

        // Restore Sliders
        foreach (SliderState s in sliderStates)
        {
            if (s.slider == null) continue;

            s.slider.value = s.value;
        }

        // Restore Toggles
        foreach (ToggleState t in toggleStates)
        {
            if (t.toggle == null) continue;

            t.toggle.isOn = t.value;
        }

        Debug.Log("UI State Loaded.");
    }
}