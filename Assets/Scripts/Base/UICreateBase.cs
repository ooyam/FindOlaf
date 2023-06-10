using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class UICreateBase<T> : SingletonMonoBehaviour<T> where T : SingletonMonoBehaviour<T>
{
    List<GameObject> _createObjList;

    protected override void AfterAwake()
    {
        _createObjList = new();
    }

    protected GameObject UIMainPrefabCreate(GameObject pre)
    {
        var obj = UIControl.instance.OpenUI(pre);
        _createObjList.Add(obj);
        return obj;
    }

    protected override void AfterOnDestroy()
    {
        foreach (var obj in _createObjList)
        {
            if (obj != null) Destroy(obj);
        }
    }
}
