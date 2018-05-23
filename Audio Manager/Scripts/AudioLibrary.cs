using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioPoolingSystem;

public class AudioLibrary : MonoBehaviour {

    public Sound[] sounds;

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

    public void PlaySound(int index)
    {
        AudioManager.Instance.PlaySound2D(sounds[index]);
    }

    public void PlayAll()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            PlaySound(i);
        }
    }

    public void PlayAll(int n)
    {
        for (int i = 0; i < n; i++)
        {
            PlaySound(i);
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

    public void StopSound(string name, int n)
    {
        Sound s = Array.Find(sounds, snd => snd.name == name);

        if (s == null)
        {
            Debug.Log("Error: audio clip not found!");
            return;
        }

        AudioManager.Instance.StopSound(s, n);
    }

    public void StopSound(int index)
    {
        AudioManager.Instance.StopSound(sounds[index]);
    }

    public void StopSound(int index, int n)
    {
        AudioManager.Instance.StopSound(sounds[index], n);
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

    #endregion
}
