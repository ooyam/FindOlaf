using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainMenu
{
    public class MaimMenuMain : UICreateBase<MaimMenuMain>
    {
        [SerializeField, Header("���C��UI�v���n�u")]
        GameObject _mainUiPrefab;

        MainMenuUiManager _mainUiMgr;

        void Start()
        {
            var obj = UIMainPrefabCreate(_mainUiPrefab);
            _mainUiMgr = obj.GetComponent<MainMenuUiManager>();
            _mainUiMgr.Initialize();
        }
    }
}
