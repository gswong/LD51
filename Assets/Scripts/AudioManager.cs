using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : Singleton<AudioManager>
{
    public AudioClip catTheme;
    public AudioClip witchTheme;
    public string state;
    public AudioSource MyAudioSource;
    public Slider volumeSlider;


    // Start is called before the first frame update
    void Start()
    {
        SetDontDestroy();
        state = "cat";
        GameObject soundGameObject = new GameObject("Audio");
        MyAudioSource = soundGameObject.AddComponent<AudioSource>();
        // register the event
        TimerManager.TimerEventOnStart += SwitchAudio;
        //TimerManager.Instance.StartTimer();
        //SwitchAudio();
    }

    void OnEnable()
    {
        volumeSlider.onValueChanged.AddListener(delegate { ChangeVolume(volumeSlider.value); });
    }

    void ChangeVolume(float sliderValue)
    {
        MyAudioSource.volume = sliderValue;
    }

    void OnDisable()
    {
        volumeSlider.onValueChanged.RemoveAllListeners();
    }

    void SwitchAudio()
    {
        if (state == "witch")
        {
            state = "cat";
        } else if (state == "cat")
        {
            state = "witch";
        }

        if (state == "witch")
        {
            MyAudioSource.PlayOneShot(witchTheme);
        } else if (state == "cat")
        {
            MyAudioSource.PlayOneShot(catTheme);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
