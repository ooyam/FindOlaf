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
        /// �����ړ�
        /// </summary>
        /// <param name="tra">      ����I�u�W�F�N�g��RectTransform</param>
        /// <param name="moveSpeed">���쑬�x</param>
        /// <param name="targetPos">�ڕW���W</param>
        /// <param name="offset">   ��~�ꏊ�̋��e�͈�</param>
        public static IEnumerator DecelerationMovement(RectTransform tra, float moveSpeed, Vector3 targetPos, float offset = 0.1f)
        {
            Vector3 nowPos = tra.localPosition; //���݂̍��W
            bool sideways = Mathf.Abs(targetPos.x - nowPos.x) >= Mathf.Abs(targetPos.y - nowPos.y); //X�����ɓ���H
            while (true)
            {
                if (tra == null) yield break;
                tra.localPosition = Vector3.Lerp(tra.localPosition, targetPos, moveSpeed);
                nowPos = tra.localPosition;

                //---------------------------------------------
                //�ړ��I��
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
        /// �����ړ�
        /// </summary>
        /// <param name="tra">       ����I�u�W�F�N�g��RectTransform</param>
        /// <param name="moveSpeed"> ���쑬�x</param>
        /// <param name="targetPos"> �ڕW���W</param>
        /// <param name="offset">    ��~�ꏊ�̋��e�͈�</param>
        /// <param name="acceleRate">������(�����ړ���0.0f�w��)</param>
        public static IEnumerator ConstantSpeedMovement(RectTransform tra, float moveSpeed, Vector3 targetPos, float offset = 0.1f, float acceleRate = 0)
        {
            float minSpeed = 0.01f; //�Œᑬ�x�w��(�������[�v�΍�)
            Vector3 nowPos = tra.localPosition; //���݂̍��W
            bool sideways = Mathf.Abs(targetPos.x - nowPos.x) >= Mathf.Abs(targetPos.y - nowPos.y); //X�����ɓ���H
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
                //�ړ��I��
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
        /// �Ȑ��ړ�
        /// </summary>
        /// <param name="tra"></param>
        /// <param name="startPos"></param>
        /// <param name="endPos"></param>
        /// <param name="height"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public static IEnumerator CurveMovement(RectTransform tra, Vector2 startPos, Vector2 endPos, float height, float speed)
        {
            // ���_�����߂�
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
        /// ��]����
        /// </summary>
        /// <param name="tra">����I�u�W�F�N�g��RectTransform</param>
        /// <param name="rotSpeed">�g�k���x</param>
        /// <param name="stopRot"> ��]��̊p�x(��Ίp)</param>
        public static IEnumerator RotateMovement(RectTransform tra, Vector3 rotSpeed, Vector3 stopRot)
        {
            //�ł��������삷�鎲����
            int axis = 0;
            if (rotSpeed.x < rotSpeed.y)
                axis = (rotSpeed.y > rotSpeed.z) ? 1 : 2;
            else if (rotSpeed.x < rotSpeed.z)
                axis = 2;

            //��]
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

            //�ŏI�p�x�ɍ��킹��
            tra.localRotation = Quaternion.Euler(stopRot.x, stopRot.y, stopRot.z);
        }

        /// <summary>
        /// �S���̊g��k������
        /// </summary>
        /// <param name="tra">         ����I�u�W�F�N�g��RectTransform</param>
        /// <param name="scalingSpeed">�g�k���x(���ׂĐ��̐��Ŏw��)</param>
        /// <param name="changedScale"> �ύX��̊g�嗦</param>
        /// <returns></returns>
        public static IEnumerator AllScaleChange(RectTransform tra, float scalingSpeed, float changedScale)
        {
            if (tra == null) yield break;
            Vector3 nowScale = tra.localScale;         //���݂̃X�P�[��
            bool scaleUp = nowScale.x < changedScale;  //�g��H

            //�k���̏ꍇ�͑��x�𔽓]
            if (!scaleUp) scalingSpeed *= -1;

            while (true)
            {
                if (tra == null) yield break;

                //�g�k���X�V
                float x = nowScale.x + scalingSpeed;
                float y = nowScale.y + scalingSpeed;
                tra.localScale = new Vector3(x, y, nowScale.z);

                //�I������
                if ((scaleUp && (x > changedScale || y > changedScale)) ||
                    (!scaleUp && (x < changedScale || y < changedScale)))
                    break;

                //���݂̃X�P�[���X�V
                nowScale = tra.localScale;
                yield return ComDefine.FIXED_UPDATE;
            }
        }

        /// <summary>
        /// �F�ύX����(TextMeshProUGUI.color)
        /// </summary>
        /// <param name="img">        �ύX�Ώ�Image</param>
        /// <param name="changeSpeed">�ύX���x</param>
        /// <param name="colArray">   �ύX�F�̔z��(0:���݂̐F)</param>
        /// <param name="chengeCount">���[�v��(�z��1����1�J�E���g�A-1�w��Ŗ����Đ�)</param>
        /// <returns></returns>
        public static IEnumerator TextPaletteChange(TextMeshProUGUI txt, float changeSpeed, Color32[] colArray, int chengeCount = 1)
        {
            int loopTimes = 0;                  //�J��Ԃ���
            int colCount = colArray.Length;     //�ύX�F�̐�
            bool infinite = chengeCount < 0;    //�������[�v�H

            int nowIndex = 0;                   //���݂̐F
            int nextIndex = 1;                  //���̐F
            int judgeRange = 5;                 //����͈�

            txt.color = colArray[nowIndex];
            while (infinite || loopTimes < chengeCount)
            {
                if (txt == null) yield break;

                //�F�ύX�J�n
                txt.color = Color.Lerp(txt.color, colArray[nextIndex], changeSpeed);

                //�ύX�I��
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
        /// �F�ύX����(Image.color)
        /// </summary>
        /// <param name="img">        �ύX�Ώ�Image</param>
        /// <param name="changeSpeed">�ύX���x</param>
        /// <param name="colArray">   �ύX�F�̔z��(0:���݂̐F)</param>
        /// <param name="chengeCount">���[�v��(�z��1����1�J�E���g�A-1�w��Ŗ����Đ�)</param>
        /// <returns></returns>
        public static IEnumerator ImagePaletteChange(Image img, float changeSpeed, Color32[] colArray, int chengeCount = 1)
        {
            int loopTimes = 0;                  //�J��Ԃ���
            int colCount = colArray.Length;     //�ύX�F�̐�
            bool infinite = chengeCount < 0;    //�������[�v�H

            int nowIndex = 0;                   //���݂̐F
            int nextIndex = 1;                  //���̐F
            int judgeRange = 5;                 //����͈�

            img.color = colArray[nowIndex];
            while (infinite || loopTimes < chengeCount)
            {
                if (img == null) yield break;

                //�F�ύX�J�n
                img.color = Color.Lerp(img.color, colArray[nextIndex], changeSpeed);

                //�ύX�I��
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