using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace CumEx
{
    public static class GameObjectEx 
    {
        /// <summary>
        /// ゲームオブジェクトを破棄する
        /// </summary>
        /// <param name="gameObject">破棄するゲームオブジェクト</param>
        /// <param name="time">何秒後に破棄するか</param>
        public async static UniTask AsyncDestroy(this GameObject gameObject,int time)
        {
            await Task.Delay(TimeSpan.FromSeconds(time)); 
            UnityEngine.Object.Destroy(gameObject);
        }
    }
}
