using UnityEngine;
using UniRx;
using Audio;

public class test:MonoBehaviour
    {
        [SerializeField] private AudioClip _clip;

        private void Start()
        {
            Observable.EveryUpdate()
                .Where(_ => Input.GetKeyDown(KeyCode.A))
                .Subscribe(_ =>
                {
                    AudioManager.Instance.Select(AudioManager.AudioType.SE)
                        .SetPitch(1.0f)
                        .SetVolume(1.0f)
                        .SetDelay(4.0f)
                        .PlaySE(_clip)
                        .OnCompleted(() => Debug.Log("OnCompleted!"));
                }).AddTo(this);
        }
    }