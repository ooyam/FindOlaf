using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class EffectManager : MonoBehaviour
    {
        [SerializeField, Header("エフェクト生成親")]
        Transform _parent;

        [SerializeField, Header("エフェクトプレハブ")]
        GameObject[] _effectPreAry;

        GameObject[] _effectObjAry;
        Animator[] _effectAniAry;

        public enum Effect
        {
            GameStart,  // ゲーム開始

            Count
        }

        public void Initialize()
        {
            _effectObjAry = new GameObject[(int)Effect.Count];
            _effectAniAry = new Animator[(int)Effect.Count];
            for (int i = 0; i < (int)Effect.Count; i++)
            {
                _effectObjAry[i] = UIControl.instance.OpenUI(_effectPreAry[i], _parent);
                _effectAniAry[i] = _effectObjAry[i].GetComponent<Animator>();
            }
        }

        public IEnumerator StartEffect(Effect effect, Action callBack = null)
        {
            yield return AnimationController.AnimationStart(_effectAniAry[(int)effect], effect.ToString(), callBack);
            callBack?.Invoke();
        }
    }
}
