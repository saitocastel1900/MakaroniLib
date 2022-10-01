using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace CSV
{
    public class CsvManager
    {
        private string Path => Application.dataPath + "/svcdata.csv";

        #region Sngleton

        private static CsvManager _instance = new CsvManager();
        public static CsvManager Instance => _instance;

        #endregion
        
        private CsvManager()
        {
            load();
        }

        private CsvData _csvData= new CsvData();
        public CsvData CsvData => _csvData; 

        /// <summary>
        /// CSVファイルを上書きする
        /// </summary>
        public void Save()
        {
            Debug.Log("インスタンスに設定された値をファイルに書き込みます");
            using (var writer = new StreamWriter(Path, false, Encoding.UTF8))
            {
                writer.WriteLine(CsvData.pos.x + "," + CsvData.pos.x + "," + CsvData.pos.x);
            }
            Debug.Log("書き込み完了");
        }

        /// <summary>
        /// CSVファイルを読み込む
        /// </summary>
        public void load()
        {
            //ファイルが存在していなかったら
            if (!File.Exists(Path))
            {
                Debug.Log("ファイルが存在しません。初期化処理を実行します。");
                Save();
                return;
            }

            using (var reader = new StreamReader(Path, Encoding.UTF8))
            {
                Debug.Log("CSVファイル読み込み中");
                string[] lines = new string[3];
                while (!reader.EndOfStream)
                {
                     lines= reader.ReadLine().Split(',');
                    Debug.Log("x:" + lines[0] + "y:" + lines[1] + "z:" + lines[2]);
                }
                _csvData.pos = new Vector3(int.Parse(lines[0]),int.Parse(lines[1]),int.Parse(lines[2]));
                Debug.Log("CSVファイル読み込み終了");
            }
        }
    }
}