using System.Threading;
using System.Threading.Tasks;
using UniRx;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TweetSample : MonoBehaviour
{
    [Header("GyazoAPIのアクセストークンをここに設定"),SerializeField]
    private string accesstoken=null;
    
    [Header("Tweetしたい文字をここに設定"),SerializeField]
    private string text=null;
    [Header("タグにしたい文字をここに設定"),SerializeField]
    private string[] hashtag=null;

    [SerializeField] private Text _text;

    [SerializeField] private Button _button;
    
    // Start is called before the first frame update
    async UniTask Start()
    {
        _button.OnClickAsObservable()
            .Subscribe(async _ =>
            {
                _text.text = "こんにちわ！！！";
                Texture2D tex = await CaptureTweet.CaptureScreenshot(this.GetCancellationTokenOnDestroy());
                _text.text = "こ”＃んにちわ";
                var url = await CaptureTweet.ImgUpload(tex, accesstoken, new CancellationTokenSource());
                _text.text = "こんFGFにちわ";
                CaptureTweet.Tweet(text, hashtag, url);
                _text.text = "こんにちわ";
                Destroy(tex);
            }).AddTo(this);
        
    }
}
