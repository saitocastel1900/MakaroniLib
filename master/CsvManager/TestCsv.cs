using System.Collections;
using System.Collections.Generic;
using CSV;
using UnityEngine;

/// <summary>
/// CsvManagerを試すクラス
/// </summary>
public class TestCsv : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //原点を指定
            CsvManager.Instance.CsvData.pos = new Vector3(0, 0, 0);
        }    
        if (Input.GetKeyDown(KeyCode.Z))
        {
            //原点を指定
            CsvManager.Instance.CsvData.pos = new Vector3(10, 10, 10);
        } 
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            CsvManager.Instance.load();
        }      
        if (Input.GetKeyDown(KeyCode.B))
        {
            CsvManager.Instance.Save();
        }   
    }
}
