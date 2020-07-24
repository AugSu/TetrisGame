using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager
{
    private const string AUDIO_PATH = "Audio/";
    private AudioSource audioSource;

    public void Awake()
    {
        audioSource = GameFacade.Instance.audioSource;
    }

    /// <summary>
    /// block下落的声音
    /// </summary>
    public void PlayDropClip()
    {
        PlayClip("Drop");
    }

    public void PlayMoveClip()
    {
        PlayClip("Balloon_003");
    }

    public void PlayClearClip()
    {
        PlayClip("LineClear");
    }

    public void PlayCursorClip()
    {
        PlayClip("Cursor_002");
    }

    private void PlayClip(string clipName)
    {
        AudioClip clip = Resources.Load<AudioClip>(AUDIO_PATH +clipName);
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.Log("路径不对");
        }
    }


}
