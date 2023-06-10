using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class AnimationController : MonoBehaviour
    {
        const int ANIME_LAYER = 0;          //レイヤー
        const float ANIME_START_TIME = 0f;  //アニメ開始位置

        //アニメーションステート名
        public const string STATE_NAME_EMPTY = "Empty"; //初期状態

        /// <summary>
        /// アニメーション再生
        /// </summary>
        /// <param name="ani">Animator</param>
        /// <param name="stateName">ステート名</param>
        public static IEnumerator AnimationStart(Animator ani, string stateName, Action action = null)
        {
            //アニメーション開始
            ani.Play(stateName, ANIME_LAYER, ANIME_START_TIME);

            //stateが切り替わるまで待機
            yield return new WaitUntil(stateCheck);

            //アニメーション終了待機
            yield return new WaitWhile(stateCheck);

            //コールバッグ
            action?.Invoke();

            //ステート監視関数
            bool stateCheck() => ani.GetCurrentAnimatorStateInfo(0).IsName(stateName);
        }

        /// <summary>
        /// アニメーション再生(終了待機無し)
        /// </summary>
        /// <param name="ani">Animator</param>
        /// <param name="stateName">ステート名</param>
        public static IEnumerator AnimationChange(Animator ani, string stateName = STATE_NAME_EMPTY, Action action = null)
        {
            //アニメーション開始
            ani.Play(stateName, ANIME_LAYER, ANIME_START_TIME);

            //stateが切り替わるまで待機
            yield return new WaitUntil(() => ani.GetCurrentAnimatorStateInfo(0).IsName(stateName));

            // コールバッグ
            action?.Invoke();
        }
    }
}