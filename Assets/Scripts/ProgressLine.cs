using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressLine : MonoBehaviour
{
    [SerializeField] private Image image;

    public void SetLit(bool lit)
    {
        image.color = lit ? Color.white : Color.black; // Originally image.color = lit ? Color.white : Color.gray
    }
}
