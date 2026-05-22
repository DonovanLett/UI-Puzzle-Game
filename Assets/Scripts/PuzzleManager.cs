using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField]
    private PuzzlePiece[] _piece;

    [SerializeField]
    private Button _enterButton;

    [SerializeField]
    private Transform _holderSheet;

    [SerializeField]
    private GameObject _radioValueDisplay; /// NEW

    [SerializeField]
    private ValueDropDown _radioValueDropDown;

    [SerializeField]
    private PlayableDirector _completeTimeline;

    [SerializeField]
    private Timer _timer;

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
        // _timer.StopTimer(); // Make this code when you're ready
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
