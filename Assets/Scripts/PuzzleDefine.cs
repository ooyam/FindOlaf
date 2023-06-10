using UnityEngine;
using System;

namespace Puzzle
{
    public static class PuzzleDefine
    {
        //----------�萔---------//

        /// <summary>
        /// �t���O
        /// </summary>
        public enum FlagType
        {
            GameStart       = 1 << 0,   //�Q�[���J�n
            GameEnd         = 1 << 1,   //�Q�[���I��
            TimeUp          = 1 << 2,   //�^�C���A�b�v
            ProblemEnd      = 1 << 3,   //�K���萔�I��

            Penalty         = 1 << 4,   //�y�i���e�B��
            FeverEffect     = 1 << 5,   //�t�B�[�o�[�O���o��
            Fever           = 1 << 6,   //�t�B�[�o�[��
            AllEraseFailure = 1 << 7,   //�S�������s(�^�C���A�^�b�N)
            Miss            = 1 << 8,   //�~�X��(�t���b�V��)

            Pause           = 1 << 9,   //�|�[�Y��
        }
        static FlagType _flags;

        /// <summary>
        /// �����̃X�v���C�g�̐F
        /// </summary>
        public enum NumSpriteColorsType
        {
            Black,  //��
            Blue,   //��
            Orange, //��

            Count
        }

        public const string OBJ_TAG_PANEL = "Panel";
        public const string OBJ_TAG_NAVI_PANEL = "NaviPanel";

        const string NUMBERS_SPRITE_DIR = "Numbers/";  //�����X�v���C�g�i�[�f�B���N�g��

        //-----------------------//



        //---------�֐�---------//

        /// <summary>
        /// �t���O���Z�b�g
        /// </summary>
        public static void ResetFlag() => _flags = 0;

        /// <summary>
        /// �t���O�̎擾
        /// </summary>
        public static bool GetFlag(FlagType type) => (_flags & type) == type;

        /// <summary>
        /// �t���O�̐ݒ�
        /// </summary>
        public static void SetFlag(FlagType type, bool on)
        {
            if (on) _flags |= type;
            else _flags &= ~type;
        }

        /// <summary>
        /// �����X�v���C�g�̎擾
        /// </summary>
        public static Sprite[][] GetNumberSpritesAry()
        {
            //�����̃X�v���C�g�擾
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