using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;


/// <summary>
/// Enumでの状態管理法
/// </summary>
public class GameManager : MonoBehaviour
{
    private GameStateReactiveProperty _gameStateReactiveProperty;
    
    /// <summary>
    /// ゲーム全体の状態
    /// </summary>
    public enum STEP
    {
        /// <summary>
        /// 変化待ち状態
        /// </summary>
        NONE=-1,
        /// <summary>
        /// 値の初期化時・準備
        /// </summary>
        SET,
        /// <summary>
        /// ゲームプレイ時
        /// </summary>
        PLAY,
        /// <summary>
        /// ゲーム終了時
        /// </summary>
        CLEAR
    }

    private STEP _step;
    private STEP _next_step;
    private float _step_timer;

    //初回起動時はSET
    void Start()
    {
        _next_step = STEP.SET;
    }

    //常に回す
    void Update()
    {
        _step_timer += Time.deltaTime;
        Debug.Log(_step_timer);
                
        //一度だけ行うもの
        if (_next_step!=STEP.NONE)
        {
            Debug.Log("ステップ変化");
            _step = _next_step;
            _next_step = STEP.NONE;
            _step_timer = 0.0f;
            
            switch (_step)
            {
                //スコアの初期化とか
                case STEP.SET:
                    _next_step = STEP.PLAY;
                    break;
                
                //BGMの開始、PLayer操作の開始とか
                case STEP.PLAY:
                    break;
                
                //BGM、Player操作の停止とか
                case STEP.CLEAR:
                    break;
            }
        }
        
        //繰り返し行うもの
        switch (_step)
        {
            //特になし
            case STEP.SET:
                break;
            
            //ゲーム倉条件の判定,GUI
            case STEP.PLAY:
                Debug.Log("ステップ繰り返し:PLAY");
                
                //if(...) _next_step=STEP>CLEAR;
                break;
            
            //特になし
            case STEP.CLEAR:
                break;
        }
    }
}
