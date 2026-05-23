using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UmbrellaStatsManager : MonoBehaviour
{
    [SerializeField]
    private StatsManager[] _statsManagers;

    // Start is called before the first frame update
    void Start()
    {
        foreach(var manager in _statsManagers)
        {
           manager.Load();
        }
       // PlayerPrefs.DeleteAll();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
