using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour, IResetScript
{
    [SerializeField]
    private AudioSource _musicSource;

    public void StartMusic()
    {
        _musicSource.Play();
    }

    public void Reset()
    {
        _musicSource.Play();
    }

    public void StopMusic()
    {
        _musicSource.Stop();
    }
}
