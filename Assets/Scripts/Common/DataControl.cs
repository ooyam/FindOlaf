using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class DataControl : SingletonMonoBehaviour<DataControl>
{
    const string PRF_FILE_PATH = "Preferences";

    void Start()
    {
        InitializeSaveData();
        LoadPreferencesData();
    }

    /// <summary>
    /// �Z�[�u�f�[�^�̏�����
    /// </summary>
    void InitializeSaveData()
    {
        PreferencesInitializeSaveData();
    }

    //--- �X�R�A ---//
    #region

    #endregion

    //--- ���ݒ� ---//
    #region

    // ���ݒ�̃Z�[�u�L�[
    public enum PrfSaveKey
    {
        BgmVolume   = 1 << 0,
        SeVolume    = 1 << 1,
    }

    /// <summary>
    /// ���ݒ�̃Z�[�u�f�[�^������
    /// </summary>
    void PreferencesInitializeSaveData()
    {
        if (PlayerPrefs.HasKey(PrfSaveKey.BgmVolume.ToString()))
        {
            return;
        }
        SetPreferencesPlayerPrefsAll();
        PlayerPrefs.Save();
    }

    /// <summary>
    /// ���ݒ�̃Z�[�u�f�[�^�ݒ�
    /// </summary>
    void SetPreferencesPlayerPrefsAll()
    {
        PrfSaveKey prfSaveKeys = (
            PrfSaveKey.BgmVolume |
            PrfSaveKey.SeVolume);
        SetPreferencesPlayerPrefs(prfSaveKeys);
    }

    /// <summary>
    /// ���ݒ�̃Z�[�u�f�[�^�ݒ�
    /// </summary>
    void SetPreferencesPlayerPrefs(PrfSaveKey prfSaveKeys)
    {
        if (IsSave(PrfSaveKey.BgmVolume)) PlayerPrefs.SetFloat(PrfSaveKey.BgmVolume.ToString(), 0f);

        bool IsSave(PrfSaveKey key) => (prfSaveKeys & key) == key;
    }

    /// <summary>
    /// ���ݒ�f�[�^�̃Z�[�u
    /// </summary>
    public void SavePreferencesData(PrfSaveKey[] prfSaveKeyAry)
    {
        PrfSaveKey prfSaveKeys = 0;
        for (int i = 0; i < prfSaveKeyAry.Length; i++)
        {
            prfSaveKeys |= prfSaveKeyAry[i];
        }
        SetPreferencesPlayerPrefs(prfSaveKeys);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// ���ݒ�f�[�^�̃��[�h
    /// </summary>
    public void LoadPreferencesData()
    {
        var bgmVolume = PlayerPrefs.GetFloat(PrfSaveKey.BgmVolume.ToString());
    }
#endregion
}
