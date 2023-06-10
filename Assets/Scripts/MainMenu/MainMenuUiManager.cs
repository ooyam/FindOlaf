using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUiManager : MonoBehaviour
{
    [SerializeField, Header("モード選択UIプレハブ")]
    GameObject _modeSelectUiPrefab;

    [SerializeField, Header("ボタンの親オブジェクト")]
    Transform _btnParent;

    [SerializeField, Header("フィルター")]
    Image _filterImg;

    enum MainBtn
    {
        GameStart,
        Ranking,
        Option,

        Count
    }

    enum ModeSelectBtn
    {
        SpeedMode,
        NoMissMode,
        Return,

        Count
    }

    GameObject _modeSelectUiObj;

    public void Initialize()
    {
        for (int i = 0; i < (int)MainBtn.Count; i++)
        {
            var btnType = (MainBtn)i;
            var btn = _btnParent.GetChild(i).GetComponent<Button>();
            btn.onClick.AddListener(() => MainBtnClick(btnType));
        }
    }

    /// <summary>
    /// ボタンクリック
    /// </summary>
    void MainBtnClick(MainBtn btnType)
    {
        if (_filterImg.enabled) return;

        switch (btnType)
        {
            case MainBtn.GameStart:
                _filterImg.enabled = true;
                OpenModeSelectUi();
                break;

            case MainBtn.Ranking:
                Debug.Log("ランキング");
                break;

            case MainBtn.Option:
                Debug.Log("オプション");
                break;
        }
    }

    /// <summary>
    /// モード選択UIの生成
    /// </summary>
    void OpenModeSelectUi()
    {
        _modeSelectUiObj = UIControl.instance.OpenUI(_modeSelectUiPrefab);
        var tra = _modeSelectUiObj.transform;
        for (int i = 0; i < (int)ModeSelectBtn.Count; i++)
        {
            var btnType = (ModeSelectBtn)i;
            tra.GetChild(i).GetComponent<Button>().onClick.AddListener(() => ModeSelectBtnClick(btnType));
        }
    }

    /// <summary>
    /// モード選択UIの削除
    /// </summary>
    void CloseModeSelectUi()
    {
        Destroy(_modeSelectUiObj);
        _filterImg.enabled = false;
    }

    /// <summary>
    /// ボタンクリック
    /// </summary>
    void ModeSelectBtnClick(ModeSelectBtn btnType)
    {
        switch (btnType)
        {
            case ModeSelectBtn.SpeedMode:
                Global.instance.GameMode = ComDefine.GameMode.Speed;
                Global.instance.SceneChange(ComDefine.GAME_SCENE_NAME, SceneFader.FadeColorType.Black);
                break;

            case ModeSelectBtn.NoMissMode:
                Global.instance.GameMode = ComDefine.GameMode.NoMiss;
                Global.instance.SceneChange(ComDefine.GAME_SCENE_NAME, SceneFader.FadeColorType.Black);
                break;

            case ModeSelectBtn.Return:
                CloseModeSelectUi();
                break;
        }
    }
}
