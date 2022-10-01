using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEditor;


namespace Saito.Json.SaveSystem
{
    /// <summary>
    /// セーブロードを統合的に管理する
    /// </summary>
    public class SaveManager
    {
        //Singleton
        //カプセル化
        private static SaveManager instance = new SaveManager();
        public static SaveManager Instance => instance;

        //コンストラクタ 初期化
        private SaveManager()
        {
            Load();
        }

        public string Path => Application.dataPath + "/SaveData.json";
        public Data Data { get;private set; }

        public void Save()
        {
            string jsonData = JsonUtility.ToJson(Data);    //Jsonデータに変更
            StreamWriter streamWriter = new StreamWriter(Path, false); //true==追記 false==上書き
            streamWriter.WriteLine(jsonData);
            streamWriter.Flush();
            streamWriter.Close();
        }

        public void Load()
        {
            //初期化
            if (!File.Exists(Path))
            {
                Debug.Log("ファイルが存在しません。初回起動です");
                Data = new Data();
                Save();
                return;
            }

            StreamReader streamReader = new StreamReader(Path);
            string jsonData = streamReader.ReadToEnd();
            Data = JsonUtility.FromJson<Data>(jsonData);
            streamReader.Close();
        }
    }
}

