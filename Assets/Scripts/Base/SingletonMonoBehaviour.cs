using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T instance;

    protected void Awake()
    {
        if (instance == null)
        {
            instance = (T)FindObjectOfType(typeof(T));
            AfterAwake();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected virtual void AfterAwake()
    {
        // 継承先では基本的にStart()で初期化などを行うが
        // Startより先に行いたい処理は
        // このメソッドをoverrideして使用する
    }

    protected void OnDestroy()
    {
        if (this == instance) instance = null;
        AfterOnDestroy();
    }

    protected virtual void AfterOnDestroy()
    {
        // 継承先でOnDestroyを使用したいとき
        // このメソッドをoverrideして使用する
    }
}
