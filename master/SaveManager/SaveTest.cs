using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Saito.Json.SaveSystem
{
    public class SaveTest : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                SaveManager.Instance.Data.name = "Saito";
                SaveManager.Instance.Data.id = 2023178;
                SaveManager.Instance.Save();
                Debug.Log("セーブ完了");
            }

            if (Input.GetKey(KeyCode.O))
            {
                SaveManager.Instance.Load();
                Debug.Log("name"+SaveManager.Instance.Data.name+"id"+SaveManager.Instance.Data.id);
                Debug.Log("ロードしました");
            }
        }
    }
}