using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class StatsManager : MonoBehaviour
{
    private enum ManagerType
    {
        Chronological,
        Sorted,
    }

    [SerializeField]
    private ManagerType _type;

    [SerializeField]
    private List<float> _times;

    [SerializeField]
    private List<GameObject> _timeTexts;

    /*
    /// Newest to oldest
    //
    [Header("Newest to Oldest")]
    [SerializeField]
    private List<float> _newestTimes;

    [SerializeField]
    private List<GameObject> _newestTimeTexts;

    [SerializeField]
    private GameObject _newestSpace;

    [SerializeField]
    private string _newestSaveString;
    //
    */

    [SerializeField]
    private GameObject _timeTextPrefab;

    [SerializeField]
    private string _saveString;

    private void Start()
    {
       // Debug.Log(PlayerPrefs.GetString("SAVE_TIMES")); 
        Load();
    }

    public void AddTimeValue(Timer timer)
    {
        if (_type == ManagerType.Sorted)
        {
            Debug.Log("New value added to " + name + ": " + timer.ElapsedTime);
           // _times.Insert(0, timer.ElapsedTime);
            _times.Add(timer.ElapsedTime);
            _times.Sort();
            string text = FormatTime(timer.Hours, timer.Minutes, timer.Seconds);

            DateTime currentTime = DateTime.Now;
            // text += " - " + currentTime.ToString("MM/dd/yyyy - hh:mm tt"); // originally "MMMM dd, yyyy - hh:mm tt"
            text += " - " + currentTime.ToString("hh:mm tt (MM/dd/yy)");


            GameObject newText = Instantiate(_timeTextPrefab, transform.position, Quaternion.identity, this.GetComponent<RectTransform>()); /// This will need to be fixed
            newText.GetComponent<TMP_Text>().text = text;
            newText.transform.SetSiblingIndex(_times.IndexOf(timer.ElapsedTime));
            _timeTexts.Insert(_times.IndexOf(timer.ElapsedTime), newText);
            Save();
        }
        else if(_type == ManagerType.Chronological)
        {
            _times.Insert(0, timer.ElapsedTime);
            //_times.Add(timer.ElapsedTime);
            string text = FormatTime(timer.Hours, timer.Minutes, timer.Seconds);

            DateTime currentTime = DateTime.Now;
            // text += " - " + currentTime.ToString("MM/dd/yyyy - hh:mm tt"); // originally "MMMM dd, yyyy - hh:mm tt"
            text += " - " + currentTime.ToString("hh:mm tt (MM/dd/yy)");


            GameObject newText = Instantiate(_timeTextPrefab, transform.position, Quaternion.identity, this.GetComponent<RectTransform>());
            newText.GetComponent<TMP_Text>().text = text;
            newText.transform.SetSiblingIndex(_times.IndexOf(timer.ElapsedTime));
            _timeTexts.Insert(_times.IndexOf(timer.ElapsedTime), newText);
            Save();
        }
    }

    public void AddTimeValueOld(Timer timer)
    {
        Debug.Log("New value added to " + name + ": " + timer.ElapsedTime);
        _times.Add(timer.ElapsedTime);
        _times.Sort();
        string text = FormatTime(timer.Hours, timer.Minutes, timer.Seconds);

        DateTime currentTime = DateTime.Now;
        // text += " - " + currentTime.ToString("MM/dd/yyyy - hh:mm tt"); // originally "MMMM dd, yyyy - hh:mm tt"
        text += " - " + currentTime.ToString("hh:mm tt (MM/dd/yy)");


        GameObject newText = Instantiate(_timeTextPrefab, transform.position, Quaternion.identity, this.GetComponent<RectTransform>()); /// This will need to be fixed
        newText.GetComponent<TMP_Text>().text = text;
        newText.transform.SetSiblingIndex(_times.IndexOf(timer.ElapsedTime));
        _timeTexts.Insert(_times.IndexOf(timer.ElapsedTime), newText);
        Save();
    }

    public void AddChronologically(Timer timer) // Maybe use later
    {
        _times.Add(timer.ElapsedTime);
        string text = FormatTime(timer.Hours, timer.Minutes, timer.Seconds);

        DateTime currentTime = DateTime.Now;
        // text += " - " + currentTime.ToString("MM/dd/yyyy - hh:mm tt"); // originally "MMMM dd, yyyy - hh:mm tt"
        text += " - " + currentTime.ToString("hh:mm tt (MM/dd/yy)");


        GameObject newText = Instantiate(_timeTextPrefab, transform.position, Quaternion.identity, this.GetComponent<RectTransform>());
        newText.GetComponent<TMP_Text>().text = text;
        newText.transform.SetSiblingIndex(_times.IndexOf(timer.ElapsedTime));
        _timeTexts.Insert(_times.IndexOf(timer.ElapsedTime), newText);
        Save();
    }

    public void AddQuickestToSlowest(Timer timer) // Maybe use later
    {
        _times.Add(timer.ElapsedTime);
        _times.Sort();
        string text = FormatTime(timer.Hours, timer.Minutes, timer.Seconds);

        DateTime currentTime = DateTime.Now;
        // text += " - " + currentTime.ToString("MM/dd/yyyy - hh:mm tt"); // originally "MMMM dd, yyyy - hh:mm tt"
        text += " - " + currentTime.ToString("hh:mm tt (MM/dd/yy)");

        GameObject newText = Instantiate(_timeTextPrefab, transform.position, Quaternion.identity, this.GetComponent<RectTransform>());
        newText.GetComponent<TMP_Text>().text = text;
        newText.transform.SetSiblingIndex(_times.IndexOf(timer.ElapsedTime));
        _timeTexts.Insert(_times.IndexOf(timer.ElapsedTime), newText);
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

        PlayerPrefs.SetString(_saveString, json); // PlayerPrefs.SetString("SAVE_TIMES", json);
        PlayerPrefs.Save();
    }

    
    /*
    /// Newest to oldest
    /// 
    public void Save(List<float> times, List<GameObject> timeTexts, string saveString)
    {
        StatData data = new StatData();

        data.times = times;

        // Instead of saving GameObjects, save what they represent
        foreach (GameObject obj in timeTexts)
        {
            data.textValues.Add(obj.GetComponent<TMPro.TMP_Text>().text);
        }

        string json = JsonUtility.ToJson(data);

        PlayerPrefs.SetString(saveString, json); // PlayerPrefs.SetString("SAVE_TIMES", json);
        PlayerPrefs.Save();
    }
    //
    */

    public void Load()
    {
        /// Clearing old text
        //
        foreach (GameObject obj in _timeTexts)
        {
            Destroy(obj);
        }

        _timeTexts.Clear();
        //


        if (!PlayerPrefs.HasKey(_saveString))
            return;

        string json = PlayerPrefs.GetString(_saveString);

        StatData data = JsonUtility.FromJson<StatData>(json);

        _times = data.times;

        _timeTexts = new List<GameObject>();

        foreach (string text in data.textValues)
        {
            GameObject obj = Instantiate(_timeTextPrefab, this.GetComponent<RectTransform>()); // originally GameObject obj = Instantiate(_timeTextPrefab);
            obj.GetComponent<TMPro.TMP_Text>().text = text;

            _timeTexts.Add(obj);
        }
    }


    /*
    /// Newest to oldest
    ///
    public void Load(List<float> times, List<GameObject> timeTexts, string saveString)
    {
        /// Clearing old text
        //
        foreach (GameObject obj in _timeTexts)
        {
            Destroy(obj);
        }

        _timeTexts.Clear();
        //


        if (!PlayerPrefs.HasKey(saveString))
            return;

        string json = PlayerPrefs.GetString(saveString);

        StatData data = JsonUtility.FromJson<StatData>(json);

        _times = data.times;

        _timeTexts = new List<GameObject>();

        foreach (string text in data.textValues)
        {
            GameObject obj = Instantiate(_timeTextPrefab, this.GetComponent<RectTransform>()); // originally GameObject obj = Instantiate(_timeTextPrefab);
            obj.GetComponent<TMPro.TMP_Text>().text = text;

            _timeTexts.Add(obj);
        }
    }
    /// 
    */

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