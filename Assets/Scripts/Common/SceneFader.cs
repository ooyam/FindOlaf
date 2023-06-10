using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace Common
{
    public class SceneFader : SingletonMonoBehaviour<SceneFader>
    {
        //フェードの色
        public enum FadeColorType
        { Black, White }
        public FadeColorType FadeColor { get; set; }

        Coroutine _fadeCor = null;

        //シーンフェード速度
        const float FADE_SPEED = 0.12f;

        static Image _filter;
        static readonly Color32[] _fadeOutBlack = new Color32[] { Color.clear, Color.black };
        static readonly Color32[] _fadeInBlack  = new Color32[] { Color.black, Color.clear };
        static readonly Color32[] _fadeOutWhite = new Color32[] { new Color32(255, 255, 255, 0), Color.white };
        static readonly Color32[] _fadeInWhite  = new Color32[] { Color.white, new Color32(255, 255, 255, 0) };

        void Start()
        {
            _filter = GetComponent<Image>();
        }

        /// <summary>
        /// フェードでシーン移管
        /// </summary>
        /// <param name="sceneName">シーン名</param>
        public void SceneChangeFade(string sceneName)
        {
            StartCoroutine(FadeOut(sceneName));
        }

        /// <summary>
        /// フェードアウト
        /// </summary>
        /// <param name="sceneName">シーン名</param>
        IEnumerator FadeOut(string sceneName)
        {
            if (_fadeCor != null) yield break;

            //色設定
            Color32[] fadeColors = _fadeOutBlack;
            switch (FadeColor)
            {
                case FadeColorType.White:
                    fadeColors = _fadeOutWhite;
                    break;
            }

            //フェード開始
            _fadeCor = StartCoroutine(ObjectMove.ImagePaletteChange(_filter, FADE_SPEED, fadeColors));
            yield return _fadeCor;
            _fadeCor = null;
            SceneManager.LoadScene(sceneName);
            yield return null;
            StartCoroutine(FadeIn());
        }

        /// <summary>
        /// フェードイン
        /// </summary>
        IEnumerator FadeIn()
        {
            if (_fadeCor != null) yield break;

            //色設定
            Color32[] fadeColors = _fadeInBlack;
            switch (FadeColor)
            {
                case FadeColorType.White:
                    fadeColors = _fadeInWhite;
                    break;
            }

            //フェード開始
            _fadeCor = StartCoroutine(ObjectMove.ImagePaletteChange(_filter, FADE_SPEED, fadeColors));
            yield return _fadeCor;
            _fadeCor = null;
        }
    }
}