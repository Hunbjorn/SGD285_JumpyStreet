using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
[System.Serializable]

public class AudioManager : MonoBehaviour
{

	#region VARIABLES

#pragma warning disable 0649
	public static AudioManager instance;
	public AudioFile[] audioFiles;
	public AudioClip[] musicList;
	public AudioMixerGroup[] output;
	public Texture playTexture;
	public Texture muteTexture;
	public Texture nextTexture;
	public Texture lastTexture;
	public Texture volUpTexture;
	public Texture volDnTexture;
	public Texture exitTexture;

	//public enum SeekDirection { Forward, Backward }
#pragma warning restore 0649
	private AudioSource source;
	private int currentClip;
	private float timeToReset;
	private float tmpVol;
	private bool timerIsSet = false;
	//private bool isLowered = false;
	private bool fadeOut = false;
	private bool fadeIn = false;
	private string tmpName;
	private string fadeInUsedString;
	private string fadeOutUsedString;


	#endregion

	// Use this for initialization
	void Awake()
	{
		//Create AM instance. If different AM present, destroy it.
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(gameObject);
			return;
		}
		//Persist this instance through level change.
		DontDestroyOnLoad(gameObject);

		//Add features to every audiofile
		foreach (var s in audioFiles)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.volume = s.volume;
		}
	}

	//OnGUI controls for players
	void OnGUI()
	{
		if (GUI.Button(new Rect(20, 50, 50, 50), playTexture))
		{
			PlayList();
            gameObject.GetComponent<AudioSource>().mute = false;
        }

        if(GUI.Button(new Rect(70, 50, 50, 50), muteTexture))
        {
            gameObject.GetComponent<AudioSource>().mute = true;
        }

		if (GUI.Button(new Rect(125, 50, 50, 50), nextTexture))
		{
			NextTitle();
			Debug.Log(currentClip);
		}

		if (GUI.Button(new Rect(175, 50, 50, 50), lastTexture))
		{
			LastTitle();
			Debug.Log(currentClip);
		}

        if (GUI.Button(new Rect(225, 50, 50, 50), volUpTexture))
		{
			RaiseVolume("Track");
		}

		if (GUI.Button(new Rect(275, 50, 50, 50), volDnTexture))
		{
			LowerVolume("Track");
		}

		if (GUI.Button(new Rect(330, 50, 50, 50), exitTexture))
		{
			Application.Quit();
		}

	}

	void Start()
	{
		source = GetComponent<AudioSource>();
		timeToReset = 0f;
		ResumeList();
	}

	#region METHODS

	public static void PlaySound(String name)
	{
		AudioFile s = Array.Find(instance.audioFiles, AudioFile => AudioFile.audioName == name);
		if (s == null)
		{
			Debug.LogError("Sound name" + name + "not found!");
			return;
		}
		else
		{
			s.source.Play();
		}
	}
	public static void LowerVolume(String name)
	{
		AudioFile s = Array.Find(instance.audioFiles, AudioFile => AudioFile.audioName == name);
		if (s == null)
		{
			Debug.LogError("Sound name" + name + "not found!");
			return;
		}
		else
		{
			instance.tmpName = name;
			instance.tmpVol = s.volume;
			s.source.volume = s.source.volume / 2;
		}
	}

	public static void RaiseVolume(String name)
	{
		AudioFile s = Array.Find(instance.audioFiles, AudioFile => AudioFile.audioName == name);
		if (s == null)
		{
			Debug.LogError("Sound name" + name + "not found!");
			return;
		}
		else
		{
			instance.tmpName = name;
			instance.tmpVol = s.volume;
			s.source.volume = s.source.volume * 2;
		}
	}

	public static void StopVolume(String name)
	{
		AudioFile s = Array.Find(instance.audioFiles, AudioFile => AudioFile.audioName == name);
		if (s == null)
		{
			Debug.LogError("Sound name" + name + "not found!");
			return;
		}
		else
		{
			s.source.Stop();
		}
	}


	public static void FadeOut(String name, float duration)
	{
		instance.StartCoroutine(instance.IFadeOut(name, duration));
	}

	public static void FadeIn(String name, float targetVolume, float duration)
	{
		instance.StartCoroutine(instance.IFadeIn(name, targetVolume, duration));
	}

	//not for use

	private IEnumerator IFadeOut(String name, float duration)
	{
		AudioFile s = Array.Find(instance.audioFiles, AudioFile => AudioFile.audioName == name);
		if (s == null)
		{
			Debug.LogError("Sound name" + name + "not found!");
			yield return null;
		}
		else
		{
			if (fadeOut == false)
			{
				fadeOut = true;
				float startVol = s.source.volume;
				fadeOutUsedString = name;
				while (s.source.volume > 0)
				{
					s.source.volume -= startVol * Time.deltaTime / duration;
					yield return null;
				}

				s.source.Stop();
				yield return new WaitForSeconds(duration);
				fadeOut = false;
			}
			else
			{
				Debug.Log("Could not handle two fade outs at once : " + name + " , " + fadeOutUsedString + "! Stopped the music " + name);
				StopVolume(name);
			}
		}
	}

	public IEnumerator IFadeIn(string name, float targetVolume, float duration)
	{
		AudioFile s = Array.Find(instance.audioFiles, AudioFile => AudioFile.audioName == name);
		if (s == null)
		{
			Debug.LogError("Sound name" + name + "not found!");
			yield return null;
		}
		else
		{
			if (fadeIn == false)
			{
				fadeIn = true;
				instance.fadeInUsedString = name;
				s.source.volume = 0f;
				s.source.Play();
				while (s.source.volume < targetVolume)
				{
					s.source.volume += Time.deltaTime / duration;
					yield return null;
				}

				yield return new WaitForSeconds(duration);

				fadeIn = false;
			}
			else
			{
				Debug.Log("Could not handle two fade ins at once: " + name + " , " + fadeInUsedString + "! Played the music " + name);
				StopVolume(fadeInUsedString);
				PlayList();
			}
		}
	}

	public void PlayList()
	{
		if (source.isPlaying)
		{
			return;
		}

		currentClip--;
		if (currentClip < 0)
		{
			currentClip = musicList.Length - 1;
		}
		StartCoroutine(WaitForMusicEnd());
	}

	public void ResumeList()
	{
		source.clip = musicList[currentClip];
		source.Play();
		// Show Title
		StartCoroutine(WaitForMusicEnd());
	}

	public void NextTitle()
	{
		source.Stop();
		currentClip++;
		if (currentClip > musicList.Length - 1)
		{
			currentClip = 0;
		}

		source.clip = musicList[currentClip];
		source.Play();
		// Show Title
		StartCoroutine(WaitForMusicEnd());
	}

	public void LastTitle()
	{
		source.Stop();
		currentClip--;
		if (currentClip < 0)
		{
			currentClip = musicList.Length - 1;
		}

		source.clip = musicList[currentClip];
		source.Play();
		// Show Title
		StartCoroutine(WaitForMusicEnd());
	}

	IEnumerator WaitForMusicEnd()
	{
		while (source.isPlaying)
		{
			yield return null;
		}
		NextTitle();
	}

	void ResetVol()
	{
		AudioFile s = Array.Find(instance.audioFiles, AudioFile => AudioFile.audioName == tmpName);
		s.source.volume = tmpVol;
	}

	private void Update()
	{
		if (Time.time >= timeToReset && timerIsSet)
		{
			ResetVol();
			timerIsSet = false;
		}
	}

	#endregion
}