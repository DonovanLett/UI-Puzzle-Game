using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ValueSlot : MonoBehaviour
{
    [SerializeField]
    private TMP_Text nameText;
    [SerializeField] 
    private TMP_Text valueText;

    public void Setup(string name, float value)
    {
        valueText.text = value.ToString("000.000");
        nameText.text = name;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
