using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAndSpinAction : MonoBehaviour
{
    public float Sensitivity;
    public Vector3 MouseReference;
    public Vector3 MouseOffset;
    public Vector3 ObjectPos;
    public Vector3 Rotation;

    // Start is called before the first frame update
    void Start()
    {
        Sensitivity = 0.4f;
        Rotation = Vector3.zero; 
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnMouseDown()
    {
        Debug.Log("Mouse clicked cauldron");
         
        // store mouse
        MouseReference = Input.mousePosition;
    }

    void OnMouseUp()
    {
        Debug.Log("Mouse released cauldron");
    }

    void OnMouseDrag()
    {
        Debug.Log("Mouse click and rotate cauldron");

        MouseOffset = (Input.mousePosition - MouseReference);
        ObjectPos = Camera.main.WorldToScreenPoint(transform.position);

        // flip rotation relative to position of game object
        float flipx = 1;
        float flipy = 1;
        if (Input.mousePosition.y < ObjectPos.y)
        {
            flipx = -1;
        }
        if (Input.mousePosition.x < ObjectPos.x)
        {
            flipy = -1;
        }
        
        // apply rotation
        Rotation.z = -(MouseOffset.x * flipx - MouseOffset.y * flipy) * Sensitivity;
        
        // rotate
        transform.Rotate(Rotation);
        
        // store mouse
        MouseReference = Input.mousePosition;
    }
}
