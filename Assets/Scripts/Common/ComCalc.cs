using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public static class ComCalc
    {
        //--- 桁数計算 ---//
        #region
        //10進法用定数
        const int TEN = 10;

        /// <summary>
        /// 位毎の値を取得
        /// </summary>
        /// <param name="value">元の値</param>
        /// <returns>[0]：1の位,[1]：10の位～</returns>
        public static int[] GetDigitValueAry(int value)
        {
            List<int> digitValueList = new();
            while (IsMoreThanTwoDigits(value))
            {
                digitValueList.Add(value % TEN);
                value /= TEN;
            }
            digitValueList.Add(value);

            return digitValueList.ToArray();
        }

        /// <summary>
        /// 2桁以上か？
        /// </summary>
        /// <param name="value">値</param>
        /// <returns>true：2桁以上</returns>
        public static bool IsMoreThanTwoDigits(int value) => value >= TEN;

        /// <summary>
        /// 桁数の取得
        /// </summary>
        /// <param name="value">値</param>
        /// <returns>桁数</returns>
        public static int GetNumberOfDigits(int value) => GetDigitValueAry(value).Length;

        /// <summary>
        /// 小数点以下端数処理
        /// </summary>
        /// <param name="value">値</param>
        /// <param name="digit">小数点第何位までにするか</param>
        public static float GetRoundedValue(float value, int digit)
        {
            int multiplier = TEN * digit;
            value *= multiplier;
            float tmp = (float)Math.Round(value, 0, MidpointRounding.AwayFromZero);
            return tmp / multiplier;
        }
        #endregion


        //--- 配列計算 ---//
        #region

        public static void Shuffle<T>(this IList<T> ary)
        {
            for (int i = ary.Count - 1; i > 0; --i)
            {
                int index = UnityEngine.Random.Range(0, i + 1);
                (ary[index], ary[i]) = (ary[i], ary[index]);
            }
        }
        #endregion


        //--- 配置座標計算 ---//
        #region

        /// <summary>
        /// 矩形配置座標御取得
        /// </summary>
        /// <param name="lineCnt">行の数</param>
        /// <param name="columnCnt">列の数</param>
        /// <param name="size">配置オブジェクトのサイズ</param>
        /// <param name="margin">配置オブジェクトの隙間</param>
        public static Vector2[] GetSquarePosAry(int lineCnt, int columnCnt, Vector2 size, Vector2 margin)
        {
            Vector2[] posAry = new Vector2[lineCnt * columnCnt];
            float lineOffset = (lineCnt - 1) / 2f;
            float columnOffset = (columnCnt - 1) / 2f;
            for (int a = 0; a < lineCnt; a++)
            {
                float offset = (a - lineOffset);
                float posY = offset * (size.y + margin.y);
                for (int b = 0; b < columnCnt; b++)
                {
                    offset = (b - columnOffset);
                    float posX = offset * (size.x + margin.y);
                    posAry[a * columnCnt + b] = new Vector2(posX, posY);
                }
            }
            return posAry;
        }
        #endregion
    }
}