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
    /// セーブデータの初期化
    /// </summary>
    void InitializeSaveData()
    {
        PreferencesInitializeSaveData();
    }

    //--- スコア ---//
    #region

    #endregion

    //--- 環境設定 ---//
    #region

    // 環境設定のセーブキー
    public enum PrfSaveKey
    {
        BgmVolume   = 1 << 0,
        SeVolume    = 1 << 1,
    }

    /// <summary>
    /// 環境設定のセーブデータ初期化
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
    /// 環境設定のセーブデータ設定
    /// </summary>
    void SetPreferencesPlayerPrefsAll()
    {
        PrfSaveKey prfSaveKeys = (
            PrfSaveKey.BgmVolume |
            PrfSaveKey.SeVolume);
        SetPreferencesPlayerPrefs(prfSaveKeys);
    }

    /// <summary>
    /// 環境設定のセーブデータ設定
    /// </summary>
    void SetPreferencesPlayerPrefs(PrfSaveKey prfSaveKeys)
    {
        if (IsSave(PrfSaveKey.BgmVolume)) PlayerPrefs.SetFloat(PrfSaveKey.BgmVolume.ToString(), 0f);

        bool IsSave(PrfSaveKey key) => (prfSaveKeys & key) == key;
    }

    /// <summary>
    /// 環境設定データのセーブ
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
    /// 環境設定データのロード
    /// </summary>
    public void LoadPreferencesData()
    {
        var bgmVolume = PlayerPrefs.GetFloat(PrfSaveKey.BgmVolume.ToString());
    }
#endregion
}
