using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RiddlePuzzleManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _riddleText;

    /*
    [SerializeField]
    private string[] _riddles, _correctAnswers, _randomAnswers;
    */

    [SerializeField]
    private Riddle[] _possibleRiddles;

    [SerializeField]
    private List<Riddle> _usedRiddles; // Possible switch this to just "_riddles"

    [SerializeField]
    private int _numberOfRiddles, _currentRiddle;

    [SerializeField]
    private string[] _fillerAnswers;

    [SerializeField]
    private List<string> _answers;


    [SerializeField]
    private Button _enterButton;

    [SerializeField]
    private ToggleGroup _toggleGroup;

    [SerializeField]
    private RiddleOption[] _options;

    [SerializeField]
    private Toggle _selectedToggle;

    [SerializeField]
    private GameObject _radioValueDisplay; /// NEW


    // Start is called before the first frame update
    void Start()
    {
        OnStartRound();
    }

    public void OnStartRound()
    {
        _answers.Clear();
        _answers.AddRange(_fillerAnswers);
        // Add _possibleRiddles.answers to string[] answers
        foreach (Riddle r in _possibleRiddles)
        {
            _answers.Add(r.answer);
        }
        // Make sure all Switches are initially off
        SetToggles();
        SetRiddles();
        
        /*
        _currentOptions.Clear();
        _currentOptions.AddRange(_options);
        */

        _currentRiddle = 0;
        _riddleText.text = _usedRiddles[_currentRiddle].riddle;
        SetOptions();
    }

    public void OnNextRound()
    {
        _currentRiddle++;
        _riddleText.text = _usedRiddles[_currentRiddle].riddle;

        SetToggles();
        /*
        _toggleGroup.allowSwitchOff = true;
        _selectedToggle.isOn = false;
        _selectedToggle = null;
        */
        SetOptions();
    }

    public void OnToggleSelected(Toggle toggle)
    {
        _selectedToggle = toggle;

        if(_toggleGroup.allowSwitchOff == true) // This isn't 100% necessary
        {
            _toggleGroup.allowSwitchOff = false;
        }
    }

    public void OnEnterButtonPressed()
    {
        if(_selectedToggle != null)
        {
            if(_selectedToggle.GetComponent<RiddleOption>().answer == _usedRiddles[_currentRiddle].answer)
            {
                
                if (_currentRiddle >= _numberOfRiddles - 1)
                {
                    PuzzleCompleted();
                    Debug.Log("All riddles completed.");
                    // _threeSequences.SetActive(true);
                    // Turn everything to non-interactable
                    return;
                }
                else
                {
                    _answers.Remove(_selectedToggle.GetComponent<RiddleOption>().answer); // Make sure this is right
                    OnNextRound();
                }
            }
            else
            {
                Debug.Log("Wrong");
                OnStartRound();
                // Restart
            }
        }
        else
        {
            Debug.Log("Please select an answer.");
        }
    }

    public void SetRiddles()
    {
        _usedRiddles.Clear();

        for (int i = 0; i < _possibleRiddles.Length; i++)
        {
            int randomIndex = UnityEngine.Random.Range(i, _possibleRiddles.Length);
            Riddle temp = _possibleRiddles[i];
            _possibleRiddles[i] = _possibleRiddles[randomIndex];
            _possibleRiddles[randomIndex] = temp;
        }

        for(int i = 0; i < _numberOfRiddles; i++)
        {
            _usedRiddles.Add(_possibleRiddles[i]);
        }
    }

    public void SetOptions()
    {
        // Step 1: Copy answers
        List<string> pool = new List<string>(_answers);

        string correctAnswer = _usedRiddles[_currentRiddle].answer;

        if (!pool.Contains(correctAnswer))
        {
            Debug.LogError("Correct answer not found in answers list!");
            return;
        }

        // Step 2: Remove correct answer
        pool.Remove(correctAnswer);

        // Step 3: Shuffle remaining answers
        Shuffle(pool);

        // Step 4: Take needed amount (options - 1)
        List<string> selected = new List<string>();
        int needed = _options.Length - 1;

        for (int i = 0; i < needed && i < pool.Count; i++)
        {
            selected.Add(pool[i]);
        }

        // Step 5: Add correct answer
        selected.Add(correctAnswer);

        // Step 6: Shuffle again
        Shuffle(selected);

        //
        if (selected.Count < _options.Length)
        {
            Debug.LogError("Not enough options to fill UI.");
            return;
        }
        //

        // Step 7: Assign to TMP_Texts
        for (int i = 0; i < _options.Length; i++)
        {
            _options[i].SetAnswer(selected[i]);

            // Debug.Log("Option Array's Length: " + _options.Length + ". Current selected: " + i);
            // _options[i].answer = selected[i];
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

    /*
    public void SetOptions() //
    {

        // Shuffle string[] _answers
        for (int i = 0; i < _answers.Count; i++)
        {
            int randomIndex = Random.Range(i, _answers.Count);
            string temp = _answers[i];
            _answers[i] = _answers[randomIndex];
            _answers[randomIndex] = temp;
        }

        // Have one of the Options at random set to the correct answer in _answers[]

        // Finally, do a for loop that does the following for each Riddle Option _options: if _options[i] doesn't already have an answer assigned to it, assign answer[i] to it. (
        for (int i = 0; i < _options.Length; i++) // There's something wrong with this
        {
            //if (i != correctToggleIndex && i != correctAnswerIndex)
           // {
                _options[i].SetAnswer(_answers[i]);
                Debug.Log(_options[i].name + " set to " + _answers[i]);
           // }
        }
    }
    */


    private void SetToggles()
    {
        _toggleGroup.allowSwitchOff = true;
        foreach (RiddleOption option in _options)
        {
            option.GetComponent<Toggle>().isOn = false;
        }
        _selectedToggle = null;
    }

    public void PuzzleCompleted()
    {
        SetToggles();
        foreach (RiddleOption option in _options)
        {
            option.GetComponent<Toggle>().interactable = false;
        }
        _enterButton.interactable = false; // Stop Code

        _radioValueDisplay.SetActive(true); // NEW
    }
}////////////////////////////