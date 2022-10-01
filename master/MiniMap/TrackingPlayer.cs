using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TrackingPlayer : MonoBehaviour
{
    [FormerlySerializedAs("_player")] [SerializeField] private GameObject player;

    // Update is called once per frame
    void LateUpdate()
    {
        var pos = player.transform.position;
        pos.y = transform.position.y;
        this.gameObject.transform.position = pos;
        
        this.gameObject.transform.rotation = Quaternion.Euler(player.transform.eulerAngles.x, player.transform.eulerAngles.y, player.transform.eulerAngles.z);
    }
}
