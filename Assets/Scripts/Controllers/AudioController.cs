using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

class AudioController : MonoBehaviour
{
    private static AudioController _instance;

    public static AudioController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<AudioController>();
                if (_instance == null)
                {
                    var go = new GameObject("AUDIO CONTROLLER");
                    _instance = go.AddComponent<AudioController>();
                }
            }

            return _instance;
        }
    }

    private AudioSource _audioSource;
    private AudioSource AudioSource
    {
        get
        {
            if (_audioSource == null)
            {
                _audioSource = GetComponent<AudioSource>();
            }
            return _audioSource;
        }
    }

    public AudioClip starter;
    public AudioClip leap;
    public AudioClip squish;
    public AudioClip splash;
    public AudioClip kaChing;
    public AudioClip home;
    public AudioClip endOfRound;

    private int i;

    void Start()
    {
        AudioSource.PlayOneShot(starter);
    }

    public void PlayLeap()
    {
        AudioSource.PlayOneShot(leap);
    }

    public void PlaySquish()
    {
        AudioSource.PlayOneShot(squish);
    }

    public void PlayDrown()
    {
        AudioSource.PlayOneShot(splash);
    }

    public void PlayCollect()
    {
        AudioSource.PlayOneShot(kaChing);
    }

    public void PlayHome()
    {
        AudioSource.PlayOneShot(home);
    }

    public void PlayEndOfRound(Action action)
    {
        StartCoroutine(CRPlayEndOfRound(action));
    }

    private IEnumerator CRPlayEndOfRound(Action action)
    {
        AudioSource.PlayOneShot(endOfRound);
        yield return new WaitForSeconds(endOfRound.length);
        action?.Invoke();
    }
}