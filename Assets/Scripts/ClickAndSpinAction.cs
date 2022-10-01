using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAndSpinAction : MonoBehaviour
{
    private float _sensitivity;
    public Vector3 _mouseReference;
    public Vector3 _mouseOffset;
    public Vector3 _objectPos;
    private Vector3 _rotation;

    // Start is called before the first frame update
    void Start()
    {
        _sensitivity = 0.4f;
        _rotation = Vector3.zero; 
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnMouseDown()
    {
        Debug.Log("Mouse clicked cauldron");
         
        // store mouse
        _mouseReference = Input.mousePosition;
    }

    void OnMouseUp()
    {
        Debug.Log("Mouse released cauldron");
    }

    void OnMouseDrag()
    {
        Debug.Log("Mouse click and rotate cauldron");

        _mouseOffset = (Input.mousePosition - _mouseReference);
        _objectPos = Camera.main.WorldToScreenPoint(transform.position);

        // flip rotation relative to position of game object
        float flipx = 1;
        float flipy = 1;
        if (Input.mousePosition.y < _objectPos.y)
        {
            flipx = -1;
        }
        if (Input.mousePosition.x < _objectPos.x)
        {
            flipy = -1;
        }
        
        // apply rotation
        _rotation.z = -(_mouseOffset.x * flipx - _mouseOffset.y * flipy) * _sensitivity;
        
        // rotate
        transform.Rotate(_rotation);
        
        // store mouse
        _mouseReference = Input.mousePosition;
    }
}
