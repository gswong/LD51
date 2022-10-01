using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickRepeatedAction : MonoBehaviour
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
        Debug.Log("Player clicked on Eye");
    }

    void OnMouseUp()
    {
        Debug.Log("Player released Eye");
        ResourceManager.Instance.IncreaseEye();
    }
}
