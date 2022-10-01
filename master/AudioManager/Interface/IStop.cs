using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public interface IStop
    {
        /// <summary>
        /// 音の再生をやめる
        /// </summary>
        /// <returns></returns>
        public AudioManager Stop();
    }
}
