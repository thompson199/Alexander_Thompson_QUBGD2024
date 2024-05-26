using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public GameObject player;

    private float xOffset = 7f;
    private float yOffset = 1.7f;
    private float zOffset = -16f;

    private Vector3 cameraOffset;
    
    // Start is called before the first frame update
    void Start()
    {
        cameraOffset = new Vector3(xOffset, yOffset, zOffset);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + cameraOffset;
    }
}
