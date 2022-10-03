using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickAndSwipeAction : MonoBehaviour
{

    public Vector3 MouseReference;
    public Vector3 MouseOffset;
    public float GatherThreshold;
    public bool GatherThresholdReached;
    public Text tutorialText;
    public Image background;
    public GameObject TutorialTooltip;
    public Canvas parentCanvas;
    [SerializeField] float xOffset;
    [SerializeField] float yOffset;

    // Start is called before the first frame update
    void Start()
    {
        GatherThreshold = 50F;
        GatherThresholdReached = false;
        tutorialText.gameObject.SetActive(false);
        background.gameObject.SetActive(false);
        xOffset = 2;
        yOffset = -0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movePos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentCanvas.transform as RectTransform,
            Input.mousePosition, parentCanvas.worldCamera,
            out movePos);

        Vector3 mousePos = parentCanvas.transform.TransformPoint(movePos);
        mousePos.x += xOffset;
        mousePos.y += yOffset;

        //Move the Object/Panel
        TutorialTooltip.transform.position = mousePos;
    }

    void OnMouseOver()
    {
        tutorialText.text = "swipe to collect";
        tutorialText.gameObject.SetActive(true);
        background.gameObject.SetActive(true);
    }

    void OnMouseExit()
    {
        tutorialText.gameObject.SetActive(false);
        background.gameObject.SetActive(false);
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
            AudioManager.Instance.RootSound();
            ResourceManager.Instance.IncreaseRoot();
        }

        GatherThresholdReached = false;
    }
}
