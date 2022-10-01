using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneButton : MonoBehaviour
{
    [SerializeField] private GameObject loadUI;
    
    [SerializeField] private Slider slider;

    public void Start()
    {
        loadUI.SetActive(true);
        
        StartCoroutine("LoadData");
    }

    IEnumerator LoadData()
    {
        var async = SceneManager.LoadSceneAsync("Load1");
        async.allowSceneActivation = false;
        
        // フェードアウト等の何かしらの処理
        //yield return FadeOut();
        
        //　asyncprocessが0.9を超えると実行されるらしい
        while (!async.isDone)
        {
            if (async.progress >= 0.9f)
            {
                // シーン読み込み
                async.allowSceneActivation = true;
                Debug.Log("シーン読み込み");
            }
            
            var progressVal = Mathf.Clamp01(async.progress / 0.9f);
            slider.value = progressVal;
            yield return null;
        }
        
    }
    
    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(10);
    }
}
