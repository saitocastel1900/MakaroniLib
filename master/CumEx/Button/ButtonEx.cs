using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Serialization;


namespace UiEx
{
    [RequireComponent(typeof(Image))]
    [AddComponentMenu("UI/ExButton", 14)]
    public class ButtonEx : MonoBehaviour,IPointerClickHandler,IPointerDownHandler,IPointerUpHandler,IPointerEnterHandler,IPointerExitHandler
    {
        [FormerlySerializedAs("PressedSprite"),Header("押した際のボタン画像")] [SerializeField] private Sprite pressedSprite;
        [FormerlySerializedAs("ButtonEnabled"),Header("ボタンを押した後、押せないようにするか")] [SerializeField] private bool buttonEnabled;
        [FormerlySerializedAs("OnClick"),Header("押した際に呼び出される関数")] [SerializeField] private UnityEvent onClick=null;
        
        private Sprite _defaultSprite;
        
        private void Awake()
        {
            _defaultSprite = this.gameObject.GetComponent<Image>().sprite;
        }

        private void OnEnable()
        {
            this.gameObject.SetActive(true);
            Debug.Log("Enable");
        }

        private void OnDisable()
        {
            Debug.Log("Disable");
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            onClick.Invoke();
            this.gameObject.GetComponent<Image>().sprite=_defaultSprite;
            Debug.Log("Button Click");
            if (buttonEnabled) this.gameObject.GetComponent<ButtonEx>().enabled=false;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            this.gameObject.GetComponent<Image>().sprite = pressedSprite;
            Debug.Log("Button Push");
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Debug.Log("Button Release");
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log("Button Enter");
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Debug.Log("Button  Exit");
        }

        public void TestOnClick()
        {
            Debug.Log("Event OnClick!");
        }
    }
}
