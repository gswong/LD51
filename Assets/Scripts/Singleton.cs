using UnityEngine;
using System.Collections;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    public static T Instance;
    private bool _doNotDestroy = false;

    protected virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = (T)this;
            gameObject.tag = "singleton";
            Debug.Log("Tagged " + gameObject.name + " as singleton");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected void SetDontDestroy()
    {
        DontDestroyOnLoad(gameObject);
        _doNotDestroy = true;
    }

    private void OnDestroy() {
        if(!_doNotDestroy) 
            Instance = null;
    }
}