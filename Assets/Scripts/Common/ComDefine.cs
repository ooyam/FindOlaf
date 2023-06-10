using System;
using UnityEngine;

namespace Common
{
    public static class ComDefine
    {
        //--- 定数 ---//
        #region

        /// <summary>
        /// モードの種類
        /// </summary>
        public enum GameMode
        {
            Speed,  // スピードモード
            NoMiss, // ノーミスモード
        }

        //画面サイズ
        public const float SCREEN_WIDTH = 1920f;
        public const float SCREEN_HEIGHT = 1080f;

        //シーン名
        public const string MAIN_MENU_SCENE_NAME = @"MainMenu";
        public const string GAME_SCENE_NAME = @"Game";

        //フレーム関連
        public const float ONE_FRAME_TIMES = 0.02f;
        public static readonly WaitForFixedUpdate FIXED_UPDATE = new();
        #endregion


        //--- 関数 ---//
        #region

        ///// <summary>
        ///// 動物パネルのスプライト取得
        ///// </summary>
        ///// <param name="aType">動物</param>
        ///// <param name="eType">表情</param>
        ///// <param name="pType">パネル</param>
        ///// <returns>パネルのスプライト</returns>
        //public static Sprite GetAnimalPanelSprite(AnimalType aType, ExpressionType eType, PanelColorType cType, PanelType pType)
        //{
        //    return Resources.Load<Sprite>(string.Format("{0}{1}/{2}/{3}/{4}", PANEL_DIR, cType, pType, aType, eType.ToString().ToLower()));
        //}

        ///// <summary>
        ///// 動物パネルのフィルタースプライト取得
        ///// </summary>
        ///// <param name="pType">パネルの種類</param>
        ///// <returns></returns>
        //public static Sprite GetPanelWhiteSprite(PanelType pType)
        //{
        //    string fileName = PANEL_DIR + PANEL_FILTER_FILE;
        //    switch (pType)
        //    {
        //        //パステル
        //        case PanelType.Pastel:
        //        case PanelType.HeadbandPastel:
        //            return Resources.Load<Sprite>(fileName + PanelType.Pastel.ToString().ToLower());

        //        //その他(ノーマル)
        //        default:
        //            return Resources.Load<Sprite>(fileName + PanelType.Normal.ToString().ToLower());
        //    }
        //}
#endregion
    }
}
