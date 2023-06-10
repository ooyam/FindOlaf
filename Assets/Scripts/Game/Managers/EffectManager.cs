using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class EffectManager : MonoBehaviour
    {
        [SerializeField, Header("�G�t�F�N�g�����e")]
        Transform _parent;

        [SerializeField, Header("�G�t�F�N�g�v���n�u")]
        GameObject[] _effectPreAry;

        GameObject[] _effectObjAry;
        Animator[] _effectAniAry;

        public enum Effect
        {
            GameStart,  // �Q�[���J�n

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
