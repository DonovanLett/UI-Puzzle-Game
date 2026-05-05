using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PuzzleSlot : MonoBehaviour
{
    [SerializeField]
    private Image _thisImage;

    [SerializeField]
    public bool isOccupied;

    // Start is called before the first frame update
    void Start()
    {
        _thisImage = GetComponent<Image>();
    }


    public void OnDrop(PointerEventData eventData)
    {
        /*
        if (eventData.pointerDrag.GetComponent<PuzzlePiece>() != null && isOccupied == false)
        {
            eventData.pointerDrag.GetComponent<PuzzlePiece>().SetCurrentSlot(this);
            isOccupied = true;
        }
        */
    }

    public void OnPuzzlePieceAdded()
    {
        isOccupied = true;
    }

    public void OnPuzzlePieceRemoved()
    {
        isOccupied = false;
    }
}