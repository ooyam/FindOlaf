using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMonoBehaviourDontDestroy<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T instance;

    protected void Awake()
    {
        if (instance == null)
        {
            instance = (T)FindObjectOfType(typeof(T));
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected void OnDestroy()
    {
        if (this == instance) instance = null;
    }
}
