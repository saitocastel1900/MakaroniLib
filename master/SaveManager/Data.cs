using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace Saito.Json.SaveSystem
{
    /// <summary>
    /// セーブ、ロードしたいデータ
    /// </summary>
    [Serializable]
    public class Data
    {
        public string name = "DefaultName";
        public int id = -1 ;
    }
}
