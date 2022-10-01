using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAndSwipeAction : MonoBehaviour
{

    public Vector3 _mouseReference;
    public Vector3 _mouseOffset;
    public float _gatherThreshold;
    public bool _gatherThresholdReached;

    // Start is called before the first frame update
    void Start()
    {
        _gatherThreshold = 50F;
        _gatherThresholdReached = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        // store mouse
        _mouseReference = Input.mousePosition;
    }

    void OnMouseDrag()
    {
        Debug.Log("Player is swiping root");
        // Detect pos > threshold from initial pos to set collection flag true 
        if (Vector3.Distance(Input.mousePosition, _mouseReference) >= _gatherThreshold)
        {
            Debug.Log("Player swiped root beyond threshold");
            _gatherThresholdReached = true;
        }
    }

    void OnMouseUp()
    {
        Debug.Log("Player released root");
        if (_gatherThresholdReached)
        {
            // Increment resource
            Debug.Log("Player gathered root");
        }

        _gatherThresholdReached = false;
    }
}
