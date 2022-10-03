using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickAndHoldAction : MonoBehaviour
{
    public float StartGatherTime;
    public bool IsGathering;
    public float GatherThreshold;
    public Text tutorialText;
    public Image background;
    public GameObject TutorialTooltip;
    public Canvas parentCanvas;
    [SerializeField] float xOffset;
    [SerializeField] float yOffset;

    // Start is called before the first frame update
    void Start()
    {
        tutorialText.gameObject.SetActive(false);
        background.gameObject.SetActive(false);
        xOffset = 2;
        yOffset = -0.5f;
        StartGatherTime = 0F;
        IsGathering = false;
        GatherThreshold = 0.5F;
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
        mousePos.x += this.xOffset;
        mousePos.y += this.yOffset;

        //Move the Object/Panel
        TutorialTooltip.transform.position = mousePos;
        float timeDifference = Time.time - StartGatherTime;
        if(IsGathering && timeDifference > GatherThreshold)
        {
            AudioManager.Instance.MushroomSound();
            Debug.Log("Player gathered mushroom");
            ResourceManager.Instance.IncreaseMushroom();
            StartGatherTime = Time.time;
        }
    }

    void OnMouseOver()
    {
        tutorialText.text = "hold to collect";
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
