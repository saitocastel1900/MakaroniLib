using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Saito.SoundManager
{
    /// <summary>
    /// 音を統合的に管理　
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class SoundManager : SingletonMonoBehaviour<SoundManager>
    {
        #region Singleton

        private void Awake()
        {
            if (this != Instance)
            {
                Debug.LogError("インスタンスが既に存在しています。インスタンスを一つにするためこのインスタンスを破棄します");
                Destroy(this.gameObject);
                return;
            }

            DontDestroyOnLoad(this.gameObject);
        }

        #endregion

        [Header("AudioSource")] 
        [SerializeField, Tooltip("BGMのAudioSourceをここにアタッチ")]
        AudioSource bgmAudioSource;

        [SerializeField, Tooltip("SEのAudioSourceをここにアタッチ")]
        AudioSource seAudioSource;

        [Header("音のボリューム")] 
        [SerializeField, Range(0.0f, 1.0f), Tooltip("マスタ-音量")]
        public float masterVolume = 1.0f;

        [SerializeField, Range(0.0f, 1.0f), Tooltip("BGMのマスタ音量")]
        public float bgmMasterVolume = 1.0f;

        [SerializeField, Range(0.0f, 1.0f), Tooltip("SEのマスタ音量")]
        public float seMasterVolume = 1.0f;

        [SerializeField, Tooltip("再生したいBgmをここで設定")]
        List<BgmSoundData> bgmSoundDatas;

        [SerializeField, Tooltip("再生したいSeをここで設定")]
        List<SeSoundData> seSoundDatas;

        /// <summary>
        /// Bgmを再生
        /// </summary>
        public void PlayBgm(BgmSoundData.BGM bgm)
        {
            if (bgmAudioSource == null)
            {
                Debug.LogError("BgmのAudioSourceが設定されていません。");
                return;
            }

            BgmSoundData data = bgmSoundDatas.Find(data => data.bgm == bgm);
            if (data == null)
            {
                Debug.LogError("指定されたラベルが見つかりません。設定し忘れていませんか？");
                return;
            }

            bgmAudioSource.volume = Mathf.Clamp(data.volume * bgmMasterVolume * masterVolume, 0.0f, 1.0f);
            bgmAudioSource.clip = data.audioClip;

            if (bgmAudioSource.clip == null)
            {
                Debug.LogError(bgm + "のclipが設定されていません。");
                return;
            }

            bgmAudioSource.loop = true;
            bgmAudioSource.Play();
            Debug.Log(data.audioClip.name + "効果音を鳴らしました");
        }

        /// <summary>
        /// Bgmの再生をやめる
        /// </summary>
        public void StopBgm(BgmSoundData.BGM bgm)
        {
            if (bgmAudioSource == null)
            {
                Debug.LogError("BgmのAudioSourceが設定されていません。");
                return;
            }

            BgmSoundData data = bgmSoundDatas.Find(data => data.bgm == bgm);
            if (data == null)
            {
                Debug.LogError("指定されたラベルが見つかりません。設定し忘れていませんか？");
                return;
            }

            bgmAudioSource.Stop();
            bgmAudioSource.clip = null;
            Debug.Log("BGMを鳴らすのをやめました");
        }

        /// <summary>
        /// Bgmの再生を一時停止する
        /// </summary>
        public void PauseBgm()
        {
            if (bgmAudioSource == null)
            {
                Debug.LogError("BgmのAudioSourceが設定されていません。");
                return;
            }

            if (bgmAudioSource.clip == null)
            {
                Debug.LogError("BGMのAudioClipが設定されていません");
                return;
            }

            bgmAudioSource.Pause();
        }

        /// <summary>
        /// Bgmを一時停止した場所から再生する
        /// </summary>
        public void UnPauseBgm()
        {
            if (bgmAudioSource == null)
            {
                Debug.LogError("BgmのAudioSourceが設定されていません。");
                return;
            }

            if (bgmAudioSource.clip == null)
            {
                Debug.LogError("BGMのAudioClipが設定されていません");
                return;
            }

            bgmAudioSource.UnPause();
        }

        /// <summary>
        /// Seを再生
        /// </summary>
        public void PlaySe(SeSoundData.SE se)
        {
            if (seAudioSource == null)
            {
                Debug.LogError("SeのAudioSourceが設定されていません。");
                return;
            }

            SeSoundData data = seSoundDatas.Find(data => data.se == se);
            if (data == null)
            {
                Debug.LogError("指定されたラベルが見つかりません。設定し忘れていませんか？");
                return;
            }

            seAudioSource.volume = Mathf.Clamp(data.volume * seMasterVolume * masterVolume, 0.0f, 1.0f);
            if (data.audioClip == null)
            {
                Debug.LogError(se + "のclipが設定されていません。");
                return;
            }

            seAudioSource.PlayOneShot(data.audioClip);
            Debug.Log(data.audioClip.name + "効果音を鳴らしました");
        }

        /// <summary>
        /// Seの再生をやめる
        /// </summary>
        public void StopSe(SeSoundData.SE se)
        {
            if (seAudioSource == null)
            {
                Debug.LogError("SeのAudioSourceが設定されていません。");
                return;
            }

            SeSoundData data = seSoundDatas.Find(data => data.se == se);
            if (data == null)
            {
                Debug.LogError("指定されたラベルが見つかりません。設定し忘れていませんか？");
                return;
            }

            seAudioSource.Stop();
            seAudioSource.clip = null;
            Debug.Log("効果音を鳴らすのをやめました");
        }

        /* Seは再生を一時停止を利用するは少ないのでコメントアウトしておきます
        /// <summary>
        /// Seの再生を一時停止する
        /// </summary>
        public void PauseSe()
        {
            seAudioSource.Pause();   
        }
        /// <summary>
        /// Seを一時停止した場所から再生する
        /// </summary>
        public void UnPauseSe()
        { 
         seAudioSource.UnPause();
        }
        */

        /// <summary>
        /// BGMを登録・調整するクラス
        /// </summary>
        [Serializable]
        public class BgmSoundData
        {
            /// <summary>
            /// 用途に応じたラベルを設定
            /// </summary>
            public enum BGM
            {
                Home,
                Game,
                Title,
                Play,
            }

            [Tooltip("音の種類をラベルで設定")]
            public BGM bgm;
            [Tooltip("使用したい音を設定")]
            public AudioClip audioClip;
            [Range(0.0f, 1.0f), Tooltip("音量")] public float volume = 1.0f;
        }

        /// <summary>
        /// SEを登録・調整するクラス
        /// </summary>
        [Serializable]
        public class SeSoundData
        {
            /// <summary>
            /// 用途に応じたラベルを設定
            /// </summary>
            public enum SE
            {
                Win,
                Lose,
                Clicked,
                Highlited,
                Taped,
            }

            [Tooltip("音の種類をラベルで設定")]
            public SE se;
            [Tooltip("使用したい音を設定")]
            public AudioClip audioClip;
            [Range(0.0f, 1.0f), Tooltip("音量")] public float volume = 1.0f;
        }
    }
}