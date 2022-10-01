using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CumEx
{
    /// <summary>
    /// Local座標での移動を楽にするメソッド
    /// </summary>
 public static class TransformEx 
 {
     public  static void AddPosX(this Transform transform,float x)
     {
         var pos = transform.position;
         pos.x += x;
         transform.position = pos;
     }
     public  static void AddPosY(this Transform transform,float y)
     {
         var pos = transform.position;
         pos.y += y;
         transform.position = pos;
     }
     public  static void AddPosZ(this Transform transform,float z)
     {
         var pos = transform.position;
         pos.z += z;
         transform.position = pos;
     }
     
     public  static void SubPosX(this Transform transform,float x)
     {
         var pos = transform.position;
         pos.x -= x;
         transform.position = pos;
     }
     public  static void SubPosY(this Transform transform,float y)
     {
         var pos = transform.position;
         pos.y -= y;
         transform.position = pos;
     }
     public  static void SubPosZ(this Transform transform,float z)
     {
         var pos = transform.position;
         pos.z -= z;
         transform.position = pos;
     }
 }   
}

