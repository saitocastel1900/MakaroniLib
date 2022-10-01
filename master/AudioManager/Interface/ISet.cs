using System;
using UnityEngine;

namespace Audio
{
    public interface ISet
    {
        /// <summary>
        /// 音量を調整する
        /// </summary>
        /// <param name="volume"></param>
        /// <returns></returns>
        public AudioManager SetVolume(float volume);

        /// <summary>
        /// 音を何秒後に再生するか調整する
        /// </summary>
        /// <param name="delay"></param>
        /// <returns></returns>
        public AudioManager SetDelay(float delay);

        /// <summary>
        /// 音のピッチを調整する
        /// </summary>
        /// <param name="pitch"></param>
        /// <returns></returns>
        public AudioManager SetPitch(float pitch);
    }
}
