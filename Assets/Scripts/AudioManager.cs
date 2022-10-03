using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : Singleton<AudioManager>
{
    public AudioClip catTheme;
    public AudioClip witchTheme;
    public AudioClip doorSound;
    public AudioClip eyeballSound;
    public AudioClip mushroomSound;
    public AudioClip rootSound;
    public AudioClip meowSound;
    public AudioClip swipeSound;
    public string state;
    public AudioSource MyAudioSource;
    public Slider volumeSlider;
    private Object soundLock;


    // Start is called before the first frame update
    void Start()
    {
        SetDontDestroy();
        state = "cat";
        //GameObject soundGameObject = new GameObject("Audio");
        //MyAudioSource = soundGameObject.AddComponent<AudioSource>();
        //MyAudioSource = this.GetComponent<AudioSource>();
        // register the event
        TimerManager.TimerEventOnStart += SwitchAudio;
        //volumeSlider = this.GetComponent<Slider>();
        //TimerManager.Instance.StartTimer();
        //SwitchAudio();
        ChangeVolume(volumeSlider.value);
    }

    public void DoorSound()
    {
        MyAudioSource.PlayOneShot(doorSound);
    }

    public void EyeballSound()
    {
        MyAudioSource.PlayOneShot(eyeballSound);
    }

    public void RootSound()
    {
        MyAudioSource.PlayOneShot(rootSound);
    }

    public void MushroomSound()
    {
        MyAudioSource.PlayOneShot(mushroomSound);
    }

    public void MeowSound()
    {
        MyAudioSource.PlayOneShot(meowSound);
    }

    public void SwipeSound()
    {
        MyAudioSource.PlayOneShot(swipeSound);
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
        Debug.Log(state);
        if (state == "witch")
        {
            Debug.Log("witch theme");
            MyAudioSource.PlayOneShot(witchTheme);
        } else if (state == "cat")
        {
            Debug.Log("cat theme");
            MyAudioSource.PlayOneShot(catTheme);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
