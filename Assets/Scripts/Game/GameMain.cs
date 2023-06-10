using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class GameMain : UICreateBase<GameMain>
    {
        [SerializeField, Header("メインUIプレハブ")]
        GameObject _mainUiPre;

        public OlafManager OlafMgr { get; set; }
        public ProblemManager ProblemMgr { get; set; }
        public TimeManager TimeMgr { get; set; }
        public ThemeManager ThemeMgr { get; set; }
        public EffectManager EffMgr { get; set; }


        void Start()
        {
            var obj = UIMainPrefabCreate(_mainUiPre);
            var mgrParent = obj.transform.GetChild(0);
            OlafMgr = mgrParent.GetComponent<OlafManager>();
            ProblemMgr = mgrParent.GetComponent<ProblemManager>();
            TimeMgr = mgrParent.GetComponent<TimeManager>();
            ThemeMgr = mgrParent.GetComponent<ThemeManager>();
            EffMgr = mgrParent.GetComponent<EffectManager>();

            OlafMgr.Initialize();
            ProblemMgr.Initialize();
            TimeMgr.Initialize();
            ThemeMgr.Initialize();
            EffMgr.Initialize();

            // ゲーム開始
            StartCoroutine(GameStart());
        }

        IEnumerator GameStart()
        {
            yield return new WaitForSeconds(1f);
            yield return EffMgr.StartEffect(EffectManager.Effect.GameStart);

            SetFlag(GameFlag.GameStart);

            ThemeMgr.SetUi();
        }

        public IEnumerator GameEnd()
        {
            SetFlag(GameFlag.GameEnd);
            yield return null;
            Debug.Log("GameEnd");
        }

        //--- フラグ ---//
        #region

        [Flags]
        public enum GameFlag
        {
            GameStart   = 1 << 0,   //ゲーム開始
            GameEnd     = 1 << 1,   //ゲーム終了
        }
        GameFlag _gameFlags;
        public void ResetAllFlag() => _gameFlags = 0;
        public bool GetFlag(GameFlag flag) => (_gameFlags & flag) == flag;
        public void ResetFlag(GameFlag flag) => _gameFlags &= ~flag;
        public void SetFlag(GameFlag flag) => _gameFlags |= flag;
        #endregion
    }
}