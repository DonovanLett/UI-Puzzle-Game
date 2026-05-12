using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables; // Number Sequence Code
using UnityEngine.Timeline; // Number Sequence Code

public class RadioPuzzleManager : MonoBehaviour
{
    [SerializeField]
    private RadioSlider[] _slider;

    [SerializeField]
    private RadioSequence[] _sequence;

    
    [SerializeField]
    private float _overallMessageClarity; // Change this variable to say the opposite of what it says right now; the higher it is, the lower the sound of static

    [SerializeField]
    private float _overlappingStaticIntensity; // If you want this to have no affect, make sure it is set to 1, not 0

    public AudioSource voiceSource;
    public AudioSource staticSource;
    public AudioLowPassFilter voiceLPF;
    public AudioHighPassFilter staticHPF;


    [SerializeField]
    private AudioClip[] _sequenceNumbers; // Number Sequence Code

    [SerializeField]
    private AudioClip[] _numberSequence;

    [SerializeField]
    private PlayableDirector director; // Number Sequence Code

    void Start()
    {
        foreach(var slider in _slider)
        {
            slider.SetValue();
        }

        // Shuffle sequence
        Shuffle(_sequence); // Display

        ///Display
        //
        for(int i = 0; i < _sequence.Length; i++) 
        {
            _sequence[i].SetValue(_slider[i]);
        }
        //
    }

    void Update() // AI
    {
        CombineSliderValues();
        ConvertSliderValuesToSound();
    }

    public void CombineSliderValues()
    {
        float overall = 0f;
        foreach(RadioSlider slider in _slider)
        {
            float currentDistance = slider._currentDistance;
            float fadingDistance = slider._fadingDistance;
            float intensity;
            if (currentDistance < fadingDistance)
            {
                intensity = ((fadingDistance - currentDistance) / fadingDistance);
            }
            else
            {
                intensity = 0f;
            }
            overall += intensity;
        }
        _overallMessageClarity = overall / (_slider.Length * _overlappingStaticIntensity); // If you want there to be no overlapping static, and for the final message to have no static at all, get rid of multiplying _slider.Length by _overlappingStaticIntensity; have it just be _slider.Length
    }

    public void ConvertSliderValuesToSound()
    {
        float t = Mathf.SmoothStep(0f, 1f, _overallMessageClarity); // "

        // Crossfade
        voiceSource.volume = t;
        staticSource.volume = 1f - t;

        // Voice clarity improves
        voiceLPF.cutoffFrequency = Mathf.Lerp(500f, 22000f, t);

        // Static gets filtered out
        staticHPF.cutoffFrequency = Mathf.Lerp(10f, 2000f, t);

        // Optional realism
        voiceSource.pitch = Mathf.Lerp(0.97f, 1f, t);
    }



    public void NumberSequenceTimelineSet(int i, float time) // Number Sequence Code; things you add to this persist past runtime; this might be a problem.
    {
        AudioClip clip = _sequenceNumbers[i];

        var timeline = director.playableAsset as TimelineAsset;

        // Find an existing AudioTrack (recommended)
        AudioTrack audioTrack = null;

        foreach (var track in timeline.GetOutputTracks())
        {
            if (track is AudioTrack at)
            {
                audioTrack = at;
                break;
            }
        }

        if (audioTrack == null)
        {
            Debug.LogError("No AudioTrack found in Timeline!");
            return;
        }

        // Create clip
        var timelineClip = audioTrack.CreateClip<AudioPlayableAsset>();

        timelineClip.start = time;
        timelineClip.duration = clip.length;

        var audioAsset = timelineClip.asset as AudioPlayableAsset;
        audioAsset.clip = clip;

        // IMPORTANT: rebuild so Timeline registers changes
        director.RebuildGraph(); // This might be what is causing you adding AudioClips to the Number Sequence to save past runtime; do more research into it.
    }



    private void OnApplicationQuit()
    {
        var timeline = director.playableAsset as TimelineAsset;

        // Find an existing AudioTrack (recommended)
        AudioTrack audioTrack = null;

        foreach (var track in timeline.GetOutputTracks())
        {
            if (track is AudioTrack at)
            {
                audioTrack = at;
                break;
            }
        }

        if (audioTrack == null)
        {
            Debug.LogError("No AudioTrack found in Timeline!");
            return;
        }

        // Clear Number Sequence Timeline
        // Option 1: Delete each clip
        foreach (var clip in audioTrack.GetClips())
        {
            audioTrack.DeleteClip(clip);
        }
        director.RebuildGraph(); // Maybe essential, maybe not
    }



    public void Shuffle<T>(T[] array) /// Display
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = UnityEngine.Random.Range(0, i + 1);

            // Swap elements
            T temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }
}
