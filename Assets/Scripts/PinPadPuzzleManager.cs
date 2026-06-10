using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;
using UnityEngine.UI;
using UnityEngine.Windows;

public class PinPadPuzzleManager : MonoBehaviour, IResetScript
{
    [SerializeField]
    private TMP_Text _pinPad;

    [SerializeField]
    private RectTransform _pinPadSpace;

    [SerializeField]
    private string _requiredPin;

    [SerializeField]
    private bool _pinBeingAdded, _incorrectSignFlashing;

    [SerializeField]
    private RadioPuzzleManager _radioPuzzleManager;

    [Header("Audio")]
    [SerializeField]
    private AudioSource _beep;

    [SerializeField]
    private AudioSource _music;

    [SerializeField]
    private PlayableDirector _endingTimeline, _incorrectTimeline;

    [SerializeField]
    private Timer _timer;

    // Start is called before the first frame update
    void Start()
    {
        RandomizePin();
        _beep = GetComponent<AudioSource>();
        _pinPad.text = "Enter PIN";
        _pinBeingAdded = false;
    }

    public void Reset()
    {
        RandomizePin();
        _beep = GetComponent<AudioSource>(); // Maybe not necessary
        _pinPad.text = "Enter PIN";
        _pinBeingAdded = false;
    }

    public void AddPinNumber(int number)
    {
        EventSystem.current.SetSelectedGameObject(null);
        if (_incorrectSignFlashing == false)
        {
            if (!_pinBeingAdded)
            {
                _pinPad.text = "";
                _pinBeingAdded = true;
            }
            string proposedText = _pinPad.text + number;

            // Calculate preferred size of the new text
            Vector2 preferredSize = _pinPad.GetPreferredValues(proposedText);

            float maxWidth = _pinPadSpace.rect.width;
            float maxHeight = _pinPadSpace.rect.height;

            // Only apply if it fits inside the RectTransform
            if (preferredSize.x <= maxWidth &&
                preferredSize.y <= maxHeight)
            {
                _pinPad.text = proposedText;
            }
            _beep.PlayOneShot(_beep.clip);
        }
    }

    /*
    public void AddPinNumber(int number)
    {
        if (!_pinBeingAdded)
        {
            _pinPad.text = "";
            _pinBeingAdded = true;
        }
        _pinPad.text += number.ToString();
    }
    */

    public void EnterPin()
    {
        EventSystem.current.SetSelectedGameObject(null);
        //_beep.PlayOneShot(_beep.clip);
        if (_pinBeingAdded && _incorrectSignFlashing == false)
        {
            if (_pinPad.text == _requiredPin.ToString())
            {
                _timer.StopTimer(); // Make this code when you're ready
                _radioPuzzleManager.WipeTimeline(); // Maybe instead call this right before the radio is filled. !!!!!!!!!
                _endingTimeline.Play();
                _music.Stop();
                _pinPad.text = "PIN Accepted";
                EventSystem.current.enabled = false; // Disables the Players ability to interact with UI
            }
            else
            {
                _incorrectSignFlashing = true;
                _incorrectTimeline.Stop();
                _incorrectTimeline.Play();
                _pinPad.text = "Invalid PIN";
            }
            _pinBeingAdded = false;
        }
        else
        {
            _beep.PlayOneShot(_beep.clip);
        }
    }

    public void ClearPin()
    {
        EventSystem.current.SetSelectedGameObject(null);
        _beep.PlayOneShot(_beep.clip);
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

    public void IncorrectSignFinished()
    {
        _incorrectSignFlashing = false;
    }
}