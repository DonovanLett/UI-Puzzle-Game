using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _areas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchToArea(int _areaIndex)
    {
        if(_areaIndex >= _areas.Length)
        {
            Debug.Log("Area Index not acceptable.");
        }
        else
        {
            for(int i = 0; i < _areas.Length; i++)
            {
                _areas[i].SetActive(false);
            }

            _areas[_areaIndex].SetActive(true);
        }
    }
}
