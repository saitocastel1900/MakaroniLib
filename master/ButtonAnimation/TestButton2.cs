using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UniRx.Triggers;
using UniRx;
using UnityEngine.Events;

namespace Saito
{
    [RequireComponent(typeof(Image))]
    [AddComponentMenu("UI/TestButton", 14)]
    public class Test2Button : MonoBehaviour
    {
        //Images
        [Header("押した際のボタン画像")] [SerializeField] private Sprite pressedSprite=null;
        [Header("選択された際のボタン画像")] [SerializeField] private Sprite selectedSprite=null;
        [Header("ホバーの配置するオブジェクト")] [SerializeField] private Image _hover=null;
        
        [Header("押した際に呼び出される関数")] [SerializeField] private UnityEvent onClick=null;

        private Tweener tweener = null;
        private Sprite _defaultSprite;

        private void Awake()
        {
            _defaultSprite = this.gameObject.GetComponent<Image>().sprite;
            OnButtonDeSelect();
            OnButtonExit();
        }

        // Start is called before the first frame update
        void Start()
        {
            //クリックしたら実行される
            this.gameObject.AddComponent<ObservablePointerClickTrigger>()
                .OnPointerClickAsObservable().Subscribe(_ =>
                    {
                        OnButtonClick();
                        onClick?.Invoke();
                    },
                    ex => Debug.LogError(ex)
                ).AddTo(this.gameObject);

            this.gameObject.AddComponent<ObservablePointerDownTrigger>()
                .OnPointerDownAsObservable().Subscribe(_ => { OnButtonDown(); },
                    ex => Debug.LogError(ex)
                ).AddTo(this.gameObject);

            this.gameObject.AddComponent<ObservablePointerUpTrigger>()
                .OnPointerUpAsObservable().Subscribe(_ => { OnButtonUp(); },
                    ex => Debug.LogError(ex)
                ).AddTo(this.gameObject);

            this.gameObject.AddComponent<ObservablePointerExitTrigger>()
                .OnPointerExitAsObservable().Subscribe(_ => { OnButtonExit(); },
                    ex => Debug.LogError(ex)
                ).AddTo(this.gameObject);

            this.gameObject.AddComponent<ObservablePointerEnterTrigger>()
                .OnPointerEnterAsObservable().Subscribe(_ => { OnButtonEnter(); },
                    ex => Debug.LogError("押されました")
                ).AddTo(this.gameObject);

            this.gameObject.AddComponent<ObservableSelectTrigger>()
                .OnSelectAsObservable().Subscribe(_ => { OnButtonSelect(); },
                    ex => Debug.LogError(ex)
                ).AddTo(this.gameObject);
            
            this.gameObject.AddComponent<ObservableDeselectTrigger>()
                .OnDeselectAsObservable().Subscribe(_ => { OnButtonDeSelect(); },
                    ex => Debug.LogError(ex)
                ).AddTo(this.gameObject);
            
            
            var mouseDownStream = this.UpdateAsObservable().Where(_ => Input.GetMouseButtonDown(0));
            var mouseUpStream = this.UpdateAsObservable().Where(_ => Input.GetMouseButtonUp(0));
    
            /*
            //長押しの判定
            //マウスクリックされたら3秒後にOnNextを流す
            mouseDownStream
                //3秒ごとにメッセージを発生させる
                .SelectMany(_ => Observable.Timer(TimeSpan.FromSeconds(3)))
                //マウスのクリックを離したら、メッセージ発信を終了する
                .TakeUntil(mouseUpStream)
                .RepeatUntilDestroy(this.gameObject)
                .Subscribe(_ => Debug.Log("長押し"))
                .AddTo(this);
                */
            
            /*
            //常に発信するイベント
            this.UpdateAsObservable().
                Where(_ => Input.GetKeyDown(KeyCode.Space)) //Spaceキーが押されていて
                .TakeUntilDestroy(this)
                .ThrottleFirst(TimeSpan.FromSeconds(2)) //2秒の間隔で発信する
                .Subscribe(_ =>
                {
                    Debug.Log("Space Key Down!");
                    Debug.Log("Space!!!!!!!!!!!!!!!!!!!!");
                }); //実行されるイベントハンドラー
                */
        }


        #region Buttonで実行したい共通処理

        private void OnButtonExit()
        {
            // Exit時の共通処理
            if(_hover!=null)_hover.color = new Color(0, 0, 0, 0);
            Debug.Log("Button  Exit");
        }

        private void OnButtonEnter()
        {
            // Enter時の共通処理
            if(_hover!=null)_hover.color = new Color(1, 1, 1, 1);
            Debug.Log("Button Enter");
        }

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
                .SetEase(Ease.OutExpo);

            if(pressedSprite!=null)this.gameObject.GetComponent<Image>().sprite=pressedSprite;
            
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

            tweener = transform.DOPunchScale(
                punch: Vector3.one * 0.1f,
                duration: 0.2f,
                vibrato: 1
            ).SetEase(Ease.OutExpo);

            if(_defaultSprite!=null)this.gameObject.GetComponent<Image>().sprite=_defaultSprite;
            
            // Up時の共通処理
            Debug.Log("Button Release");
        }

        private void OnButtonClick()
        {
            // Click時の共通処理
            Debug.Log("Button Click");
        }

        private void OnButtonSelect()
        {
            // Select時の共通処理
            if(selectedSprite!=null)this.gameObject.GetComponent<Image>().sprite = selectedSprite;
            Debug.Log("This gameObject Selected!");
        }

        private void OnButtonDeSelect()
        {
            if(_defaultSprite!=null)this.gameObject.GetComponent<Image>().sprite=_defaultSprite;
            Debug.Log("This gameObject DeSelected!");
        }

        /// <summary>
        /// クリック時に呼び出される関数(登録)
        /// </summary>
        public void TestOnClick()
        {
            Debug.Log("Event OnClick!");
        }

        #endregion
    }
}
