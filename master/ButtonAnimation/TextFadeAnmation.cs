using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

/// <summary>
/// フェードするテキスト
/// </summary>
public class TextFadeAnmation : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;
    private AnimationCurve _doTextCurve = default;
    
    private void Awake()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        Initialize();
        Play(2.5f);
    }

    private void Initialize()
    {
        textMeshPro.DOFade(0, 0);
        //textMeshPro.characterSpacing = -50;
    }

    private void Play(float duration)
    {
        // 文字間隔を開ける
        /*DOTween.To(() => textMeshPro.characterSpacing, //何に
                value => textMeshPro.characterSpacing = value,//何を 
                10, //どこまで
                duration)//どのくらいの間
            .SetEase(Ease.OutQuart) 
            .SetLoops(-1,LoopType.Restart);
        */
        
        // フェード
        DOTween.Sequence()
            .Append(textMeshPro.DOFade(1, duration / 4))
            .AppendInterval(duration / 2)
            .Append(textMeshPro.DOFade(0, duration / 4))
            .SetLoops(-1,LoopType.Restart)
            .SetLink(this.gameObject);
    }
}
