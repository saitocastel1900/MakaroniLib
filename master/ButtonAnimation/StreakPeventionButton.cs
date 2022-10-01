using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace Saito
{
    public class StreakPeventionButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        // Start is called before the first frame update
        void Start()
        {
            _button?.gameObject.AddComponent<ObservablePointerClickTrigger>()
                .OnPointerClickAsObservable()
                .TakeUntilDestroy(this)
                .ThrottleFirst(TimeSpan.FromSeconds(2))
                .Subscribe(_ => { Debug.Log("ClickButton"); });
        }
    }
}