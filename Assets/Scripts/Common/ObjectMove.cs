using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Common
{
    public class ObjectMove : MonoBehaviour
    {
        /// <summary>
        /// 減速移動
        /// </summary>
        /// <param name="tra">      動作オブジェクトのRectTransform</param>
        /// <param name="moveSpeed">動作速度</param>
        /// <param name="targetPos">目標座標</param>
        /// <param name="offset">   停止場所の許容範囲</param>
        public static IEnumerator DecelerationMovement(RectTransform tra, float moveSpeed, Vector3 targetPos, float offset = 0.1f)
        {
            Vector3 nowPos = tra.localPosition; //現在の座標
            bool sideways = Mathf.Abs(targetPos.x - nowPos.x) >= Mathf.Abs(targetPos.y - nowPos.y); //X方向に動作？
            while (true)
            {
                if (tra == null) yield break;
                tra.localPosition = Vector3.Lerp(tra.localPosition, targetPos, moveSpeed);
                nowPos = tra.localPosition;

                //---------------------------------------------
                //移動終了
                //---------------------------------------------
                if ((sideways && targetPos.x - offset < nowPos.x && nowPos.x < targetPos.x + offset) ||
                    (!sideways && targetPos.y - offset < nowPos.y && nowPos.y < targetPos.y + offset))
                {
                    tra.localPosition = targetPos;
                    break;
                }
                yield return ComDefine.FIXED_UPDATE;
            }
        }

        /// <summary>
        /// 等速移動
        /// </summary>
        /// <param name="tra">       動作オブジェクトのRectTransform</param>
        /// <param name="moveSpeed"> 動作速度</param>
        /// <param name="targetPos"> 目標座標</param>
        /// <param name="offset">    停止場所の許容範囲</param>
        /// <param name="acceleRate">加速率(等速移動は0.0f指定)</param>
        public static IEnumerator ConstantSpeedMovement(RectTransform tra, float moveSpeed, Vector3 targetPos, float offset = 0.1f, float acceleRate = 0)
        {
            float minSpeed = 0.01f; //最低速度指定(無限ループ対策)
            Vector3 nowPos = tra.localPosition; //現在の座標
            bool sideways = Mathf.Abs(targetPos.x - nowPos.x) >= Mathf.Abs(targetPos.y - nowPos.y); //X方向に動作？
            while (true)
            {
                if (tra == null) yield break;
                moveSpeed += acceleRate;
                if (acceleRate != 0)
                {
                    if (0.0f <= moveSpeed && moveSpeed < minSpeed) moveSpeed = minSpeed;
                    else if (-minSpeed < moveSpeed && moveSpeed <= 0.0f) moveSpeed = -minSpeed;
                }
                tra.localPosition = Vector3.MoveTowards(tra.localPosition, targetPos, moveSpeed);
                nowPos = tra.localPosition;

                //---------------------------------------------
                //移動終了
                //---------------------------------------------
                if ((sideways && targetPos.x - offset < nowPos.x && nowPos.x < targetPos.x + offset) ||
                    (!sideways && targetPos.y - offset < nowPos.y && nowPos.y < targetPos.y + offset))
                {
                    tra.localPosition = targetPos;
                    break;
                }
                yield return ComDefine.FIXED_UPDATE;
            }
        }

        /// <summary>
        /// 曲線移動
        /// </summary>
        /// <param name="tra"></param>
        /// <param name="startPos"></param>
        /// <param name="endPos"></param>
        /// <param name="height"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public static IEnumerator CurveMovement(RectTransform tra, Vector2 startPos, Vector2 endPos, float height, float speed)
        {
            // 中点を求める
            //Vector3 half = end - start * 0.50f + start;
            //half.y += Vector3.up.y + height;

            //StartCoroutine(LerpThrow(target, start, half, end, duration));

            //float startTime = Time.timeSinceLevelLoad;
            //float rate = 0f;
            //while (true)
            //{
            //    if (rate >= 1.0f)
            //        yield break;

            //    float diff = Time.timeSinceLevelLoad - startTime;
            //    rate = diff / (duration / 60f);
            //    target.transform.position = CalcLerpPoint(start, half, end, rate);

            //    yield return null;
            //}
            yield return null;
        }

        /// <summary>
        /// 回転動作
        /// </summary>
        /// <param name="tra">動作オブジェクトのRectTransform</param>
        /// <param name="rotSpeed">拡縮速度</param>
        /// <param name="stopRot"> 回転後の角度(絶対角)</param>
        public static IEnumerator RotateMovement(RectTransform tra, Vector3 rotSpeed, Vector3 stopRot)
        {
            //最も多く動作する軸判定
            int axis = 0;
            if (rotSpeed.x < rotSpeed.y)
                axis = (rotSpeed.y > rotSpeed.z) ? 1 : 2;
            else if (rotSpeed.x < rotSpeed.z)
                axis = 2;

            //回転
            float tolerance = 10.0f;
            while (true)
            {
                if (tra == null) yield break;
                tra.Rotate(rotSpeed.x, rotSpeed.y, rotSpeed.z);
                Vector3 nowRot = tra.localEulerAngles;
                float refRot = nowRot.x;
                float refStopRot = stopRot.x;
                switch (axis)
                {
                    case 1:
                        refRot = nowRot.y;
                        refStopRot = stopRot.y;
                        break;
                    case 2:
                        refRot = nowRot.z;
                        refStopRot = stopRot.z;
                        break;
                }
                if (refStopRot - tolerance <= refRot && refRot <= refStopRot + tolerance) break;
                yield return ComDefine.FIXED_UPDATE;
            }

            //最終角度に合わせる
            tra.localRotation = Quaternion.Euler(stopRot.x, stopRot.y, stopRot.z);
        }

        /// <summary>
        /// 全軸の拡大縮小動作
        /// </summary>
        /// <param name="tra">         動作オブジェクトのRectTransform</param>
        /// <param name="scalingSpeed">拡縮速度(すべて正の数で指定)</param>
        /// <param name="changedScale"> 変更後の拡大率</param>
        /// <returns></returns>
        public static IEnumerator AllScaleChange(RectTransform tra, float scalingSpeed, float changedScale)
        {
            if (tra == null) yield break;
            Vector3 nowScale = tra.localScale;         //現在のスケール
            bool scaleUp = nowScale.x < changedScale;  //拡大？

            //縮小の場合は速度を反転
            if (!scaleUp) scalingSpeed *= -1;

            while (true)
            {
                if (tra == null) yield break;

                //拡縮率更新
                float x = nowScale.x + scalingSpeed;
                float y = nowScale.y + scalingSpeed;
                tra.localScale = new Vector3(x, y, nowScale.z);

                //終了判定
                if ((scaleUp && (x > changedScale || y > changedScale)) ||
                    (!scaleUp && (x < changedScale || y < changedScale)))
                    break;

                //現在のスケール更新
                nowScale = tra.localScale;
                yield return ComDefine.FIXED_UPDATE;
            }
        }

        /// <summary>
        /// 色変更動作(TextMeshProUGUI.color)
        /// </summary>
        /// <param name="img">        変更対象Image</param>
        /// <param name="changeSpeed">変更速度</param>
        /// <param name="colArray">   変更色の配列(0:現在の色)</param>
        /// <param name="chengeCount">ループ回数(配列1周で1カウント、-1指定で無限再生)</param>
        /// <returns></returns>
        public static IEnumerator TextPaletteChange(TextMeshProUGUI txt, float changeSpeed, Color32[] colArray, int chengeCount = 1)
        {
            int loopTimes = 0;                  //繰り返し回数
            int colCount = colArray.Length;     //変更色の数
            bool infinite = chengeCount < 0;    //無限ループ？

            int nowIndex = 0;                   //現在の色
            int nextIndex = 1;                  //次の色
            int judgeRange = 5;                 //判定範囲

            txt.color = colArray[nowIndex];
            while (infinite || loopTimes < chengeCount)
            {
                if (txt == null) yield break;

                //色変更開始
                txt.color = Color.Lerp(txt.color, colArray[nextIndex], changeSpeed);

                //変更終了
                Color32 nowColor = txt.color;
                if (nowColor.r + judgeRange >= colArray[nextIndex].r && colArray[nextIndex].r >= nowColor.r - judgeRange && //R
                    nowColor.g + judgeRange >= colArray[nextIndex].g && colArray[nextIndex].g >= nowColor.g - judgeRange && //G
                    nowColor.b + judgeRange >= colArray[nextIndex].b && colArray[nextIndex].b >= nowColor.b - judgeRange && //B
                    nowColor.a + judgeRange >= colArray[nextIndex].a && colArray[nextIndex].a >= nowColor.a - judgeRange)   //A
                {
                    loopTimes++;
                    nowIndex = nextIndex;
                    nextIndex = (nextIndex + 1 >= colCount) ? 0 : nextIndex + 1;
                }
                yield return ComDefine.FIXED_UPDATE;
            }
            txt.color = colArray[nowIndex];
        }

        /// <summary>
        /// 色変更動作(Image.color)
        /// </summary>
        /// <param name="img">        変更対象Image</param>
        /// <param name="changeSpeed">変更速度</param>
        /// <param name="colArray">   変更色の配列(0:現在の色)</param>
        /// <param name="chengeCount">ループ回数(配列1周で1カウント、-1指定で無限再生)</param>
        /// <returns></returns>
        public static IEnumerator ImagePaletteChange(Image img, float changeSpeed, Color32[] colArray, int chengeCount = 1)
        {
            int loopTimes = 0;                  //繰り返し回数
            int colCount = colArray.Length;     //変更色の数
            bool infinite = chengeCount < 0;    //無限ループ？

            int nowIndex = 0;                   //現在の色
            int nextIndex = 1;                  //次の色
            int judgeRange = 5;                 //判定範囲

            img.color = colArray[nowIndex];
            while (infinite || loopTimes < chengeCount)
            {
                if (img == null) yield break;

                //色変更開始
                img.color = Color.Lerp(img.color, colArray[nextIndex], changeSpeed);

                //変更終了
                Color32 nowColor = img.color;
                if (nowColor.r + judgeRange >= colArray[nextIndex].r && colArray[nextIndex].r >= nowColor.r - judgeRange && //R
                    nowColor.g + judgeRange >= colArray[nextIndex].g && colArray[nextIndex].g >= nowColor.g - judgeRange && //G
                    nowColor.b + judgeRange >= colArray[nextIndex].b && colArray[nextIndex].b >= nowColor.b - judgeRange && //B
                    nowColor.a + judgeRange >= colArray[nextIndex].a && colArray[nextIndex].a >= nowColor.a - judgeRange)   //A
                {
                    loopTimes++;
                    nowIndex = nextIndex;
                    nextIndex = (nextIndex + 1 >= colCount) ? 0 : nextIndex + 1;
                }
                yield return ComDefine.FIXED_UPDATE;
            }
            img.color = colArray[nowIndex];
        }
    }
}