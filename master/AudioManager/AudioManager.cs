using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace Audio
{
    /// <summary>
    /// 音をメソッドチェーン方式で呼ぶ
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : SingletonMonoBehaviour<AudioManager>, IPlay,ISet,IResult
    {
        //audiosource
        [SerializeField] private AudioSource bgmAudioSource;
        [SerializeField] private AudioSource seAudioSource;

        //bgm
        private float _bgmVolume;
        public float BgmVolume => _bgmVolume;

        //se
        private float _seVolume;
        public float SeVolume => _seVolume;
        
        private float _delayTime = 0.0f;

      

        private void Awake()
        { 
            #region Singleton
            if (this != Instance)
            {
                Debug.LogError("インスタンスが既に存在しています。インスタンスを一つにするためこのインスタンスを破棄します");
                Destroy(this.gameObject);
                return;
            }

            DontDestroyOnLoad(this.gameObject);
            #endregion

            if (bgmAudioSource == null || seAudioSource == null)
            {
                Debug.LogError("[Audio]AudioSourceが設定されていません");
                return;
            }
        }
        
        public enum AudioType
        {
            SE,
            BGM,
        }

        private AudioType _type;
        
        public AudioManager Select(AudioType type)
        {
            _type = type;
            return this;
        }

        public AudioManager PlaySE(AudioClip clip, float duration = 0.0f)
        {
            if (seAudioSource == null) Debug.LogError("[Audio]AudioSourceがアタッチされていません");

            if (clip != null)
            {
                seAudioSource.time=duration==0.0f ? clip.length: duration;
                StartCoroutine(
                    DelayMethod(_delayTime,
                        () => { seAudioSource.PlayOneShot(clip); })
                );
            }
            else Debug.LogError("[Audio]AudioClipが設定されていません");

            return this;
        }

        public  AudioManager PlayBGM(AudioClip clip, float duration = 0.0f, bool loopFlag = false)
        {
            if (bgmAudioSource == null) Debug.LogError("[Audio]AudioSourceがアタッチされていません");

            if (clip != null)
            {
                bgmAudioSource.time=duration==0.0f ? clip.length: duration;
                bgmAudioSource.clip = clip;
                bgmAudioSource.loop = loopFlag;
                StartCoroutine(
                    DelayMethod(_delayTime,
                        () => { bgmAudioSource.Play(); })
                );
            }
            else Debug.LogError("[Audio]AudioClipが設定されていません");

            return this;
        }

        public AudioManager Stop()
        {
            if (bgmAudioSource == null) Debug.LogError("[Audio]AudioSourceがアタッチされていません");
            
            bgmAudioSource.Stop();
            bgmAudioSource.clip = null;

            return this;
        }

        public AudioManager Pause()
        {
            if (bgmAudioSource == null) Debug.LogError("[Audio]AudioSourceがアタッチされていません");
            if (bgmAudioSource.clip == null) Debug.LogError("[Audio]AudioClipが設定されていません");
            
            bgmAudioSource.Pause();
            
            return this;
        }

        public AudioManager UnPause()
        {
            if (bgmAudioSource == null) Debug.LogError("[Audio]AudioSourceがアタッチされていません");
            if (bgmAudioSource.clip == null) Debug.LogError("[Audio]AudioClipが設定されていません");
            
            bgmAudioSource.UnPause();
            
            return this;
        }

        public AudioManager SetVolume(float volume)
        {
            if (_type == AudioType.SE)
            {
                _seVolume=Math.Clamp(volume, 0.0f, 1.0f);
                seAudioSource.volume = SeVolume;
            }
            else
            {
                _bgmVolume=Math.Clamp(volume, 0.0f, 1.0f);
                bgmAudioSource.volume = BgmVolume;
            }
            
            return this;
        }

        public AudioManager SetPitch(float pitch)
        {
            if (_type == AudioType.SE)
            {
                seAudioSource.pitch = Math.Clamp(pitch, -3.0f, 3.0f);
            }
            else bgmAudioSource.pitch = Math.Clamp(pitch, -3.0f, 3.0f);
            
            return this;
        }

        public AudioManager SetDelay(float delay)
        {
            this._delayTime = delay;
            return this;
        }

        public async void OnCompleted(Action _event)
        {
            await Task.Delay(TimeSpan.FromSeconds(_delayTime));
            _delayTime = 0.0f;
            _event.Invoke();
        }
        
        private static IEnumerator DelayMethod(float waitTime,Action action){
            yield return new WaitForSeconds (waitTime);
            action ();
        }
    }
}