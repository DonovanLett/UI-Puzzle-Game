using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
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

    // Start is called before the first frame update
    void Start()
    {
        _decryptedAnswer = _possibleStrings[Random.Range(0, _possibleStrings.Length)];
        _encryptedAnswer = AtbashCipher(_decryptedAnswer);

        _encryptedTextBox.text = _encryptedAnswer;
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


    public void OnEnterButtonPressed()
    {
        if (_decryptedTextBox.text == _decryptedAnswer)
        {
            Debug.Log("Correct");
            _decryptedTextBox.readOnly = true; 
            _enterButton.interactable = false;
            _radioValueDisplay.SetActive(true);
        }
        else
        {
            Debug.Log("Incorrect");
        }
    }
}