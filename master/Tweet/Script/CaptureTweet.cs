using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public static class CaptureTweet
{
    /// <summary>
    /// スクショして実機に保存し、保存した画像を返す
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public static async UniTask<Texture2D> CaptureScreenshot(CancellationToken token)
    {
        //プラットフォームごとに保存先を分ける
#if UNITY_EDITOR || UNITY_WEBGL　|| UNITY_STANDALONE_WIN
        string path = Application.dataPath;
#elif UNITY_ANDROID
        string path = Application.persistentDataPath;
#else
Debug.LogError("この環境（機種）に対応していません");
#endif
        //保存先フォルダを作成する
        if (!Directory.Exists(path + "/Captures"))
        {
            Directory.CreateDirectory(Path.Combine(path, "Captures"));
        }
        
        //スクショの保存先とファイル名の設定
        string date = DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss");
        string file = path + "/Captures/capture" + date + ".png";

        //スクリーンショット
        ScreenCapture.CaptureScreenshot(file);
        Debug.Log("スクショ実行中" + File.Exists(file));
        await UniTask.WaitUntil(() => File.Exists(file), cancellationToken: token);
        Debug.Log("スクショ完了！" + File.Exists(file));

        //スクショした画像を読み込んで（バイト）、テクスチャとして返す
        Texture2D tex = new Texture2D(0, 0);
        tex.LoadImage(File.ReadAllBytes(file));
        return tex;
    }

    /// <summary>
    ///  スクショした画像をGyazoにアップロードする
    /// </summary>
    /// <param name="tex"></param>
    /// <param name="accesstoken"></param>
    /// <param name="source"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static async UniTask<string> ImgUpload(Texture2D tex, string accesstoken, CancellationTokenSource source)
    {
        //POST先のURL
        var uploadUrl = "https://upload.gyazo.com/api/upload";
        var bimg = tex.EncodeToPNG();
        
        //formの作成
        var form = new WWWForm();
        form.AddField("access_token", accesstoken);
        form.AddBinaryData("imagedata", bimg, "screenshot.png", "image/png");

        //画像をアップロード、URLゲット!
        using var request = await UnityWebRequest.Post(uploadUrl, form)
            .SendWebRequest()
            .ToUniTask(Progress.Create<float>(x => Debug.Log(x)),cancellationToken:source.Token);;
        
        switch (request.result)
        {
            case UnityWebRequest.Result.InProgress:
                Debug.Log("リクエスト中");
                break;

            case UnityWebRequest.Result.Success:
                Debug.Log("リクエスト成功");
                break;

            case UnityWebRequest.Result.ConnectionError:
                Debug.LogError
                (
                    @"サーバとの通信に失敗。
リクエストが接続できなかった、
セキュリティで保護されたチャネルを確立できなかったなど。"
                         
                );
                break;

            case UnityWebRequest.Result.ProtocolError:
                Debug.LogError
                (
                    @"サーバがエラー応答を返した。
サーバとの通信には成功したが、
接続プロトコルで定義されているエラーを受け取った。"
                );
                break;

            case UnityWebRequest.Result.DataProcessingError:
                Debug.LogError
                (
                    @"データの処理中にエラーが発生。
リクエストはサーバとの通信に成功したが、
受信したデータの処理中にエラーが発生。
データが破損しているか、正しい形式ではないなど。"
                    
                );
                break;
             
            default: throw new ArgumentOutOfRangeException();
        }

        //Jsonパース
        var response = JsonUtility.FromJson<GyazoResponse>(request.downloadHandler.text);
        if (response.permalink_url == null)
        {
            Debug.LogError("画像をアップロードまた、ダウンロード出来ませんでした");
            source.Cancel();
        }

        //取得できた画像urlを返す
        return response.permalink_url;
    }

    /// <summary>
    /// 画像付きのツイートをする
    /// </summary>
    /// <param name="text"></param>
    /// <param name="tags"></param>
    /// <param name="imgUrl"></param>
    public static void Tweet(string text , IEnumerable<string> tags , string imgUrl )
    {
        //事前に文言と画像は設定しておく
        var tweetUrl = $"https://twitter.com/intent/tweet?text={UnityWebRequest.EscapeURL(text)}&url={UnityWebRequest.EscapeURL(imgUrl)}";

        //タグを付ける
        if (tags != null)
        {
            //tiwtterでは ','==半空白
            var strTag = string.Join(",", tags);

            if (!string.IsNullOrEmpty(strTag))
                tweetUrl += $"&hashtags={UnityWebRequest.EscapeURL(strTag)}";
        }

        //ツイートする
        Application.OpenURL(tweetUrl);
        Debug.Log("ツイートしました");
    }
}