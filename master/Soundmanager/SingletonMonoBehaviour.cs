using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MonoBehaviourを継承したシングルトンなクラス（基底クラス）
/// </summary>
public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    /// <summary>
    /// インスタンス
    /// </summary>
    private static T _instance;

    /// <summary>
    /// インスタンスのゲッター
    /// </summary>
    public static T Instance
    {
        get
        {
            //インスタンスのnullチェック(初回起動時)
            if (_instance == null)
            {
                T[] instances = null;
                instances = FindObjectsOfType<T>();
                //FindObjectOfType(typeof(T)) as T; 　こちらで大丈夫です

                //インスタンスが存在なし
                if (instances.Length == 0)
                {
                    Debug.LogError(typeof(T) + "インスタンスはありません。アタッチし忘れていませんか？");
                    return null;
                }
                //インスタンスが複数個存在している...
                else if (instances.Length >= 2)
                {
                    Debug.LogError(typeof(T) + "インスタンスが複数個生成されています。");
                    return null;
                }
                //インスタンスが1個存在している(平常)
                else
                {
                    _instance = instances[0];
                    Debug.Log(typeof(T) + "インスタンスが1個生成されています。正常です。");
                }
            }

            return _instance;
        }
    }
}