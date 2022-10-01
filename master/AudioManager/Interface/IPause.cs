using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public interface IPause
    {
        /// <summary>
        /// 音の再生を一時停止する
        /// </summary>
        /// <returns></returns>
        public AudioManager Pause();
    }
}