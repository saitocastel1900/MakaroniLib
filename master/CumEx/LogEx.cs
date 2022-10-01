using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CumEx
{
    public class LogEx : MonoBehaviour
    {
        //あくまでクラスに付与して使用
        public override string ToString()
        {
            //ここにメンバ
            return $"{this.name}";
        }
    }
}


