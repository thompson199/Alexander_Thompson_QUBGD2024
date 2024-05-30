using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private GameObject _self;
    
    private float xOffset = 7f;
    private float yOffset = 1.7f;
    private float zOffset = -16f;

    private Vector3 cameraOffset;
    
    public GameObject player;
    
    // Start is called before the first frame update
    private void Start()
    {
        _self = gameObject;
        cameraOffset = new Vector3(xOffset, yOffset, zOffset);
    }

    // Update is called once per frame
    private void Update()
    {
        _self.transform.position = player.transform.position + cameraOffset;
    }
}
