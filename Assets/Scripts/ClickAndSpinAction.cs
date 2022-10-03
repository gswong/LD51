using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.UI;

public class ClickAndSpinAction : MonoBehaviour
{
    public float Sensitivity;
    public Vector3 MouseReference;
    public Vector3 MouseOffset;
    public Vector3 ObjectPos;
    public Vector3 Rotation;
    public bool IsStirring;
    public float TimeReference;
    public int PotionMade;
    private const float _StirringThresholdRotation = 0.2f;
    private const float _StirringThresholdTime = 1f;
    private Queue<float> rotations;
    public float PrevStirringDirection;
    public static event Action ChangeStirringDirection;
    public float averageRotation;

    public Text tutorialText;
    public Image background;
    public GameObject TutorialTooltip;
    public Canvas parentCanvas;
    [SerializeField] float xOffset;
    [SerializeField] float yOffset;


    // Start is called before the first frame update
    void Start()
    {
        Sensitivity = 0.4f;
        Rotation = Vector3.zero;
        PotionMade = 0;
        rotations = new Queue<float>(100);
        IsStirring = false;
        for (int i = 0; i < 100; i++)
        {
            rotations.Enqueue(0f);
        }
        averageRotation = rotations.Average();
        PrevStirringDirection = 0f;

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

        rotations.Dequeue();
        rotations.Enqueue(Rotation.z);
        averageRotation = rotations.Average();

        if (IsStirring)
        {
            // stirring direction change
            if (!(PrevStirringDirection > _StirringThresholdRotation && averageRotation > _StirringThresholdRotation) &&
                !(PrevStirringDirection < -_StirringThresholdRotation && averageRotation < -_StirringThresholdRotation))
            {
                ChangeStirringDirection?.Invoke();
                Debug.Log("The stirring direction has changed!");
            }

            if (Mathf.Abs(averageRotation) > _StirringThresholdRotation)
            {
                float timeDiff = Time.time - TimeReference;
                if (timeDiff > _StirringThresholdTime)
                {
                    if (ResourceManager.Instance.ResourceRootValue > 0 && ResourceManager.Instance.ResourceEyeValue > 0 && ResourceManager.Instance.ResourceMushroomValue > 0)
                    {
                        Debug.Log("Potion made!");
                        PotionMade++;
                        ResourceManager.Instance.IncreaseScore();
                        ResourceManager.Instance.DecreaseRoot();
                        ResourceManager.Instance.DecreaseEye();
                        ResourceManager.Instance.DecreaseMushroom();
                    }
                    IsStirring = false;
                }
            }
            else
            {
                IsStirring = false;
            }
        }
        else
        {
            if (Mathf.Abs(averageRotation) > _StirringThresholdRotation)
            {
                Debug.Log("Start stirring");
                IsStirring = true;
                TimeReference = Time.time;
            }
        }
        PrevStirringDirection = averageRotation;
    }

    void OnMouseOver()
    {
        tutorialText.text = "stir to make potion";
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
        Debug.Log("Mouse clicked cauldron");

        // store mouse
        MouseReference = Input.mousePosition;
    }

    void OnMouseUp()
    {
        Rotation.z = 0;
        transform.Rotate(Rotation);
        Debug.Log("Mouse released cauldron");
    }

    void OnMouseDrag()
    {
        //Debug.Log("Mouse click and rotate cauldron");

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
