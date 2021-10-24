//////////////////////////////////////////////////////
// Assignment/Lab/Project: SGD285-JumpyStreet
// Name: Julian Davis
// Section: 2021FA.SGD.285
// Instructor: Aurore Wold
// Date: 10/25/2021
//////////////////////////////////////////////////////
using UnityEditor;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.Audio;

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

    [HideInInspector]
    public bool playOnAwake;

}