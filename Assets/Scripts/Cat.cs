using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cat : Singleton<Cat>
{
    public bool CatEnabled;
    public GameObject ResourceCat;
    public bool Cooldown;
    public float CooldownRefTime;
    // Start is called before the first frame update
    void Start()
    {
        SetDontDestroy();
        TimerManager.TimerEventOnEnd += ToggleCat;
        CatEnabled = false;
        CooldownRefTime = 0f;
    }

    void ToggleCat()
    {
        CatEnabled = !CatEnabled;
        Debug.Log("Cat: " + CatEnabled.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        ResourceCat = GameObject.FindGameObjectWithTag("ResourceCat");
        if (currentScene.name == "ResourceRoom" && CatEnabled && ResourceCat != null && Time.time - CooldownRefTime > 0.5f)
        {
            Debug.Log("CatResource enabled");
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.x = mousePosition.x - 3f;
            mousePosition.y = mousePosition.y + 3f;
            mousePosition.z = -3f;
            ResourceCat.transform.position = mousePosition;
            SwipeResource();
            CooldownRefTime = Time.time;
        }
        else if(ResourceCat != null && !CatEnabled && currentScene.name == "ResourceRoom")
        {
            Debug.Log("CatResource disabled");
            Vector3 pos = new Vector3(-1f, -3f, 0f);
            pos.z = 3f;
            ResourceCat.transform.position = pos;
        }
        GameObject CauldronCat = GameObject.FindGameObjectWithTag("CauldronCat");
        if (CatEnabled && currentScene.name == "CauldronRoom")
        {
            CauldronCat.GetComponentInChildren<Renderer>().enabled = true;
        }
        else if (currentScene.name == "CauldronRoom")
        {
            CauldronCat.GetComponentInChildren<Renderer>().enabled = false;
        }
    }

    public void SwipeResource()
    {
        // Play Swipe Sound
        AudioManager.Instance.SwipeSound();
        ResourceManager.Instance.DecreaseRandomResource();
    }

    public void SwipeCauldron()
    {
        // Play Attack Sound
        AudioManager.Instance.MeowSound();
        ResourceManager.Instance.AttackCauldron();
    }
}
