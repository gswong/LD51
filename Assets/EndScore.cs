using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScore : MonoBehaviour
{
    public Text finalScore;

    // Start is called before the first frame update
    void Start()
    {
        ResourceManager.Instance.EyeText.gameObject.SetActive(false);
        ResourceManager.Instance.MushroomText.gameObject.SetActive(false);
        ResourceManager.Instance.PotionText.gameObject.SetActive(false);
        ResourceManager.Instance.RootText.gameObject.SetActive(false);

        TimerManager.Instance.StopTimer();
        var duration = Time.time - TimerManager.Instance.StartTime;
        finalScore.text = "30 Potions made in " + Mathf.Round(duration) + "s";
        finalScore.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
