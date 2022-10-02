using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// How to register event:
//
// In your class start method, register callback function
// 
//    TimerManager.TimerEvent += CallbackFunction;
//
// To start 10s timer, call start method
//
//    TimerManager.Instance.StartTime()
//
[RequireComponent(typeof(TimerManager))]
public class TimerManager : Singleton<TimerManager>
{
    public bool IsRunning;
    public static event Action TimerEventOnEnd;
    public static event Action TimerEventOnStart;
    public float TimeReference;
    public float TimeThreshold;

    // Start is called before the first frame update
    void Start()
    {
        SetDontDestroy();
        IsRunning = false;
        TimeThreshold = 10f;
        GameManager.GameStartEvent += StartTimer;
    }

    // Update is called once per frame
    void Update()
    {
        float timeDiff = Time.time - TimeReference;
        if (timeDiff > TimeThreshold && IsRunning)
        {
            TimerEventOnEnd?.Invoke();
            TimerEventOnStart?.Invoke();
            Debug.Log("Timer expired");
            TimeReference = Time.time;
        }
    }

    public void StartTimer()
    {
        IsRunning = true;
        TimeReference = Time.time;
        TimerEventOnStart?.Invoke();
        Debug.Log("Timer start");
    }

    public void StopTimer()
    {
        IsRunning = false;
    }
}
