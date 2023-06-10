using System;
using UnityEngine;

namespace Common
{
    public class Global : SingletonMonoBehaviourDontDestroy<Global>
    {
        public ComDefine.GameMode GameMode { get; set; } = ComDefine.GameMode.Speed;

        void Start()
        {
        }

        void Update()
        {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
            if (Input.GetKey(KeyCode.Escape))
                QuitGame();
#endif
        }

        /// <summary>
        /// �V�[���ڊ�
        /// </summary>
        /// <param name="sceneName">�V�[����</param>
        /// <param name="colorType">�t�F�[�h���̐F</param>
        public void SceneChange(string sceneName, SceneFader.FadeColorType colorType)
        {
            SceneFader.instance.FadeColor = colorType;
            SceneFader.instance.SceneChangeFade(sceneName);
        }

        /// <summary>
        /// �Q�[���I��
        /// </summary>
        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}