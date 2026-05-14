using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VolumeColorController : MonoBehaviour
{
    [SerializeField] private Volume volume;

    private ColorAdjustments colorAdjustments;

    private void Awake()
    {
        volume.profile.TryGet(out colorAdjustments);
    }

    public void SetColorFilter(Color color)
    {
        colorAdjustments.colorFilter.value = color;
    }
}