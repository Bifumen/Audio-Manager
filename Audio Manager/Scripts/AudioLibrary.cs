using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioPoolingSystem;

public class AudioLibrary : MonoBehaviour {

    public Sound[] sounds;

    [SerializeField] [HideInInspector] private PlayOnStart playOnStart = PlayOnStart.None;
    [SerializeField] [HideInInspector] private PlayType nameOrIndex = PlayType.Name;

    [SerializeField] [HideInInspector] private string awakeSoundName = "";
    [SerializeField] [HideInInspector] private int awakeSoundIndex = 0;

    public enum PlayType { Name, Index, PlayAll }
    public enum PlayOnStart { None, PlayOnStart, PlayOnEnable }

    [Space]
    [Tooltip("If true, when the gameobject is disabled, stops all sounds with the stopOnDisable property checked")]
    public bool stopOnSoundsDisable = false;

    void Start()
    {
        if (playOnStart == PlayOnStart.PlayOnStart)
        {
            if (nameOrIndex == PlayType.Name)
                PlaySound(awakeSoundName);
            else if (nameOrIndex == PlayType.Index)
                PlaySoundByIndex(awakeSoundIndex);
            else
                PlayAll();
        }
    }

    void OnEnable()
    {
        if (playOnStart == PlayOnStart.PlayOnEnable)
        {
            StartCoroutine(Startup());
        }
    }

    void OnDisable()
    {
        if(stopOnSoundsDisable)
        {
            foreach(Sound s in sounds)
            {
                if (s.stopOnDisable)
                    AudioManager.Instance.StopSound(s);
            }
        }
    }

    private IEnumerator Startup()
    {
        yield return null;

        if (nameOrIndex == PlayType.Name)
            PlaySound(awakeSoundName);
        else if (nameOrIndex == PlayType.Index)
            PlaySoundByIndex(awakeSoundIndex);
        else
            PlayAll();
    }

    #region Play
    public void PlaySound(string name)
    {
        Sound s = Array.Find(sounds, snd => snd.name == name);

        if (s == null)
        {
            Debug.Log("Error: audio clip not found!");
            return;
        }

        AudioManager.Instance.PlaySound2D(s);
    }

    public void PlaySoundByIndex(int index)
    {
        AudioManager.Instance.PlaySound2D(sounds[index]);
    }

    public void PlayAll()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            PlaySoundByIndex(i);
        }
    }

    public void PlayAll(int n)
    {
        for (int i = 0; i < n; i++)
        {
            PlaySoundByIndex(i);
        }
    }
    #endregion

    #region Stop
    public void StopSound(string name)
    {
        Sound s = Array.Find(sounds, snd => snd.name == name);

        if (s == null)
        {
            Debug.Log("Error: audio clip not found!");
            return;
        }

        AudioManager.Instance.StopSound(s);
    }

    public void StopSoundLimited(string name, int n)//stops n number of sounds
    {
        Sound s = Array.Find(sounds, snd => snd.name == name);

        if (s == null)
        {
            Debug.Log("Error: audio clip not found!");
            return;
        }

        AudioManager.Instance.StopSound(s, n);
    }

    public void StopSoundByIndex(int index)
    {
        AudioManager.Instance.StopSound(sounds[index]);
    }

    public void StopSoundByIndexLimited(int index, int n)//stops n number of sounds
    {
        AudioManager.Instance.StopSound(sounds[index], n);
    }

    public void StopAllSounds()
    {
        foreach(Sound s in sounds)
        {
            AudioManager.Instance.StopSound(s);
        }
    }

    #endregion

    #region Get

    public Sound GetSound(string name)
    {
        Sound s = null;

        s = Array.Find(sounds, snd => snd.name == name);

        return s;
    }

    public int? GetSoundIndex(string name)
    {
        int? index = null;

        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == name)
            {
                index = i;
                break;
            }
        }

        return index;
    }

    public bool IsPlaying(string name)
    {
        return AudioManager.Instance.IsPlaying(GetSound(name));
    }

    public bool IsPlaying(int index)
    {
        return AudioManager.Instance.IsPlaying(sounds[index]);
    }

    #endregion
}
