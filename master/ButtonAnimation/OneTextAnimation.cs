using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

/// <summary>
/// 一文ずつフェードするアニメーション
/// </summary>
public class OneTextAnimation : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;
    private string text;
    private AnimationCurve _doTextCurve = default;
    
    private void Awake()
    {
        textMeshPro = this.gameObject.GetComponent<TextMeshProUGUI>();
        var tmpro=Initialize();
        Play(tmpro);
    }
    
    private TextMeshProUGUI Initialize()
    {
        var tmpro = this.GetComponent<TextMeshProUGUI>();
        tmpro.DOFade(0, 0);
        return tmpro;
    }
    
    private void Play(TextMeshProUGUI tmpro)
    {
        DOTweenTMPAnimator tmproAnimator = new DOTweenTMPAnimator(tmpro);
        for (int i = 0; i < tmproAnimator.textInfo.characterCount; i++)
        {
            tmproAnimator.DOFadeChar(i, 1, 2f)
                .SetDelay(i * 0.1f)
                .SetEase(Ease.Linear);
        }
    }
}
