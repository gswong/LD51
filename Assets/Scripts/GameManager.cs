using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public AudioManager Audio;
    public TimerManager Timer;
    public ResourceManager Resources;
    public static event Action GameStartEvent;
    public Text finalScore;
    public bool PlayerWon;


    // Start is called before the first frame update
    void Start()
    {
        SetDontDestroy();

        // Initialize all game resources
        Audio = AudioManager.Instance; 
        Timer = TimerManager.Instance;
        Resources = ResourceManager.Instance;
        finalScore.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StartGame()
    {
        GameStartEvent?.Invoke();
        SceneManager.LoadScene("ResourceRoom");
    }
}
