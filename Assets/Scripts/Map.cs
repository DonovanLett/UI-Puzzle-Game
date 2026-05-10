using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _areas;

    public void SwitchToArea(GameObject area)
    {
        if (_areas.Contains(area))
        {
            for (int i = 0; i < _areas.Length; i++)
            {
                _areas[i].SetActive(false);
            }
            area.SetActive(true);
        }



        /*
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
        */
    }
}