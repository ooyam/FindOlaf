using UnityEngine;
using System.Collections;
using Common;

///
/// !!! Machine generated code !!!
/// !!! DO NOT CHANGE Tabs to Spaces !!!
/// 
[System.Serializable]
public class SeData
{
    [SerializeField]
    SoundControl.SeType setype;
    public SoundControl.SeType SeType { get { return setype; } set { this.setype = value; } }

    [SerializeField]
    string clipname;
    public string ClipName { get { return clipname; } set { this.clipname = value; } }

    [SerializeField]
    float volume;
    public float Volume { get { return volume; } set { this.volume = value; } }
}