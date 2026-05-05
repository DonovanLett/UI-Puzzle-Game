using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Windows;

public class PinPadPuzzleManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _pinPad;

    [SerializeField]
    private string _requiredPin;

    [SerializeField]
    private bool _pinBeingAdded;

    [SerializeField]
    private RadioPuzzleManager _radioPuzzleManager;

    // Start is called before the first frame update
    void Start()
    {
        RandomizePin();

        _pinPad.text = "Enter PIN";
        _pinBeingAdded = false;
    }

    public void AddPinNumber(int number)
    {
        if (!_pinBeingAdded)
        {
            _pinPad.text = "";
            _pinBeingAdded = true;
        }
        _pinPad.text += number.ToString();
    }

    public void EnterPin()
    {
        if (_pinBeingAdded)
        {
            if (_pinPad.text == _requiredPin.ToString())
            {
                _pinPad.text = "PIN Accepted";
                EventSystem.current.enabled = false; // Disables the Players ability to interact with UI
            }
            else
            {
                _pinPad.text = "Invalid PIN";
            }
            _pinBeingAdded = false;
        }
    }

    public void ClearPin()
    {
        if (_pinBeingAdded)
        {
            _pinPad.text = "Enter PIN";
            _pinBeingAdded = false;
        }
    }


    public void RandomizePin()
    {
        int pin = Random.Range(0, 10000);
        _requiredPin = pin.ToString("0000");
        SetRadioSequence(1.5f, 0.7f);
    }

    public void SetRadioSequence(float startPoint, float gap)
    {
        // Radio Number Sequence Code
        float time = startPoint;
        foreach (char ch in _requiredPin)
        {
            if (int.TryParse(ch.ToString(), out int result))
            {
               // Debug.Log("Valid int: " + result);
                _radioPuzzleManager.NumberSequenceTimelineSet(result, time);
                time += gap;

            }
            else
            {
              //  Debug.Log("Not a valid int");
            }
            Debug.Log(ch);
        }
    }
}