using UnityEngine;
using System.Collections;
using Common;

///
/// !!! Machine generated code !!!
/// !!! DO NOT CHANGE Tabs to Spaces !!!
/// 
[System.Serializable]
public class BgmData
{
    [SerializeField]
    SoundControl.BgmType bgmtype;
    public SoundControl.BgmType BgmType { get { return bgmtype; } set { this.bgmtype = value; } }

    [SerializeField]
    string clipname;
    public string ClipName { get { return clipname; } set { this.clipname = value; } }

    [SerializeField]
    float volume;
    public float Volume { get { return volume; } set { this.volume = value; } }
}