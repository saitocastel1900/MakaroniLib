using System;
using UnityEngine;

namespace Audio
{
    public interface IResult
    {
        /// <summary>
        /// 実行完了後に行いたいこと
        /// </summary>
        /// <param name="_event"></param>
        public void OnCompleted(Action _event);
    }
}