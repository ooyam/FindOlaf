using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class ThemeManager : MonoBehaviour
    {
        [SerializeField, Header("‚¨‘èUI")]
        Transform _parent;

        OlafManager.Olaf _themeOlaf;
        GameObject _themeObj;

        public void Initialize()
        {

        }

        public void SetUi()
        {
            SetTheme();
        }

        public void SetTheme()
        {
            _themeOlaf = (OlafManager.Olaf)Random.Range(0, (int)OlafManager.Olaf.Count);
            if (_themeObj != null)
            {
                Destroy(_themeObj);
            }
            _themeObj = UIControl.instance.OpenUI(GameMain.instance.OlafMgr.OlafPreAry[(int)_themeOlaf], _parent);
            GameMain.instance.OlafMgr.CreateOlaf(_themeOlaf);
        }
    }
}
