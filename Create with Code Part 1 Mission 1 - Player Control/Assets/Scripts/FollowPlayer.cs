using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;

    private Vector3 _camOffset = new Vector3(0, 5, -7);
    
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        // Offset the camera behind the player by adding to player's position
        transform.position = player.transform.position + _camOffset;
    }
}
