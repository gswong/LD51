using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(ResourceManager))]
public class ResourceManager : Singleton<ResourceManager>
{
    public int ResourceRootValue;
    public int ResourceEyeValue;
    public int ResourceMushroomValue;

    public int ResourceRootValueForCat;
    public int ResourceEyeValueForCat;
    public int ResourceMushroomValueForCat;

    public int ScoreValue;
    public int ProgressValue;

    public TMP_Text EyeText;
    public TMP_Text MushroomText;
    public TMP_Text RootText;
    public TMP_Text PotionText;
    public Canvas ResourceCanvas;

    public enum ResourceType { Eye, Mushroom, Root }

    void Start()
    {
        EyeText = GameObject.FindGameObjectWithTag("EyeText").GetComponent<TMP_Text>();
        MushroomText = GameObject.FindGameObjectWithTag("MushroomText").GetComponent<TMP_Text>();
        RootText = GameObject.FindGameObjectWithTag("RootText").GetComponent<TMP_Text>();
        PotionText = GameObject.FindGameObjectWithTag("PotionText").GetComponent<TMP_Text>();
        Interlocked.Exchange(ref ResourceEyeValue, 0);
        Interlocked.Exchange(ref ResourceMushroomValue, 0);
        Interlocked.Exchange(ref ResourceRootValue, 0);
        Interlocked.Exchange(ref ResourceEyeValueForCat, 0);
        Interlocked.Exchange(ref ResourceMushroomValueForCat, 0);
        Interlocked.Exchange(ref ResourceRootValueForCat, 0);
        ResourceCanvas = GameObject.FindGameObjectWithTag("ResourceCanvas").GetComponent<Canvas>();
        ResourceCanvas.enabled = false;
        GameManager.GameStartEvent += EnableCanvas;
        SetDontDestroy();
    }

    public void EnableCanvas()
    {
        ResourceCanvas.enabled = true;
    }

    public void IncreaseScore()
    {
        Interlocked.Increment(ref ScoreValue);
    }

    public void IncreaseEye()
    {
        Interlocked.Increment(ref ResourceEyeValue);
    }

    public void IncreaseMushroom()
    {
        Interlocked.Increment(ref ResourceMushroomValue);
    }

    public void IncreaseRoot()
    {
        Interlocked.Increment(ref ResourceRootValue);
    }

    public void DecreaseEye()
    {
        if(ResourceEyeValue > 0)
        {
            Interlocked.Decrement(ref ResourceEyeValue);
        }
    }

    public void DecreaseMushroom()
    {
        if(ResourceMushroomValue > 0)
        {
            Interlocked.Decrement(ref ResourceMushroomValue);
        }
    }

    public void DecreaseRoot()
    {
        if(ResourceRootValue > 0)
        {
            Interlocked.Decrement(ref ResourceRootValue);
        }
    }

    public void IncreaseEyeForCat()
    {
        Interlocked.Increment(ref ResourceRootValueForCat);
    }

    public void IncreaseMushroomForCat()
    {
        Interlocked.Increment(ref ResourceMushroomValueForCat);
    }

    public void IncreaseRootForCat()
    {
        Interlocked.Increment(ref ResourceRootValueForCat);
    }

    public void DecreaseEyeForCat()
    {
        if(ResourceEyeValueForCat > 0)
        {
            Interlocked.Decrement(ref ResourceEyeValueForCat);
        }
    }

    public void DecreaseMushroomForCat()
    {
        if(ResourceMushroomValueForCat > 0)
        {
            Interlocked.Decrement(ref ResourceMushroomValueForCat);
        }
    }

    public void DecreaseRootForCat()
    {
        if(ResourceRootValueForCat > 0)
        {
            Interlocked.Decrement(ref ResourceRootValueForCat);
        }
    }

    public void DecreaseRandomResource()
    {
        var v = Enum.GetValues(typeof(ResourceType));
        var resource = v.GetValue(UnityEngine.Random.Range(0, v.Length));
        switch(resource)
        {
            case ResourceType.Eye:
                DecreaseEye();
                IncreaseEyeForCat();
                break;
            case ResourceType.Mushroom:
                DecreaseMushroom();
                IncreaseMushroomForCat();
                break;
            case ResourceType.Root:
                DecreaseRoot();
                IncreaseRootForCat();
                break;
            default:
                break;
        }
    }

    public void RegainRandomResource()
    {
        // If resource is regainable by player from cat, add an action
        // to increase player resource and decrease cat resource
        var regainResourceAction = new List<Action>();

        if (ResourceEyeValueForCat > 0)
        {
            regainResourceAction.Add(() => {
                IncreaseEye();
                DecreaseEyeForCat();
            });
        }

        if (ResourceMushroomValueForCat > 0)
        {
            regainResourceAction.Add(() => {
                IncreaseMushroom();
                DecreaseMushroomForCat();
            });
        }

        if (ResourceRootValueForCat > 0)
        {
            regainResourceAction.Add(() => {
                IncreaseRoot();
                DecreaseRootForCat();
            });
        }

        if (regainResourceAction.Count > 0)
        {
            regainResourceAction[UnityEngine.Random.Range(0, regainResourceAction.Count)]();
        }
    }

    void Update()
    {
        // update ui
        if (ResourceCanvas.enabled)
        {
            EyeText.text = ResourceEyeValue.ToString(); 
            MushroomText.text = ResourceMushroomValue.ToString(); 
            RootText.text = ResourceRootValue.ToString(); 
            PotionText.text = ScoreValue.ToString(); 
        }
    }

    public void AttackCauldron()
    {
        // Randomly lost or regain resource for now when cat attacks cauldron scene
        // Plan to update when logic is finalized
        var attackAction = new List<Action>();

        attackAction.Add(() => {
            // Play lost resource sound
            DecreaseRandomResource();
        });

        attackAction.Add(() => {
            // Play regain sound
            RegainRandomResource();
        });

        if (attackAction.Count > 0)
        {
            attackAction[UnityEngine.Random.Range(0, attackAction.Count)]();
        }
    }

}
