using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class UIControl : SingletonMonoBehaviour<UIControl>
    {
        [SerializeField, Header("メインキャンバス")]
        Transform _mainCanvasTra;

        public GameObject OpenUI(GameObject prefab, Transform parent = null)
        {
            if (parent == null)
            {
                parent = _mainCanvasTra;
            }
            return Instantiate(prefab, parent);
        }
    }
}
