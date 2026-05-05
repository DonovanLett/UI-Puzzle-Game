using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Image _image;
    [SerializeField]
    private Image _imageDrop;
    [SerializeField]
    public Vector3 _defaultPosition;

    // Start is called before the first frame update
    void Start()
    {
        _image = GetComponent<Image>();
        _defaultPosition = _image.rectTransform.localPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        ResetPosition();
        _image.raycastTarget = false;
    }

    public void ResetPosition()
    {
        _image.rectTransform.localPosition = _defaultPosition;
    }
}
