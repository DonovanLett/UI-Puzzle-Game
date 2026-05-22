using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsManager : MonoBehaviour
{
    [SerializeField]
    private List<float> _times;

    [SerializeField]
    private List<GameObject> _timeTexts;

    [SerializeField]
    private GameObject _timeTextPrefab;

    private void Start()
    {
        Load();
    }

    public void AddTimeValue(Timer timer)
    {
        _times.Add(timer.ElapsedTime);
        _times.Sort();
        string text = FormatTime(timer.Hours, timer.Minutes, timer.Seconds);
        GameObject newText = Instantiate(_timeTextPrefab, transform.position, Quaternion.identity);
        newText.transform.SetParent(this.GetComponent<RectTransform>(), true);
        newText.GetComponent<TMP_Text>().text = text;
        newText.transform.SetSiblingIndex(_times.IndexOf(timer.ElapsedTime));
        _timeTexts.Add(newText);
        Save();
    }

    public void Save()
    {
        StatData data = new StatData();

        data.times = _times;

        // Instead of saving GameObjects, save what they represent
        foreach (GameObject obj in _timeTexts)
        {
            data.textValues.Add(obj.GetComponent<TMPro.TMP_Text>().text);
        }

        string json = JsonUtility.ToJson(data);

        PlayerPrefs.SetString("SAVE_TIMES", json);
        PlayerPrefs.Save();
    }

    public void Load()
    {
        if (!PlayerPrefs.HasKey("SAVE_TIMES"))
            return;

        string json = PlayerPrefs.GetString("SAVE_TIMES");

        StatData data = JsonUtility.FromJson<StatData>(json);

        _times = data.times;

        _timeTexts = new List<GameObject>();

        foreach (string text in data.textValues)
        {
            GameObject obj = Instantiate(_timeTextPrefab);
            obj.GetComponent<TMPro.TMP_Text>().text = text;

            _timeTexts.Add(obj);
        }
    }

    public string FormatTime(float hours, float minutes, float seconds)
    {
        List<string> parts = new List<string>();

        if (hours > 0)
            parts.Add(FormatUnit(hours, "hour"));

        if (minutes > 0)
            parts.Add(FormatUnit(minutes, "minute"));

        if (seconds > 0)
            parts.Add(FormatUnit(seconds, "second"));

        // Special case: everything is 0
        if (parts.Count == 0)
            return "0 seconds";

        // Build final string with commas + "and"
        if (parts.Count == 1)
            return parts[0];

        if (parts.Count == 2)
            return parts[0] + " and " + parts[1];

        return parts[0] + ", " + parts[1] + " and " + parts[2];
    }

    private string FormatUnit(float value, string unit)
    {
        int rounded = Mathf.FloorToInt(value); // or (int)value if you guarantee integers

        if (rounded == 1)
            return $"{rounded} {unit}";
        else
            return $"{rounded} {unit}s";
    }
}