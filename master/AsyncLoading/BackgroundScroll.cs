using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Shader書いた方が効率的だけど、csでuvスクロールしたい時用のスクリプト
/// </summary>
public class BackgroundScroll : MonoBehaviour
{
   private const float X_SPEED = 0.04f;
       private const float Y_SPEED = 0.0f;
       public RawImage background;
       
       void Update()
       {
           var rect = background.uvRect;
           rect.x = (rect.x + X_SPEED * Time.unscaledDeltaTime) % 1.0f;
           rect.y = (rect.y + Y_SPEED * Time.unscaledDeltaTime) % 1.0f;
           background.uvRect = rect;
       }
}
