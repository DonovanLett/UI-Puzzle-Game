using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DropPosition : MonoBehaviour, IDropHandler
{
    [SerializeField]
    private Image _thisImage;

    // Start is called before the first frame update
    void Start()
    {
        _thisImage = GetComponent<Image>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag.name == "this")
        {
            eventData.pointerDrag.GetComponent<Draggable>()._defaultPosition = _thisImage.rectTransform.position;
        }
    }
}
