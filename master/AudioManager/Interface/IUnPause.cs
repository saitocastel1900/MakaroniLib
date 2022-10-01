using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public interface IUnPause
    {
        /// <summary>
        /// 音の再生を再開する
        /// </summary>
        /// <returns></returns>
        public AudioManager UnPause();
    }
}