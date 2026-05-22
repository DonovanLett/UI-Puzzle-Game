using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [Header("Timer State")]
    [SerializeField] private bool startOnAwake = false;

    private float elapsedTime;
    private bool isRunning;

    // Read-only properties

    [SerializeField]
    public int Hours => Mathf.FloorToInt(elapsedTime / 3600f);

    [SerializeField]
    public int Minutes => Mathf.FloorToInt((elapsedTime % 3600f) / 60f);

    [SerializeField]
    public int Seconds => Mathf.FloorToInt(elapsedTime % 60f);

    public float ElapsedTime => elapsedTime;

    [SerializeField]
    private StatsManager _connectedStatsManager;

    private void Start()
    {
        if (startOnAwake)
        {
            StartTimer();
        }

        //UpdateVisuals();
    }

    private void Update()
    {
        if (!isRunning)
            return;

        elapsedTime += Time.deltaTime;

        //UpdateVisuals();
    }

    public void StartTimer()
    {
        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;
        _connectedStatsManager.AddTimeValue(this);
    }

    public void ResetTimer()
    {
        elapsedTime = 0f;

        //UpdateVisuals();
    }

    public void ResetAndStopTimer()
    {
        StopTimer();
        ResetTimer();
    }


    /*
    private void UpdateVisuals()
    {
        if (timerText != null)
        {
            timerText.text = $"{Hours:00}:{Minutes:00}:{Seconds:00}";
        }
    }
    */
}
