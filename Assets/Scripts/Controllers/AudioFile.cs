//////////////////////////////////////////////////////
// Assignment/Lab/Project: SGD212-MajorProject3
//Name: Julian Davis
//Section: 2020SP.SGD.212.4142
//Instructor: George Cox
// Date: 04/01/2020
//////////////////////////////////////////////////////
using UnityEditor;
using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class AudioFile
{
    public string audioName;

    public AudioClip audioClip;

    [Range(0f, 1f)]
    public float volume;

    [HideInInspector]
    public AudioSource source;

    [HideInInspector]
    public AudioMixerGroup[] output;

    [HideInInspector]
    public bool isLooping;

    public bool playOnAwake;

}