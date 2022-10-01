using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;

namespace Saito
{
    public class LongClickButton : MonoBehaviour
    {
        [SerializeField] private Button _button = null;

        [SerializeField] private float duration = 2.0f;

        // Start is called before the first frame update
        void Start()
        {
            var clickDownStream =
                _button?.gameObject.AddComponent<ObservablePointerDownTrigger>()
                    .OnPointerDownAsObservable().TakeUntilDestroy(this);

            var clickUpStream =
                _button?.gameObject.AddComponent<ObservablePointerExitTrigger>()
                    .OnPointerExitAsObservable().TakeUntilDestroy(this);

            clickDownStream
                .SelectMany(_ => Observable.Timer(TimeSpan.FromSeconds(duration)))
                .TakeUntil(clickUpStream)
                .RepeatUntilDestroy(this.gameObject)
                .Subscribe(_ => Debug.Log("’·‰Ÿ‚µ"));
        }
    }
}