using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SequencesDropDown : MonoBehaviour
{
    [SerializeField]
    private Button _dropDownButton;
    [SerializeField]
    private GameObject _dropDown;


    [SerializeField]
    private GameObject[] _covers;
    [SerializeField]
    private List<GameObject> _currentCovers;

    [SerializeField]
    private int _sequencesRevealedPerPuzzleSolved;
    // Start is called before the first frame update
    void Start()
    {
        _currentCovers.Clear();
        _currentCovers.AddRange(_covers);
        Shuffle(_currentCovers);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPuzzleSolved()
    {
        for(int i = 0; i <= _sequencesRevealedPerPuzzleSolved - 1; i++)
        {
            _currentCovers[i].SetActive(false);
        }
    }

    void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = UnityEngine.Random.Range(i, list.Count);
            (list[i], list[randomIndex]) = (list[randomIndex], list[i]);
        }
    }

    public void DropDown()
    {

    }

    public void RiseUp()
    {

    }
}
