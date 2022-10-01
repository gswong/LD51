using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAndHoldAction : MonoBehaviour
{
    public float StartGatherTime;
    public bool IsGathering;
    public float GatherThreshold;

    // Start is called before the first frame update
    void Start()
    {
        StartGatherTime = 0F;
        IsGathering = false;
        GatherThreshold = 0.5F;
    }

    // Update is called once per frame
    void Update()
    {
        float timeDifference = Time.time - StartGatherTime;
        if(IsGathering && timeDifference > GatherThreshold)
        {
            Debug.Log("Player gathered mushroom");
            ResourceManager.Instance.IncreaseMushroom();
            StartGatherTime = Time.time;
        }
    }
    
    void OnMouseDown()
    {
        Debug.Log("Player clicked on mushroom");
        IsGathering = true;
        StartGatherTime = Time.time;
    }

    void OnMouseUp()
    {
        Debug.Log("Player released mushroom");
        IsGathering = false;
    }

}
