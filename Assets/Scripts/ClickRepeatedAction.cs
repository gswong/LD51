using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickRepeatedAction : MonoBehaviour
{
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
    }

    void OnMouseOver()
    {
        tutorialText.text = "click to collect";
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
        Debug.Log("Player clicked on Eye");
    }

    void OnMouseUp()
    {
        Debug.Log("Player released Eye");
        AudioManager.Instance.EyeballSound();
        ResourceManager.Instance.IncreaseEye();
    }
}
