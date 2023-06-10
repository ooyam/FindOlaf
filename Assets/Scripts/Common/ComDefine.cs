using System;
using UnityEngine;

namespace Common
{
    public static class ComDefine
    {
        //--- �萔 ---//
        #region

        /// <summary>
        /// ���[�h�̎��
        /// </summary>
        public enum GameMode
        {
            Speed,  // �X�s�[�h���[�h
            NoMiss, // �m�[�~�X���[�h
        }

        //��ʃT�C�Y
        public const float SCREEN_WIDTH = 1920f;
        public const float SCREEN_HEIGHT = 1080f;

        //�V�[����
        public const string MAIN_MENU_SCENE_NAME = @"MainMenu";
        public const string GAME_SCENE_NAME = @"Game";

        //�t���[���֘A
        public const float ONE_FRAME_TIMES = 0.02f;
        public static readonly WaitForFixedUpdate FIXED_UPDATE = new();
        #endregion


        //--- �֐� ---//
        #region

        ///// <summary>
        ///// �����p�l���̃X�v���C�g�擾
        ///// </summary>
        ///// <param name="aType">����</param>
        ///// <param name="eType">�\��</param>
        ///// <param name="pType">�p�l��</param>
        ///// <returns>�p�l���̃X�v���C�g</returns>
        //public static Sprite GetAnimalPanelSprite(AnimalType aType, ExpressionType eType, PanelColorType cType, PanelType pType)
        //{
        //    return Resources.Load<Sprite>(string.Format("{0}{1}/{2}/{3}/{4}", PANEL_DIR, cType, pType, aType, eType.ToString().ToLower()));
        //}

        ///// <summary>
        ///// �����p�l���̃t�B���^�[�X�v���C�g�擾
        ///// </summary>
        ///// <param name="pType">�p�l���̎��</param>
        ///// <returns></returns>
        //public static Sprite GetPanelWhiteSprite(PanelType pType)
        //{
        //    string fileName = PANEL_DIR + PANEL_FILTER_FILE;
        //    switch (pType)
        //    {
        //        //�p�X�e��
        //        case PanelType.Pastel:
        //        case PanelType.HeadbandPastel:
        //            return Resources.Load<Sprite>(fileName + PanelType.Pastel.ToString().ToLower());

        //        //���̑�(�m�[�}��)
        //        default:
        //            return Resources.Load<Sprite>(fileName + PanelType.Normal.ToString().ToLower());
        //    }
        //}
#endregion
    }
}
