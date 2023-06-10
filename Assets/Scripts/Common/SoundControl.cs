using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class SoundControl : SingletonMonoBehaviour<SoundControl>
    {
        //�����t�@�C���i�[�f�B���N�g��
        const string CLIP_DIR = @"SoundClip/";
        const string BGM_DIR = CLIP_DIR + @"Bgm/";
        const string SE_DIR = CLIP_DIR + @"Se/";

        //�f�[�^�I�u�W�F�N�g��
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

        //���ʃt�F�[�h����
        const float BGM_FADE_IN_TIME = 1.0f;
        const float BGM_FADE_OUT_TIME = 0.5f;

        //BGM�s�b�`�ύX�萔
        const float BGM_PITCH_CHANGE_VALUE = 0.25f;

        //SE�萔
        const int SE_PLAY_MAX = 30;                     //SE�����Đ��ő吔
        const int SE_MAX_DUPLICATE = 3;                 //����SE�̍ő�d���Đ���
        const float TIME_CONSIDERED_DUPLICATE = 0.02f;  //�d���Ƃ݂Ȃ�����

        //AudioSource
        static AudioSource _bgmAudio;
        static AudioSource[] _seAudioAry;

        //�f�[�^
        static Dictionary<BgmType, BgmData> _bgmInfoDic;
        static Dictionary<SeType, SeData> _seInfoDic;
        static Dictionary<BgmType, AudioClip> _bgmClipDic;
        static Dictionary<SeType, AudioClip> _seClipDic;

        //�Đ�����BGM
        static BgmType _nowPlayBgm;

        //BGM�t�F�[�h���̃R���[�`��
        static Coroutine _bgmFadeCor;

        void Start()
        {
            //BGM�pAudioSource�̎擾
            _bgmAudio = GetComponent<AudioSource>();

            //SE�pAudioSource�̒ǉ�
            _seAudioAry = new AudioSource[SE_PLAY_MAX];
            for (int i = 0; i < SE_PLAY_MAX; i++)
            {
                _seAudioAry[i] = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
            }

            //�f�[�^�̎擾
            Bgm bgmData = Resources.Load(BGM_DATA_OBJ) as Bgm;
            Se seData = Resources.Load(SE_DATA_OBJ) as Se;
            BgmData[] bgmDataAry = bgmData.dataArray;
            SeData[] seDataAry = seData.dataArray;

            //BGM�N���b�v,���擾
            _bgmInfoDic = new Dictionary<BgmType, BgmData>();
            _bgmClipDic = new Dictionary<BgmType, AudioClip>();
            for (int i = 0; i < bgmDataAry.Length; i++)
            {
                _bgmInfoDic.Add(bgmDataAry[i].BgmType, bgmDataAry[i]);
                _bgmClipDic.Add(bgmDataAry[i].BgmType, (AudioClip)Resources.Load(BGM_DIR + bgmDataAry[i].ClipName));
            }

            //SE�N���b�v,���擾
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
        /// BGM�J�n(�t�F�[�h�L)
        /// </summary>
        /// <param name="bgmType">BGM�̎��</param>
        public void BgmFadeStart(BgmType bgmType)
        {
            //�f�[�^�擾
            _nowPlayBgm = bgmType;

            //��BGM���t�F�[�h���̏ꍇ�̓R���[�`����~
            BgmFadeBreak();

            _bgmAudio.clip = _bgmClipDic[bgmType];
            _bgmAudio.Play();
            _bgmAudio.volume = 0f;
            _bgmFadeCor = instance.StartCoroutine(SetBgmVolumeFade(_bgmInfoDic[bgmType].Volume));
        }

        /// <summary>
        /// BGM�ĊJ(�t�F�[�h�L)
        /// </summary>
        public void BgmFadeRestart()
        {
            //��BGM���t�F�[�h���̏ꍇ�̓R���[�`����~
            BgmFadeBreak();
            _bgmAudio.volume = 0f;
            _bgmFadeCor = instance.StartCoroutine(SetBgmVolumeFade(_bgmInfoDic[_nowPlayBgm].Volume));
        }

        /// <summary>
        /// BGM�I��(�t�F�[�h�L)
        /// </summary>
        /// <param name="_BGMName">�N���b�v��</param>
        public IEnumerator BgmFadeStop()
        {
            _bgmFadeCor = instance.StartCoroutine(SetBgmVolumeFade(0f));
            yield return _bgmFadeCor;
        }

        /// <summary>
        /// BGM�I��(�t�F�[�h��)
        /// </summary>
        /// <param name="_BGMName">�N���b�v��</param>
        public void BgmStop()
        {
            SetBgmVolume(0f);
        }

        /// <summary>
        /// BGM���ʃt�F�[�h
        /// </summary>
        /// <param name="volume"></param>
        IEnumerator SetBgmVolumeFade(float volume)
        {
            yield return null;
            float offset = volume - _bgmAudio.volume;
            if (offset == 0)
            {
                //�t�F�[�h�K�v�Ȃ�
                yield break;
            }
            else if (offset > 0)
            {
                //�t�F�[�h�C��
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
                //�t�F�[�h�A�E�g
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
        /// BGM���ʐݒ�
        /// </summary>
        /// <param name="volume"></param>
        void SetBgmVolume(float volume) => _bgmAudio.volume = volume * Preferences.instance.BgmVolume;
        public void SetBgmVolume() => SetBgmVolume(_bgmInfoDic[_nowPlayBgm].Volume);

        /// <summary>
        /// BGM�s�b�`�㏸
        /// </summary>
        public void BgmPitchUp() => _bgmAudio.pitch += BGM_PITCH_CHANGE_VALUE;

        /// <summary>
        /// BGM�s�b�`���~
        /// </summary>
        public void BgmPitchDown() => _bgmAudio.pitch -= BGM_PITCH_CHANGE_VALUE;

        /// <summary>
        /// BGM�s�b�`���Z�b�g
        /// </summary>
        public void BgmPitchReset() => _bgmAudio.pitch = 1f;

        /// <summary>
        /// BGM�̃t�F�[�h�R���[�`����~
        /// </summary>
        void BgmFadeBreak()
        {
            if (_bgmFadeCor != null) instance.StopCoroutine(_bgmFadeCor);
        }
        #endregion


        //--- SE ---//
        #region

        /// <summary>
        /// �g�p�\��AudioSource�̊Ǘ��ԍ��擾
        /// </summary>
        /// <returns>�g�p�\��AudioSource</returns>
        AudioSource GetSeAudioSource(AudioClip clip)
        {
            AudioSource returnAudioSrc = null;
            int duplicateSeCnt = 0;
            for (int i = 0; i < SE_PLAY_MAX; i++)
            {
                //�Đ�����AudioSource
                if (_seAudioAry[i].isPlaying)
                {
                    //����SE�������^�C�~���O�ŗ���Ă���ꍇ
                    if (_seAudioAry[i].clip == clip && _seAudioAry[i].time < TIME_CONSIDERED_DUPLICATE)
                    {
                        //��萔�𒴂����ꍇ��null��Ԃ�
                        if (++duplicateSeCnt >= SE_MAX_DUPLICATE) return null;
                    }
                }
                //���Đ�
                else
                {
                    //AudioSource�̎擾
                    returnAudioSrc ??= _seAudioAry[i];
                }
            }
            return returnAudioSrc;
        }

        /// <summary>
        /// SE�Đ�
        /// </summary>
        AudioSource SeOneShot(AudioClip clip, float volume)
        {
            //�g�p�\��AudioSource���Ȃ��ꍇ�͍Đ����Ȃ�
            AudioSource audio = GetSeAudioSource(clip);
            if (audio == null) return null;

            //����,�N���b�v�ݒ�,�Đ�
            audio.volume = volume;
            audio.clip = clip;
            audio.Play();
            return audio;
        }

        /// <summary>
        /// SE�Đ�
        /// </summary>
        /// <param name="seType">SE�̎��</param>
        /// <returns>�N���b�v��ݒ肵��AudioSource</returns>
        public AudioSource SeOneShot(SeType seType)
        {
            return SeOneShot(_seClipDic[seType], _seInfoDic[seType].Volume * Preferences.instance.SeVolume);
        }

        /// <summary>
        /// SE���~�߂�
        /// </summary>
        /// <param name="audio">�~�߂�AudioSource</param>
        public void SeStop(AudioSource audio)
        {
            if (audio == null) return;
            audio.Stop();
        }

        /// <summary>
        /// SE�����ׂĎ~�߂�
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