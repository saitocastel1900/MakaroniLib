using UnityEngine;
using System;
using System.IO;

/// <summary>
///試しに3つの関数を実行してみる 
/// </summary>

//セーブデータ
//Jsonを使う際のおまじない
[Serializable]
public class Data01
{
    //Jsonのキー名と変数名は同じにする必要がある
    public Player[] player;
}
[Serializable]
public class Player
{
    public string name;
    public PlayerInfo playerinfo;
}
[Serializable]
public class PlayerInfo
{
    public bool child;
     public int age;
}

public class JsonTurtial : MonoBehaviour
{
    private void Start()
    {
        //JsonファイルをUnityオブジェに対応する
        //ResorcesフォルダからJsonファイルを読み込む
        string json = Resources.Load<TextAsset>("Data1").ToString();

        //読み込んだJsoファイルをUnityオブジェに変換
        Data01 data01 = JsonUtility.FromJson<Data01>(json);
        Debug.Log("名前:"+data01.player[0].name+" 年齢:"+data01.player[0].playerinfo.age);

        //UnityオブジェからJsonファイルを生成する
        //新たにインスタンスを作る
        Data01 data02 = new Data01();

        //jsonファイルを上書きしてみる(インスタンス,Jsonデータ)
        //Jsonデータをインスタンスに書き込む
        JsonUtility.FromJsonOverwrite(json,data02);
        Debug.Log("上書き完了"+data02.player[0].name);
        
        //trueを指定する事で読みやすく整形される
        string data02tojson = JsonUtility.ToJson(data02,true);
        
        //Jsonファイルを出力してみる
        File.WriteAllText($"Assets/Resources/Data02.json", data02tojson);
        
    }
}