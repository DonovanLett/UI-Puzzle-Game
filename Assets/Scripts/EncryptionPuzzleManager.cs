using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;
using UnityEngine.UI;
using UnityEngine.Windows;

public class EncryptionPuzzleManager : MonoBehaviour
{
    [SerializeField]
    private string _decryptedAnswer, _encryptedAnswer;

    [SerializeField]
    private TMP_InputField _encryptedTextBox, _decryptedTextBox;

    [SerializeField]
    private Button _enterButton;

    [SerializeField]
    private string[] _possibleStrings;

    [SerializeField]
    private GameObject _radioValueDisplay; /// NEW

    [SerializeField]
    private ValueDropDown _radioValueDropDown;

    [SerializeField]
    private PlayableDirector _completeTimeline;

    [SerializeField]
    private Timer _timer;

    [Header("On-Screen Cipher")]
    [SerializeField]
    private CircularTextLayout _cipherCircle;

    [SerializeField]
    private int _shiftAmount;

    // Start is called before the first frame update
    void Start()
    {
        _shiftAmount = UnityEngine.Random.Range(1, 26);

        _decryptedAnswer = _possibleStrings[Random.Range(0, _possibleStrings.Length)];
        // _encryptedAnswer = AtbashCipher(_decryptedAnswer);
        _encryptedAnswer = Encrypt(_decryptedAnswer, _shiftAmount);
        _encryptedTextBox.text = _encryptedAnswer;

        // Circle Cipher Code
        foreach(var letter in _cipherCircle.letters)
        {
            // letter.text = AtbashCipher(letter.text);
            letter.text = Encrypt(letter.text, _shiftAmount);
        }
    }

    private string AtbashCipher(string input)
    {
        StringBuilder result = new StringBuilder();
        foreach (char c in input)
        {
            if (char.IsLetter(c))
            {
                if (char.IsUpper(c))
                {
                    // 'A' = 65, 'Z' = 90
                    char transformed = (char)('Z' - (c - 'A'));
                    result.Append(transformed);
                }
                else
                {
                    // 'a' = 97, 'z' = 122
                    char transformed = (char)('z' - (c - 'a'));
                    result.Append(transformed);
                }
            }
            else
            {
                // Keep non-letters unchanged (spaces, punctuation, etc.)
                result.Append(c);
            }
        }

        return result.ToString();
    }

    private string Encrypt(string input, int shift)
    {
        shift %= 26;

        StringBuilder result = new StringBuilder();

        foreach (char c in input)
        {
            // Uppercase letters
            if (c >= 'A' && c <= 'Z')
            {
                int index = c - 'A';

                int shifted = (index + shift) % 26;

                char encrypted = (char)('A' + shifted);

                result.Append(encrypted);
            }

            // Lowercase letters
            else if (c >= 'a' && c <= 'z')
            {
                int index = c - 'a';

                int shifted = (index + shift) % 26;

                char encrypted = (char)('a' + shifted);

                result.Append(encrypted);
            }

            // Non-letters
            else
            {
                result.Append(c);
            }
        }

        return result.ToString();
    }


    public void OnEnterButtonPressed()
    {
        EventSystem.current.SetSelectedGameObject(null);
        if (_decryptedTextBox.text == _decryptedAnswer)
        {
            Debug.Log("Correct");
            _timer.StopTimer(); // Make this code when you're ready
            _decryptedTextBox.readOnly = true;
            _enterButton.interactable = false;
            _radioValueDropDown.EnableRandomImages(3);
            //_radioValueDisplay.SetActive(true);
            _completeTimeline.Play();
        }
        else
        {
            Debug.Log("Incorrect");
        }
    }
}