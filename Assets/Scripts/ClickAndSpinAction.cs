using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAndSpinAction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        Debug.Log("Test mouse down");
    }

    void OnMouseUp()
    {
        Debug.Log("Test mouse up");
    }

    void OnMouseDrag()
    {
        Debug.Log("Test mouse drag");
    }
}
