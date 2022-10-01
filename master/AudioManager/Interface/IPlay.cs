using System;
using UnityEngine;

namespace Audio
{
    public interface IPlay
    {
        /// <summary>
        /// 効果音を流す
        /// </summary>
        /// <param name="clip"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public AudioManager PlaySE(AudioClip clip, float duration = 0.0f);

        /// <summary>
        /// BGMを再生する
        /// </summary>
        /// <param name="clip"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public AudioManager PlayBGM(AudioClip clip, float duration = 0.0f, bool loopFlag = false);
    }
}