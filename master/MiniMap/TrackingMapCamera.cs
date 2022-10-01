using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Minimap
{
    public class TrackingMapCamera : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private float zoomSpeed = 3.0f;
        private Camera _camera;
        
        // Update is called once per frame
        private void Start()
        {
            _camera = this.gameObject.GetComponent<Camera>();
        }

        void LateUpdate()
        {
            var pos = player.transform.position;
            pos.y = transform.position.y;
            this.gameObject.transform.position = pos;

            this.gameObject.transform.rotation = Quaternion.Euler(90.0f, player.transform.eulerAngles.y, 0.0f);
            
            //ズーム機能
            _camera.orthographicSize += Input.mouseScrollDelta.y*zoomSpeed;
        }
    }
}