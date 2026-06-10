using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector _narrationCutscene;

    [SerializeField]
    private PlayableDirector _openingCutscene;

    [SerializeField]
    private PlayableDirector _replayNarrationCutscene;

    [SerializeField]
    private EventSystem _eventSystem;

    [SerializeField]
    private bool _isOnOpeningText;

    [SerializeField]
    private GameObject[] _scriptsToReset;

    private void Update()
    {
        if (Input.anyKeyDown && _isOnOpeningText)
        {
            _isOnOpeningText = false;
            _narrationCutscene.Stop();
            _openingCutscene.Play();
        }
    }

    public void OnStartButtonPressed()
    {
        _narrationCutscene.Play();
    }

    public void EnableEventSystem()
    {
        /*
        if (EventSystem.current != null)
        {
            EventSystem.current.enabled = true;
        }
        else
        {
            Debug.LogWarning("No active EventSystem found.");
        }
        */
        _eventSystem.enabled = true;
    }

    public void DisableEventSystem()
    {
        /*
        if (EventSystem.current != null)
        {
            EventSystem.current.enabled = false;
        }
        else
        {
            Debug.LogWarning("No active EventSystem found.");
        }
        */
        _eventSystem.enabled = false;
    }

    public void OnQuit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void Replay()
    {
        foreach(var obj in _scriptsToReset) // originally foreach(var script in _scriptsToReset)
        {
            IResetScript script = obj.GetComponent<IResetScript>();
            if(script != null)
            {
                script.Reset();
            }


            //script.Reset();
        }

        _replayNarrationCutscene.Play();
        Debug.Log("Replay Cutscene Should be Triggered");
    }

    public void OpeningTextSkippable()
    {
        _isOnOpeningText = true;
    }
}
