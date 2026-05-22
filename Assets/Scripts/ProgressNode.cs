using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressNode : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Color litColor;
    [SerializeField] private Color unlitColor;

    public void SetLit(bool lit)
    {
        image.color = lit ? litColor : unlitColor;
    }
}
