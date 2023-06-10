using UnityEngine;
using System;

namespace Puzzle
{
    public static class PuzzleDefine
    {
        //----------定数---------//

        /// <summary>
        /// フラグ
        /// </summary>
        public enum FlagType
        {
            GameStart       = 1 << 0,   //ゲーム開始
            GameEnd         = 1 << 1,   //ゲーム終了
            TimeUp          = 1 << 2,   //タイムアップ
            ProblemEnd      = 1 << 3,   //規定問題数終了

            Penalty         = 1 << 4,   //ペナルティ中
            FeverEffect     = 1 << 5,   //フィーバー前演出中
            Fever           = 1 << 6,   //フィーバー中
            AllEraseFailure = 1 << 7,   //全消し失敗(タイムアタック)
            Miss            = 1 << 8,   //ミス中(フラッシュ)

            Pause           = 1 << 9,   //ポーズ中
        }
        static FlagType _flags;

        /// <summary>
        /// 数字のスプライトの色
        /// </summary>
        public enum NumSpriteColorsType
        {
            Black,  //黒
            Blue,   //青
            Orange, //橙

            Count
        }

        public const string OBJ_TAG_PANEL = "Panel";
        public const string OBJ_TAG_NAVI_PANEL = "NaviPanel";

        const string NUMBERS_SPRITE_DIR = "Numbers/";  //数字スプライト格納ディレクトリ

        //-----------------------//



        //---------関数---------//

        /// <summary>
        /// フラグリセット
        /// </summary>
        public static void ResetFlag() => _flags = 0;

        /// <summary>
        /// フラグの取得
        /// </summary>
        public static bool GetFlag(FlagType type) => (_flags & type) == type;

        /// <summary>
        /// フラグの設定
        /// </summary>
        public static void SetFlag(FlagType type, bool on)
        {
            if (on) _flags |= type;
            else _flags &= ~type;
        }

        /// <summary>
        /// 数字スプライトの取得
        /// </summary>
        public static Sprite[][] GetNumberSpritesAry()
        {
            //数字のスプライト取得
            Sprite[][] numSpritesAry = new Sprite[(int)NumSpriteColorsType.Count][];
            for (int i = 0; i < (int)NumSpriteColorsType.Count; i++)
            {
                NumSpriteColorsType cType = (NumSpriteColorsType)Enum.ToObject(typeof(NumSpriteColorsType), i);
                numSpritesAry[(int)cType] = Resources.LoadAll<Sprite>(NUMBERS_SPRITE_DIR + cType.ToString());
            }
            return numSpritesAry;
        }

        //-----------------------//
    }
}