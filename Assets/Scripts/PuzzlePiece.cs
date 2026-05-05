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

    /*
    [SerializeField]
    private Transform _holderSheet;
    */

    [SerializeField]
    private RectTransform _holderSheet, _challengeSpace;

    // Start is called before the first frame update
    void Start()
    {
        _image = GetComponent<Image>();
        _rectTransform = GetComponent<RectTransform>();
        //_holderSheet = transform.parent; // Make sure this is right!!!!!!
        _holderSheet = transform.parent.GetComponent<RectTransform>(); ;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _canRotate == true)
        {
            // _image.rectTransform.localRotation = Quaternion.identity; // Fix this!!!!!!!!!!!!
            // _image.rectTransform.Rotate(0, 0, 90); // Make sure this is right!!!!!
            //transform.rotation = Quaternion.Euler(0f, 0f, 90f); // Correct This
            //_image.GetComponent<RectTransform>().Rotate(0f, 0f, -90.0f);
            _rectTransform.Rotate(0f, 0f, -90.0f);
            CheckPuzzlePiece();
        } 
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
       // _image.raycastTarget = false;

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
            // transform.SetParent(_holderSheet.GetComponent<RectTransform>(), false);
            // transform.SetParent(null); // Change this so that it still exists inside the Canvas

            // transform.parent = null;
        }

        Vector3 pos = _rectTransform.localPosition;
        pos.z = 5f;
        _rectTransform.localPosition = pos;

        /*
        else if (transform.parent == _holderSheet.transform)
        {
            transform.SetParent(null); // Switch toe rectTransform
            //transform.parent = null;
        }
        */
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        _rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        //_rectTransform.pivot = new Vector2(0.5f, 0.5f);
        //RectTransform Pivot = (0.5, 0.5);
        Vector2 localPoint;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _rectTransform.parent as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out localPoint
        );

        _rectTransform.anchoredPosition = localPoint;

        /*
        Vector2 localPoint;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _rectTransform.parent as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out localPoint
        );
        _rectTransform.anchoredPosition = localPoint;
        */
        //transform.position = eventData.position;

        // _rectTransform.anchoredPosition = new Vector3(eventData.position.x, eventData.position.y, 0.0f);
    }


    public void OnEndDrag(PointerEventData eventData)
    {
       // _image.raycastTarget = false;
        SearchForSlot(eventData);
    }

    /*
    public void OnEndDrag(PointerEventData eventData)
    {
        _image.raycastTarget = false;

        if (_currentSlot == null)
        {
            ResetPosition();
        }
        //CheckWhereToSnapTo();
    }

    /*
    public void CheckWhereToSnapTo()
    {
        if(_currentSlot != null)
        {
            _image.rectTransform.localPosition = _currentSlot.transform.position;
        }
        else
        {
            ResetPosition();
            // Snap back to the Sprite Sheet
        }
    }
    */
    /*

    public void ResetPosition()
    {
        _image.rectTransform.localPosition = _defaultPosition;
    }
    */

    public void ResetPosition()
    {
        transform.SetParent(_holderSheet.GetComponent<RectTransform>(), false);  // transform.SetParent(_holderSheet.transform, false); originally
        Vector3 pos = _rectTransform.localPosition;
        pos.z = 5f;
        _rectTransform.localPosition = pos;
        // _image.rectTransform.SetParent(_holderSheet.transform, false);
        // transform.parent = _holderSheet.transform; // Make sure this is right!!!!!!
        // Set its Parent to be the Layer Component on the Right
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
       // _image.raycastTarget = false;

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
        // transform.SetParent(_currentSlot.GetComponent<RectTransform>(), false);
        _image.rectTransform.position = _currentSlot.GetComponent<RectTransform>().position; // Maybe delete this in a second

        /*
        //_image.rectTransform.sizeDelta = _currentSlot.GetComponent<RectTransform>().sizeDelta;
        if (_image.rectTransform != null && _currentSlot.GetComponent<RectTransform>() != null)
        {
            _image.rectTransform.SetSizeWithCurrentAnchors(
                RectTransform.Axis.Horizontal,
                _currentSlot.GetComponent<RectTransform>().rect.width
            );

            _image.rectTransform.SetSizeWithCurrentAnchors(
                RectTransform.Axis.Vertical,
                _currentSlot.GetComponent<RectTransform>().rect.height
            );
        }
        */


        _currentSlot.OnPuzzlePieceAdded();
        CheckPuzzlePiece();
    }

    public void CheckPuzzlePiece()
    {
        if (_currentSlot != null && _currentSlot == _requiredSlot && Quaternion.Angle(_image.rectTransform.localRotation, Quaternion.identity) < 0.01f) ///// _image.rectTransform.localRotation == Quaternion.Euler(0f, 0f, 0f)
        {
            _isCorrect = true;
            /*
            Debug.Log(name + " is in correct spot.");
            Debug.Log("Rotation is On. " + _image.rectTransform.localRotation);
            */
        }
        else
        {
            _isCorrect = false;
            /*
            Debug.Log(name + " is in incorrect spot.");
            if(_currentSlot == null)
            {
                Debug.Log("Slot is Null");
            }
            else if(_currentSlot != _requiredSlot)
            {
                Debug.Log("Slot is Incorrect");
            }
            else if(_image.rectTransform.localRotation != Quaternion.Euler(0f, 0f, 0f))
            {
                Debug.Log("Rotation is Off. " + _image.rectTransform.localRotation);
            }
            */
        }
    }
}
