using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UniRx.Triggers;
using UniRx;
using UnityEngine.Events;

public class SimpleButtonAnimation : MonoBehaviour
{
    private Tweener tweener = null;
    private Vector3 baseScale;

    private void Awake()
    {
        baseScale = this.transform.localScale;
    }

    void Start()
    {
        this.gameObject.AddComponent<ObservablePointerDownTrigger>()
            .OnPointerDownAsObservable().Subscribe(_ => { OnButtonDown(); },
                ex => Debug.LogError(ex)
            ).AddTo(this.gameObject);

        this.gameObject.AddComponent<ObservablePointerUpTrigger>()
            .OnPointerUpAsObservable().Subscribe(_ => { OnButtonUp(); },
                ex => Debug.LogError(ex)
            ).AddTo(this.gameObject);
    }


    #region Buttonで実行したい共通処理

    private void OnButtonDown()
    {
        if (tweener != null)
        {
            tweener.Kill();
            Debug.Log(tweener);
            tweener = null;
            transform.localScale = Vector3.one;
        }

        tweener = transform.DOScale(
                endValue: new Vector3(0.9f, 0.9f, 0.9f),
                duration: 0.2f
            )
            .SetEase(Ease.OutExpo).Play();

        // Down時の共通処理
        Debug.Log("Button Push");
    }

    private void OnButtonUp()
    {
        if (tweener != null)
        {
            tweener.Kill();
            Debug.Log(tweener);
            tweener = null;
            transform.localScale = Vector3.one;
        }

        //SetRelativeで相対的に拡大することも良いかも
        tweener = transform.DOScale(
            baseScale,
            0.3f
        ).SetEase(Ease.OutCubic).Play();

        // Up時の共通処理
        Debug.Log("Button Release");
    }

    #endregion
}
