using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RadioSequence : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _sliderName;

    [SerializeField]
    private TMP_Text _sliderValue;

    [SerializeField]
    

    public void SetValue(RadioSlider _slider)
    {
        _sliderName.text = _slider.name;
        _sliderValue.text = _slider._correctValue.ToString();
    }
}
