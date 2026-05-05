using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class RadioSlider : MonoBehaviour
{
    [SerializeField]
    public float _correctValue, _currentValue, _currentDistance, _fadingDistance; // Possibly eventually make distance a public float

    [SerializeField]
    private TMP_Text _displayedText, _valueText; // Second one is new

    // Start is called before the first frame update
    void Start()
    {
        /*
        SetValue();
        */
        /*
        _correctValue = Random.Range(GetComponent<Slider>().minValue + _fadingDistance, GetComponent<Slider>().maxValue); // You plus _fadingDistance to minValue here, but you can remove it if you ever make it so that the initial value of the slider is randomzied as well.
        _correctValue = Mathf.Ceil(_correctValue * 1000f) / 1000f;
        _displayedText.text = _currentValue.ToString("000.000");
        _currentDistance = Mathf.Abs(_correctValue - _currentValue);

        if (_valueText != null)
        {
            _valueText.text = _correctValue.ToString("000.000"); // New
        }
        */
    }

    public void SetValue()
    {
        _correctValue = Random.Range(GetComponent<Slider>().minValue + _fadingDistance, GetComponent<Slider>().maxValue); // You plus _fadingDistance to minValue here, but you can remove it if you ever make it so that the initial value of the slider is randomzied as well.
        _correctValue = Mathf.Ceil(_correctValue * 1000f) / 1000f;
        _displayedText.text = _currentValue.ToString("000.000");
        _currentDistance = Mathf.Abs(_correctValue - _currentValue);

        if (_valueText != null)
        {
            _valueText.text = _correctValue.ToString("000.000"); // New
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnValueChanged(float newValue)
    {
        _currentValue = newValue;
        _displayedText.text = _currentValue.ToString("000.000");
        _currentDistance = Mathf.Abs(_correctValue - _currentValue);
    }

    public void ValueAdded(float additive)
    {
        _currentValue = Mathf.Ceil(_currentValue * 1000f) / 1000f;
        _currentValue += additive;
        GetComponent<Slider>().value = _currentValue;
        _displayedText.text = _currentValue.ToString("000.000");
        _currentDistance = Mathf.Abs(_correctValue - _currentValue);
    }
}
