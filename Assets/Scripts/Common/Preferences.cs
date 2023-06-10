using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preferences : SingletonMonoBehaviour<Preferences>
{
    public float BgmVolume { get; set; } = 1f;
    public float SeVolume { get; set; } = 1f;
}
