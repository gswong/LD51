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
    public static event Action TimerEvent;
    public float TimeReference;
    public float TimeThreshold;

    // Start is called before the first frame update
    void Start()
    {
        IsRunning = false;
        TimeThreshold = 10f;
        SetDontDestroy();
    }

    // Update is called once per frame
    void Update()
    {
        float timeDiff = Time.time - TimeReference;
        if (timeDiff > TimeThreshold)
        {
            TimerEvent?.Invoke();
            Debug.Log("Timer expired");
            TimeReference = Time.time;
        }
    }

    public void StartTimer()
    {
        IsRunning = true;
        TimeReference = Time.time;
    }

    public void StopTimer()
    {
        IsRunning = false;
    }
}
