using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//using UnityEngine.UIElements;
using static Unity.VisualScripting.Member;
using static UnityEngine.GraphicsBuffer;

public class PuzzlePiece : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Image _image;

    private RectTransform _rectTransform;

    [SerializeField]
    public Vector3 _defaultPosition;

    [SerializeField]
    private PuzzleSlot _currentSlot;

    [SerializeField]
    private int _slotNumber;

    [SerializeField]
    private PuzzleSlot _requiredSlot;

    [SerializeField]
    public bool _isCorrect;

    [SerializeField]
    private bool _canRotate;

    [SerializeField]
    private RectTransform _holderSheet, _challengeSpace;

    // Start is called before the first frame update
    void Start()
    {
        _image = GetComponent<Image>();
        _rectTransform = GetComponent<RectTransform>();
        _holderSheet = transform.parent.GetComponent<RectTransform>(); ;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _canRotate == true)
        {
            _rectTransform.Rotate(0f, 0f, -90.0f);
            CheckPuzzlePiece();
        } 
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(_currentSlot != null)
        {
            _currentSlot.OnPuzzlePieceRemoved();
            _currentSlot = null;

            if(_isCorrect == true)
            {
                _isCorrect = false;
            }
        }
        else if (transform.parent == _holderSheet.transform)
        {
            transform.SetParent(_challengeSpace.GetComponent<RectTransform>(), true);
        }

        Vector3 pos = _rectTransform.localPosition;
        pos.z = 5f;
        _rectTransform.localPosition = pos;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        _rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        
        Vector2 localPoint;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _rectTransform.parent as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out localPoint
        );

        _rectTransform.anchoredPosition = localPoint;
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        SearchForSlot(eventData);
    }

    public void ResetPosition()
    {
        transform.SetParent(_holderSheet.GetComponent<RectTransform>(), false);  // transform.SetParent(_holderSheet.transform, false); originally
        Vector3 pos = _rectTransform.localPosition;
        pos.z = 5f;
        _rectTransform.localPosition = pos;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _canRotate = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _canRotate = false;
    }

    public void SearchForSlot(PointerEventData eventData)
    {
        List<RaycastResult> results = new List<RaycastResult>();

        List<PuzzleSlot> slots = new List<PuzzleSlot>();

        EventSystem.current.RaycastAll(eventData, results);

        foreach (RaycastResult result in results)
        {
            if(result.gameObject.GetComponent<PuzzleSlot>() != null && result.gameObject.GetComponent<PuzzleSlot>().isOccupied == false)
            {
                slots.Add(result.gameObject.GetComponent<PuzzleSlot>());
            }
        }

        if (slots.Count > 0)
        {
            PuzzleSlot closestSlot = null;
            float minDistance = Mathf.Infinity;

            Vector3 currentPos = _image.rectTransform.position;

            foreach (PuzzleSlot slot in slots)
            {
                float distance = Vector3.Distance(currentPos, slot.transform.position);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestSlot = slot;
                }
            }
            SetCurrentSlot(closestSlot);
        }
        else
        {
            ResetPosition();
        }
    }

    public void SetCurrentSlot(PuzzleSlot slot)
    {
        _currentSlot = slot;
        _image.rectTransform.position = _currentSlot.GetComponent<RectTransform>().position; // Maybe delete this in a second
        _currentSlot.OnPuzzlePieceAdded();
        CheckPuzzlePiece();
    }

    public void CheckPuzzlePiece()
    {
        if (_currentSlot != null && _currentSlot == _requiredSlot && Quaternion.Angle(_image.rectTransform.localRotation, Quaternion.identity) < 0.01f) ///// _image.rectTransform.localRotation == Quaternion.Euler(0f, 0f, 0f)
        {
            _isCorrect = true;
        }
        else
        {
            _isCorrect = false;
        }
    }
}
