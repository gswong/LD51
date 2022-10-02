using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : Singleton<Cat>
{
    // Start is called before the first frame update
    void Start()
    {
        SetDontDestroy();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SwipeResource()
    {
        // Play Swipe Sound
        ResourceManager.Instance.DecreaseRandomResource();
    }

    public void SwipeCauldron()
    {
        // Play Attack Sound
        ResourceManager.Instance.AttackCauldron();
    }
}
