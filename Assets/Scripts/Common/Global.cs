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
        /// シーン移管
        /// </summary>
        /// <param name="sceneName">シーン名</param>
        /// <param name="colorType">フェード時の色</param>
        public void SceneChange(string sceneName, SceneFader.FadeColorType colorType)
        {
            SceneFader.instance.FadeColor = colorType;
            SceneFader.instance.SceneChangeFade(sceneName);
        }

        /// <summary>
        /// ゲーム終了
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