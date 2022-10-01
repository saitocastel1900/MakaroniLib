using System;
using System.Collections;
using System.Collections.Generic;
using Saito.SoundManager;
using UnityEngine;

public class Main : MonoBehaviour
{
    public void PushButton_Bgm()
    {
        SoundManager.Instance.PlayBgm(SoundManager.BgmSoundData.BGM.Play);
    }
   
}
