using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SfxController : MonoBehaviour
{
    public AudioMixer audioMixer;
    [Space(10)]
    public Slider sfxSlider;

    // Use this for initialization
    private void Start()
    {
        audioMixer.SetFloat("SfxVolume", PlayerPrefs.GetFloat("SfxVolume", 0));
        sfxSlider = GameObject.Find("SfxVolume").GetComponent<Slider>();
    }

    public void SetSfxVolume(float SfxVolume)
    {

        audioMixer.SetFloat("SfxVolume", SfxVolume);
    }

    private void OnDisable()
    {
        float SfxVolume = 0;

        audioMixer.GetFloat("SfxVolume", out SfxVolume);

        PlayerPrefs.SetFloat("SfxVolume", SfxVolume);
        PlayerPrefs.Save();
    }

}
