using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAndSwipeAction : MonoBehaviour
{

    public Vector3 MouseReference;
    public Vector3 MouseOffset;
    public float GatherThreshold;
    public bool GatherThresholdReached;

    // Start is called before the first frame update
    void Start()
    {
        GatherThreshold = 50F;
        GatherThresholdReached = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        // store mouse
        MouseReference = Input.mousePosition;
    }

    void OnMouseDrag()
    {
        Debug.Log("Player is swiping root");
        // Detect pos > threshold from initial pos to set collection flag true 
        if (Vector3.Distance(Input.mousePosition, MouseReference) >= GatherThreshold)
        {
            Debug.Log("Player swiped root beyond threshold");
            GatherThresholdReached = true;
        }
    }

    void OnMouseUp()
    {
        Debug.Log("Player released root");
        if (GatherThresholdReached)
        {
            // Increment resource
            Debug.Log("Player gathered root");
            ResourceManager.Instance.IncreaseRoot();
        }

        GatherThresholdReached = false;
    }
}
