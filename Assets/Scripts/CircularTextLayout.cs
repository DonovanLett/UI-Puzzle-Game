using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CircularTextLayout : MonoBehaviour
{
    public TMP_Text[] letters;
    public float radius = 200f;

    /*
    void Start() // Possible switch this to on validate
    {
        letters = GetComponentsInChildren<TMP_Text>();
        ArrangeLetters();
    }
    */

    private void OnValidate()
    {
        letters = GetComponentsInChildren<TMP_Text>();
        ArrangeLettersFromTop();
    }

    void ArrangeLetters()
    {
        float angleStep = 360f / letters.Length;

        for (int i = 0; i < letters.Length; i++)
        {
            float angle = angleStep * i;

            float radians = angle * Mathf.Deg2Rad;

            float x = Mathf.Cos(radians) * radius;
            float y = Mathf.Sin(radians) * radius;

            letters[i].rectTransform.anchoredPosition =
                new Vector2(x, y);
        }
    }

    void ArrangeLettersFromTop()
    {
        float angleStep = 360f / letters.Length;

        for (int i = 0; i < letters.Length; i++)
        {
            float angle = 90f - angleStep * i;

            float radians = angle * Mathf.Deg2Rad;

            float x = Mathf.Cos(radians) * radius;
            float y = Mathf.Sin(radians) * radius;

            letters[i].rectTransform.anchoredPosition =
                new Vector2(x, y);
        }
    }


}
