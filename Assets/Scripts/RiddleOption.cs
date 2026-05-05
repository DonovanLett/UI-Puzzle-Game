using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RiddleOption : MonoBehaviour
{
    [SerializeField]
    public string answer;

    [SerializeField]
    public TMP_Text _label;

    public void SetAnswer(string answer)
    {
        this.answer = answer;
        _label.text = this.answer;
    }
}
