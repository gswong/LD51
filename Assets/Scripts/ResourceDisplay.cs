using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Canvas canvas = this.GetComponent<Canvas>();
        Text t = (Text)canvas.GetComponent<Text>();
        Debug.Log(t.text);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
