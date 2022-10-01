using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CumEx
{
   //拡張メソッドはクラスとメソッド共にstaticにする
  public static class StringEx 
  {
      //thisで拡張する型を指定する
      /// <summary>
      /// 長さを表示
      /// </summary>
      /// <param name="str"></param>
      public static void FancyDebug(this string str)
      {
         Debug.LogFormat("{0}",str.Length); 
      }
  }  
}

