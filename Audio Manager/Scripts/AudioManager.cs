using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using AudioPoolingSystem;

public class AudioManager : MonoBehaviour {

    public GameObject pooledObject;
    public int pooledAmount = 2;
    public bool willGrow = true;
    private List<GameObject> pooledObjects;
    private List<AudioSourceElement> pooledObjectsAudioSourceEl;    // Store the Audio Source component reference for fast access

    public static AudioManager Instance
    {
        get;
        private set;
    }

    // Use this for initialization
    void Awake () {
        // First we check if there are any other instances conflicting
        if (Instance != null && Instance != this)
        {
            // If that is the case, we destroy other instances
            Destroy(gameObject);
        }

        // Here we save our singleton instance
        Instance = this;

        InitializePooledObjects();
    }

    #region Play Sound
    public void PlaySound2D(Sound s)
    {
        int? nullableIndex = GetPooledObjectIndex();

        if (nullableIndex == null)
        {
            return;
        }

        int index = (int)nullableIndex;

        pooledObjects[index].SetActive(true);
        pooledObjectsAudioSourceEl[index].audioSource.spatialBlend = 0f;  // 2D
        setProperties(pooledObjectsAudioSourceEl[index].audioSource, s.clip, s.volume, s.pitch, s.loop, s.mixer);
        pooledObjectsAudioSourceEl[index].sound = s;
        pooledObjectsAudioSourceEl[index].audioSource.Play();
    }

    public void PlaySound3D(Sound s, Vector3 position)
    {
        int? nullableIndex = GetPooledObjectIndex();

        if (nullableIndex == null)
        {
            return;
        }

        int index = (int)nullableIndex;

        pooledObjects[index].SetActive(true);
        pooledObjects[index].transform.position = position;

        pooledObjectsAudioSourceEl[index].audioSource.spatialBlend = 1f;  // 3D
        setProperties(pooledObjectsAudioSourceEl[index].audioSource, s.clip, s.volume, s.pitch, s.loop, s.mixer);
        pooledObjectsAudioSourceEl[index].sound = s;
        pooledObjectsAudioSourceEl[index].audioSource.Play();
    }
    #endregion

    #region Stop Sound
    public void StopSound(int index)
    {
        pooledObjectsAudioSourceEl[index].audioSource.Stop();
    }

    public void StopSound(Sound s)
    {
        for (int i = 0; i < pooledObjectsAudioSourceEl.Count; i++)
        {
            if (ReferenceEquals(pooledObjectsAudioSourceEl[i].sound, s))
            {
                pooledObjectsAudioSourceEl[i].audioSource.Stop();
            }
        }
    }

    public void StopSound(Sound s, int n)
    {
        int p = 0;

        for (int i = 0; i < pooledObjectsAudioSourceEl.Count; i++)
        {
            if (ReferenceEquals(pooledObjectsAudioSourceEl[i].sound, s))
            {
                pooledObjectsAudioSourceEl[i].audioSource.Stop();
                p++;
            }
            if (p >= n)
                return;
        }
    }

    #endregion

    #region Pooling
    void InitializePooledObjects()
    {
        pooledObjects = new List<GameObject>();
        pooledObjectsAudioSourceEl = new List<AudioSourceElement>();
        
        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = Instantiate(pooledObject) as GameObject;
            // Add the Audio Source components, for faster access  
            pooledObjectsAudioSourceEl.Add(obj.GetComponent<AudioSourceElement>());
            obj.transform.parent = this.transform;
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    public int? GetPooledObjectIndex()
    {
        // Parse all pooled objects
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            // In case the pooled object is null create an pooled object and return it's index
            // Note: This actually is not going to be used, because we initialize 
            // all the pooled objects on Awake method
            if (pooledObjects[i] == null)
            {
                GameObject obj = Instantiate(pooledObject) as GameObject;
                pooledObjectsAudioSourceEl.Add(obj.GetComponent<AudioSourceElement>());
                obj.transform.parent = this.transform;
                obj.SetActive(false);
                pooledObjects[i] = obj;
                return i;
            }
            // If the object is inactive, return it's index
            if (!pooledObjects[i].activeInHierarchy)
            {
                return i;
            }
        }

        // If the list of the pooledObjects can grow, create a new pooled object and return it's index
        if (willGrow)
        {
            GameObject obj = Instantiate(pooledObject) as GameObject;
            pooledObjectsAudioSourceEl.Add(obj.GetComponent<AudioSourceElement>());
            obj.transform.parent = this.transform;
            pooledObjects.Add(obj);
            obj.SetActive(false);
            return (pooledObjects.Count - 1);
        }

        return null;
    }
    #endregion

    private void setProperties(AudioSource source, AudioClip clip, float volume, float pitch, bool loop, AudioMixerGroup mixer)
    {
        source.clip = clip;
        source.volume = volume;
        source.pitch = pitch;
        source.loop = loop;
        source.outputAudioMixerGroup = mixer;
    }

}
