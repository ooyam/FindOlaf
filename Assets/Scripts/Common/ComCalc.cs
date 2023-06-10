using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public static class ComCalc
    {
        //--- �����v�Z ---//
        #region
        //10�i�@�p�萔
        const int TEN = 10;

        /// <summary>
        /// �ʖ��̒l���擾
        /// </summary>
        /// <param name="value">���̒l</param>
        /// <returns>[0]�F1�̈�,[1]�F10�̈ʁ`</returns>
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
        /// 2���ȏォ�H
        /// </summary>
        /// <param name="value">�l</param>
        /// <returns>true�F2���ȏ�</returns>
        public static bool IsMoreThanTwoDigits(int value) => value >= TEN;

        /// <summary>
        /// �����̎擾
        /// </summary>
        /// <param name="value">�l</param>
        /// <returns>����</returns>
        public static int GetNumberOfDigits(int value) => GetDigitValueAry(value).Length;

        /// <summary>
        /// �����_�ȉ��[������
        /// </summary>
        /// <param name="value">�l</param>
        /// <param name="digit">�����_�扽�ʂ܂łɂ��邩</param>
        public static float GetRoundedValue(float value, int digit)
        {
            int multiplier = TEN * digit;
            value *= multiplier;
            float tmp = (float)Math.Round(value, 0, MidpointRounding.AwayFromZero);
            return tmp / multiplier;
        }
        #endregion


        //--- �z��v�Z ---//
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


        //--- �z�u���W�v�Z ---//
        #region

        /// <summary>
        /// ��`�z�u���W��擾
        /// </summary>
        /// <param name="lineCnt">�s�̐�</param>
        /// <param name="columnCnt">��̐�</param>
        /// <param name="size">�z�u�I�u�W�F�N�g�̃T�C�Y</param>
        /// <param name="margin">�z�u�I�u�W�F�N�g�̌���</param>
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