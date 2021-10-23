using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[System.Serializable]

public class Soundmanager : MonoBehaviour
{
    #region VARIABLES

#pragma warning disable 0649
    public AudioFile[] soundFiles;
    public AudioClip[] soundList;
    public AudioMixerGroup[] output;

#pragma warning restore 0649
    private AudioSource source;

    #endregion
    private void Awake()
    {
        foreach (var s in soundFiles)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.volume = s.volume;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    #region METHODS

    // Update is called once per frame
    void Update()
    {
        
    }

    #endregion
}