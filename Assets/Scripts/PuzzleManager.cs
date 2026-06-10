using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour, IResetScript
{
    [SerializeField]
    private PuzzlePiece[] _piece;

    [SerializeField]
    private Button _enterButton;

    [SerializeField]
    private Transform _holderSheet;

    [SerializeField]
    private RectTransform _content;

    [SerializeField]
    private GameObject _radioValueDisplay; /// NEW

    [SerializeField]
    private ValueDropDown _radioValueDropDown;

    [SerializeField]
    private PlayableDirector _completeTimeline;

    [SerializeField]
    private Timer _timer;

    [SerializeField]
    private Button _returnButton;

    [SerializeField] private ScrollRect _scrollRect;

    [SerializeField]
    private Vector2 _savedPosition;

    [SerializeField]
    private GameObject _area, _puzzleArea;

    // Start is called before the first frame update
    void Start()
    {
        _piece = _holderSheet.GetComponentsInChildren<PuzzlePiece>(); // Make sure this is right
        // Find a way to randomize the order in which they appear in the Holder Sheet, along with each one's rotation

        // Shuffle
        for (int i = 0; i < _piece.Length; i++)
        {
            int randomIndex = UnityEngine.Random.Range(i, _piece.Length);
            PuzzlePiece temp = _piece[i];
            _piece[i] = _piece[randomIndex];
            _piece[randomIndex] = temp;
        }

        // Re-Organize
        for (int i = 0; i < _piece.Length; i++)
        {
            _piece[i].transform.SetSiblingIndex(i);
        }

        // Randomize Rotation
        foreach(PuzzlePiece piece in _piece)
        {
            int rotationIndex = UnityEngine.Random.Range(1, 5);
            piece.transform.Rotate(0, 0, 90 * rotationIndex); // Make sure this is right!!!!!!!!!!!
        }

        // OnRoundStart()
        foreach(PuzzlePiece piece in _piece)
        {
            piece.OnRoundStart();
        }

        _savedPosition = _scrollRect.normalizedPosition;
    }

    public void Reset()
    {
        // Reset all elements in array _piece to children of this GameObject
        foreach(PuzzlePiece piece in _piece)
        {
            piece.Reset();
        }

        // Shuffle
        for (int i = 0; i < _piece.Length; i++)
        {
            int randomIndex = UnityEngine.Random.Range(i, _piece.Length);
            PuzzlePiece temp = _piece[i];
            _piece[i] = _piece[randomIndex];
            _piece[randomIndex] = temp;
        }

        // Re-Organize
        for (int i = 0; i < _piece.Length; i++)
        {
            _piece[i].transform.SetSiblingIndex(i);
        }

        // Randomize Rotation
        foreach (PuzzlePiece piece in _piece)
        {
            int rotationIndex = UnityEngine.Random.Range(1, 5);
            piece.transform.Rotate(0, 0, 90 * rotationIndex); // Make sure this is right!!!!!!!!!!!
        }
        StartCoroutine(ApplySavedPosition());

        foreach (var piece in _piece)
        {
            piece.GetComponent<Image>().raycastTarget = true;
        }
        _enterButton.interactable = true;
        _returnButton.gameObject.SetActive(false);

        // StartCoroutine(ApplySavedPosition());

        /*
        Canvas.ForceUpdateCanvases();
        //LayoutRebuilder.ForceRebuildLayoutImmediate(_content);
        LayoutRebuilder.ForceRebuildLayoutImmediate(_holderSheet.GetComponent<ScrollRect>().content);
        Debug.Log("Content Height: " + _scrollRect.content.rect.height);
        Debug.Log("Viewport Height: " + _scrollRect.viewport.rect.height);
        Debug.Log($"Saved Position: {_savedPosition.y}");
        Debug.Log("Before:");
        Debug.Log("Normalized: " + _scrollRect.verticalNormalizedPosition);
        Debug.Log("Anchored: " + _scrollRect.content.anchoredPosition);

        _scrollRect.verticalNormalizedPosition = _savedPosition.y;

        Debug.Log("After:");
        Debug.Log("Normalized: " + _scrollRect.verticalNormalizedPosition);
        Debug.Log("Anchored: " + _scrollRect.content.anchoredPosition);
        StartCoroutine(ApplySavedPosition());
        /*
        Debug.Log(_savedPosition.y);
        Canvas.ForceUpdateCanvases();
        //LayoutRebuilder.ForceRebuildLayoutImmediate(_content);
        LayoutRebuilder.ForceRebuildLayoutImmediate(_holderSheet.GetComponent<ScrollRect>().content);
        _scrollRect.verticalNormalizedPosition = _savedPosition.y;
        // _scrollRect.normalizedPosition = _savedPosition;
        Canvas.ForceUpdateCanvases();
        Debug.Log(_scrollRect.verticalNormalizedPosition);
        // StartCoroutine(ApplySavedPosition());
        //_scrollRect.normalizedPosition = _savedPosition;
        */
    }

    public void ResetScrollRect()
    {
        // StartCoroutine(ApplySavedPosition());
    }

    IEnumerator ApplySavedPosition()
    {
        // yield return null;
        yield return null;
        _area.SetActive(true);
        _puzzleArea.SetActive(true);
        yield return null;
        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(_scrollRect.content);
        _scrollRect.verticalNormalizedPosition = _savedPosition.y;
        _area.SetActive(false);
        _puzzleArea.SetActive(false);

        /*
        Debug.Log("One frame later:");
        Debug.Log("Normalized: " + _scrollRect.verticalNormalizedPosition);
        Debug.Log("Anchored: " + _scrollRect.content.anchoredPosition);
        */
    }

    public void OnScrollChanged()
    {
        // Debug.Log(_scrollRect.normalizedPosition);
    }

    public void OnEnterButtonPressed()
    {
        EventSystem.current.SetSelectedGameObject(null);
        foreach (var piece in _piece)
        {
            if(piece._isCorrect == false)
            {
                Debug.Log("Fail");
                return;
            }
        }

        Debug.Log("Completed");
        _timer.StopTimer(); // Make this code when you're ready
        _radioValueDropDown.EnableRandomImages(3);
        _completeTimeline.Play();
        //_radioValueDisplay.SetActive(true); // NEW
        _radioValueDisplay.transform.SetAsLastSibling();

        // Stop Code
        foreach(var piece in _piece)
        {
            piece.GetComponent<Image>().raycastTarget = false;
        }
        _enterButton.interactable = false;

    }
}
