using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class SoundControl : SingletonMonoBehaviour<SoundControl>
    {
        //音源ファイル格納ディレクトリ
        const string CLIP_DIR = @"SoundClip/";
        const string BGM_DIR = CLIP_DIR + @"Bgm/";
        const string SE_DIR = CLIP_DIR + @"Se/";

        //データオブジェクト名
        const string DATA_OBJ_DIR = @"Data/Sound/";
        const string BGM_DATA_OBJ = DATA_OBJ_DIR + @"Bgm";
        const string SE_DATA_OBJ = DATA_OBJ_DIR + @"Se";

        public enum BgmType
        {
            MainMenu,
            Stage1,
            Stage2,
            Stage3,
            Stage4,

            Count
        }

        public enum SeType
        {
            BtnYes,
            BtnNo,

            Count
        }

        //音量フェード時間
        const float BGM_FADE_IN_TIME = 1.0f;
        const float BGM_FADE_OUT_TIME = 0.5f;

        //BGMピッチ変更定数
        const float BGM_PITCH_CHANGE_VALUE = 0.25f;

        //SE定数
        const int SE_PLAY_MAX = 30;                     //SE同時再生最大数
        const int SE_MAX_DUPLICATE = 3;                 //同じSEの最大重複再生数
        const float TIME_CONSIDERED_DUPLICATE = 0.02f;  //重複とみなす時間

        //AudioSource
        static AudioSource _bgmAudio;
        static AudioSource[] _seAudioAry;

        //データ
        static Dictionary<BgmType, BgmData> _bgmInfoDic;
        static Dictionary<SeType, SeData> _seInfoDic;
        static Dictionary<BgmType, AudioClip> _bgmClipDic;
        static Dictionary<SeType, AudioClip> _seClipDic;

        //再生中のBGM
        static BgmType _nowPlayBgm;

        //BGMフェード中のコルーチン
        static Coroutine _bgmFadeCor;

        void Start()
        {
            //BGM用AudioSourceの取得
            _bgmAudio = GetComponent<AudioSource>();

            //SE用AudioSourceの追加
            _seAudioAry = new AudioSource[SE_PLAY_MAX];
            for (int i = 0; i < SE_PLAY_MAX; i++)
            {
                _seAudioAry[i] = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
            }

            //データの取得
            Bgm bgmData = Resources.Load(BGM_DATA_OBJ) as Bgm;
            Se seData = Resources.Load(SE_DATA_OBJ) as Se;
            BgmData[] bgmDataAry = bgmData.dataArray;
            SeData[] seDataAry = seData.dataArray;

            //BGMクリップ,情報取得
            _bgmInfoDic = new Dictionary<BgmType, BgmData>();
            _bgmClipDic = new Dictionary<BgmType, AudioClip>();
            for (int i = 0; i < bgmDataAry.Length; i++)
            {
                _bgmInfoDic.Add(bgmDataAry[i].BgmType, bgmDataAry[i]);
                _bgmClipDic.Add(bgmDataAry[i].BgmType, (AudioClip)Resources.Load(BGM_DIR + bgmDataAry[i].ClipName));
            }

            //SEクリップ,情報取得
            _seInfoDic = new Dictionary<SeType, SeData>();
            _seClipDic = new Dictionary<SeType, AudioClip>();
            for (int i = 0; i < seDataAry.Length; i++)
            {
                _seInfoDic.Add(seDataAry[i].SeType, seDataAry[i]);
                _seClipDic.Add(seDataAry[i].SeType, (AudioClip)Resources.Load(SE_DIR + seDataAry[i].ClipName));
            }
        }

        //--- BGM ---//
        #region

        /// <summary>
        /// BGM開始(フェード有)
        /// </summary>
        /// <param name="bgmType">BGMの種類</param>
        public void BgmFadeStart(BgmType bgmType)
        {
            //データ取得
            _nowPlayBgm = bgmType;

            //他BGMがフェード中の場合はコルーチン停止
            BgmFadeBreak();

            _bgmAudio.clip = _bgmClipDic[bgmType];
            _bgmAudio.Play();
            _bgmAudio.volume = 0f;
            _bgmFadeCor = instance.StartCoroutine(SetBgmVolumeFade(_bgmInfoDic[bgmType].Volume));
        }

        /// <summary>
        /// BGM再開(フェード有)
        /// </summary>
        public void BgmFadeRestart()
        {
            //他BGMがフェード中の場合はコルーチン停止
            BgmFadeBreak();
            _bgmAudio.volume = 0f;
            _bgmFadeCor = instance.StartCoroutine(SetBgmVolumeFade(_bgmInfoDic[_nowPlayBgm].Volume));
        }

        /// <summary>
        /// BGM終了(フェード有)
        /// </summary>
        /// <param name="_BGMName">クリップ名</param>
        public IEnumerator BgmFadeStop()
        {
            _bgmFadeCor = instance.StartCoroutine(SetBgmVolumeFade(0f));
            yield return _bgmFadeCor;
        }

        /// <summary>
        /// BGM終了(フェード無)
        /// </summary>
        /// <param name="_BGMName">クリップ名</param>
        public void BgmStop()
        {
            SetBgmVolume(0f);
        }

        /// <summary>
        /// BGM音量フェード
        /// </summary>
        /// <param name="volume"></param>
        IEnumerator SetBgmVolumeFade(float volume)
        {
            yield return null;
            float offset = volume - _bgmAudio.volume;
            if (offset == 0)
            {
                //フェード必要なし
                yield break;
            }
            else if (offset > 0)
            {
                //フェードイン
                float value = offset / (BGM_FADE_IN_TIME / ComDefine.ONE_FRAME_TIMES);
                float nowVolume = _bgmAudio.volume;
                while (true)
                {
                    nowVolume += value;
                    SetBgmVolume(nowVolume);
                    yield return ComDefine.FIXED_UPDATE;
                    if (nowVolume >= volume) break;
                }
            }
            else
            {
                //フェードアウト
                float value = offset / (BGM_FADE_OUT_TIME / ComDefine.ONE_FRAME_TIMES);
                float nowVolume = _bgmAudio.volume;
                while (true)
                {
                    nowVolume += value;
                    SetBgmVolume(nowVolume);
                    yield return ComDefine.FIXED_UPDATE;
                    if (nowVolume <= volume) break;
                }
            }

            SetBgmVolume(volume);
        }

        /// <summary>
        /// BGM音量設定
        /// </summary>
        /// <param name="volume"></param>
        void SetBgmVolume(float volume) => _bgmAudio.volume = volume * Preferences.instance.BgmVolume;
        public void SetBgmVolume() => SetBgmVolume(_bgmInfoDic[_nowPlayBgm].Volume);

        /// <summary>
        /// BGMピッチ上昇
        /// </summary>
        public void BgmPitchUp() => _bgmAudio.pitch += BGM_PITCH_CHANGE_VALUE;

        /// <summary>
        /// BGMピッチ下降
        /// </summary>
        public void BgmPitchDown() => _bgmAudio.pitch -= BGM_PITCH_CHANGE_VALUE;

        /// <summary>
        /// BGMピッチリセット
        /// </summary>
        public void BgmPitchReset() => _bgmAudio.pitch = 1f;

        /// <summary>
        /// BGMのフェードコルーチン停止
        /// </summary>
        void BgmFadeBreak()
        {
            if (_bgmFadeCor != null) instance.StopCoroutine(_bgmFadeCor);
        }
        #endregion


        //--- SE ---//
        #region

        /// <summary>
        /// 使用可能なAudioSourceの管理番号取得
        /// </summary>
        /// <returns>使用可能なAudioSource</returns>
        AudioSource GetSeAudioSource(AudioClip clip)
        {
            AudioSource returnAudioSrc = null;
            int duplicateSeCnt = 0;
            for (int i = 0; i < SE_PLAY_MAX; i++)
            {
                //再生中のAudioSource
                if (_seAudioAry[i].isPlaying)
                {
                    //同じSEが同じタイミングで流れている場合
                    if (_seAudioAry[i].clip == clip && _seAudioAry[i].time < TIME_CONSIDERED_DUPLICATE)
                    {
                        //一定数を超えた場合はnullを返す
                        if (++duplicateSeCnt >= SE_MAX_DUPLICATE) return null;
                    }
                }
                //未再生
                else
                {
                    //AudioSourceの取得
                    returnAudioSrc ??= _seAudioAry[i];
                }
            }
            return returnAudioSrc;
        }

        /// <summary>
        /// SE再生
        /// </summary>
        AudioSource SeOneShot(AudioClip clip, float volume)
        {
            //使用可能なAudioSourceがない場合は再生しない
            AudioSource audio = GetSeAudioSource(clip);
            if (audio == null) return null;

            //音量,クリップ設定,再生
            audio.volume = volume;
            audio.clip = clip;
            audio.Play();
            return audio;
        }

        /// <summary>
        /// SE再生
        /// </summary>
        /// <param name="seType">SEの種類</param>
        /// <returns>クリップを設定したAudioSource</returns>
        public AudioSource SeOneShot(SeType seType)
        {
            return SeOneShot(_seClipDic[seType], _seInfoDic[seType].Volume * Preferences.instance.SeVolume);
        }

        /// <summary>
        /// SEを止める
        /// </summary>
        /// <param name="audio">止めるAudioSource</param>
        public void SeStop(AudioSource audio)
        {
            if (audio == null) return;
            audio.Stop();
        }

        /// <summary>
        /// SEをすべて止める
        /// </summary>
        public void SE_StopAll()
        {
            for (int i = 0; i < SE_PLAY_MAX; i++)
            {
                if (!_seAudioAry[i].isPlaying) continue;
                SeStop(_seAudioAry[i]);
            }
        }
        #endregion
    }
}