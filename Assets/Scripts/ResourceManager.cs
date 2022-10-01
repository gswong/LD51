using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[RequireComponent(typeof(ResourceManager))]
public class ResourceManager : Singleton<ResourceManager>
{
    public int ResourceRootValue;
    public int ResourceEyeValue;
    public int ResourceMushroomValue;

    public int ScoreValue;
    public int ProgressValue;

    public enum ResourceType { Eye, Mushroom, Root }

    void Start()
    {
        Interlocked.Exchange(ref ResourceEyeValue, 0);
        Interlocked.Exchange(ref ResourceMushroomValue, 0);
        Interlocked.Exchange(ref ResourceRootValue, 0);
    }

    public void IncreaseEye()
    {
        Interlocked.Increment(ref ResourceEyeValue);
    }

    public void IncreaseMushroom()
    {
        Interlocked.Increment(ref ResourceMushroomValue);
    }
    
    public void IncreaseRoot()
    {
        Interlocked.Increment(ref ResourceRootValue);
    }

    public void DecreaseEye()
    {
        if(ResourceEyeValue > 0)
        {
            Interlocked.Decrement(ref ResourceEyeValue);
        }
    }

    public void DecreaseMushroom()
    {
        if(ResourceMushroomValue > 0)
        {
            Interlocked.Decrement(ref ResourceMushroomValue);
        }
    }
    
    public void DecreaseRoot()
    {
        if(ResourceRootValue > 0)
        {
            Interlocked.Decrement(ref ResourceRootValue);
        }
    }

    public void DecreaseRandomResource()
    {
        var v = Enum.GetValues(typeof(ResourceType));
        var resource = v.GetValue(UnityEngine.Random.Range(0, v.Length));
        switch(resource)
        {
            case ResourceType.Eye:
                DecreaseEye();
                break;
            case ResourceType.Mushroom:
                DecreaseMushroom();
                break;
            case ResourceType.Root:
                DecreaseRoot();
                break;
            default:
                break;
        }
    }
}
