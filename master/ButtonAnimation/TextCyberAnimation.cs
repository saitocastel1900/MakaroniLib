using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

/// <summary>
/// サイバー風にテキストをアニメーションする
/// </summary>
public class TextCyberAnimation : MonoBehaviour
{
         private TextMeshProUGUI textMeshPro;
        private string text;
        private AnimationCurve _doTextCurve = default;
    
        private void Awake()
        {
            textMeshPro = this.gameObject.GetComponent<TextMeshProUGUI>();
            Initialize();
            Play(2.5f);
        }
    
        private void Initialize()
        {
            //使用するテキストを設定
            text = textMeshPro.text;
            //アニメーション開始時は空にする
            textMeshPro.text = "";
        }
    
        private void Play(float duration)
        {
            //指定した文字で事前に文字を設定できる
            // textMeshPro.DOText(endValue: text, duration: duration,false,ScrambleMode.Custom,scrambleChars:"???dfsert45td")
            textMeshPro.DOText(endValue: text, duration: duration,false,ScrambleMode.All)
                .SetEase(Ease.Linear)
                .SetLoops(-1,LoopType.Restart)
                .SetLink(this.gameObject);
        }
}
