using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class AnimationController : MonoBehaviour
    {
        const int ANIME_LAYER = 0;          //���C���[
        const float ANIME_START_TIME = 0f;  //�A�j���J�n�ʒu

        //�A�j���[�V�����X�e�[�g��
        public const string STATE_NAME_EMPTY = "Empty"; //�������

        /// <summary>
        /// �A�j���[�V�����Đ�
        /// </summary>
        /// <param name="ani">Animator</param>
        /// <param name="stateName">�X�e�[�g��</param>
        public static IEnumerator AnimationStart(Animator ani, string stateName, Action action = null)
        {
            //�A�j���[�V�����J�n
            ani.Play(stateName, ANIME_LAYER, ANIME_START_TIME);

            //state���؂�ւ��܂őҋ@
            yield return new WaitUntil(stateCheck);

            //�A�j���[�V�����I���ҋ@
            yield return new WaitWhile(stateCheck);

            //�R�[���o�b�O
            action?.Invoke();

            //�X�e�[�g�Ď��֐�
            bool stateCheck() => ani.GetCurrentAnimatorStateInfo(0).IsName(stateName);
        }

        /// <summary>
        /// �A�j���[�V�����Đ�(�I���ҋ@����)
        /// </summary>
        /// <param name="ani">Animator</param>
        /// <param name="stateName">�X�e�[�g��</param>
        public static IEnumerator AnimationChange(Animator ani, string stateName = STATE_NAME_EMPTY, Action action = null)
        {
            //�A�j���[�V�����J�n
            ani.Play(stateName, ANIME_LAYER, ANIME_START_TIME);

            //state���؂�ւ��܂őҋ@
            yield return new WaitUntil(() => ani.GetCurrentAnimatorStateInfo(0).IsName(stateName));

            // �R�[���o�b�O
            action?.Invoke();
        }
    }
}